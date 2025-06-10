using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace ucondo_challenge.infrastructure.Cache;

public class RedisCache : IRedisCache
{
    private readonly IOptions<RedisOptions> _options;
    private ConnectionMultiplexer _conn = null!;
    private bool _connectionFailed;
    private bool _reconnecting;
    public static bool RedisConnected;
    private readonly ILogger<RedisCache> _logger;
        

    private static readonly object _syncRoot = new object();

    public RedisCache(IOptions<RedisOptions> options, IConfiguration configuration, ILogger<RedisCache> logger)
    {
        ArgumentNullException.ThrowIfNull(options);

        _options = options;
        _logger = logger;
        Connect(configuration);
    }

    private ConnectionMultiplexer Connect(IConfiguration configuration)
    {
        try
        {
            var redisUser = configuration["REDIS_USERNAME"];
            var redisPassword = configuration["REDIS_PASSWORD"];
            var connectionStringConfig = configuration["RedisCacheConfiguration:ConnectionString"];

            string connectionString;

            if (!string.IsNullOrEmpty(redisUser) && !string.IsNullOrEmpty(redisPassword))
            {
                connectionString = connectionStringConfig.Replace("{0}", redisPassword).Replace("{1}", redisUser);
                
                _logger.LogInformation($"Using REDIS connection string: {connectionString}");
            }
            else
            {
                connectionString = connectionStringConfig;
                
                _logger.LogInformation($"Using REDIS connection string: {connectionString}");
            }

            var options = ConfigurationOptions.Parse(connectionString);
            _conn = ConnectionMultiplexer.Connect(options);
            _conn.ConnectionFailed += (sender, e) => _connectionFailed = true;
            _conn.ConnectionRestored += (sender, e) => _connectionFailed = false;

            if (_options.Value.FailoverEnabled)
                RedisConnected = _conn.IsConnected;
        }
        catch (RedisConnectionException)
        {
            if (!_options.Value.FailoverEnabled)
                throw;

            RedisConnected = false;
        }

        return _conn;
    }


    private void CheckConnectionAndRetryConnectIfDisconnected()
    {
        if ((!_conn.IsConnected || _connectionFailed) && !_reconnecting)
        {
            lock (_syncRoot)
            {
                if (!_reconnecting)
                {
                    _reconnecting = true;
                    // Async retry.
                    Task.Run(async () =>
                    {
                        try
                        {
                            Debug.WriteLine($"Trying connect to redis server: {_options.Value.ConnectionString}");

                            var options = ConfigurationOptions.Parse(_options.Value.ConnectionString!);
                            options.AbortOnConnectFail = !_options.Value.FailoverEnabled;

                            _conn = await ConnectionMultiplexer.ConnectAsync(options);
                            _conn.ConnectionFailed += (sender, e) => _connectionFailed = true;
                            _conn.ConnectionRestored += (sender, e) => _connectionFailed = false;

                            _reconnecting = false;
                        }
                        catch (RedisConnectionException)
                        {
                            if (!_options.Value.FailoverEnabled)
                                throw;
                        }
                    });
                }
            }
        }
    }

    private IDatabase GetDatabase()
    {
        CheckConnectionAndRetryConnectIfDisconnected();
        return _conn.GetDatabase();
    }

    public TEntity GetSet<TEntity>(string key, Func<TEntity> func, TimeSpan expireAt, Func<TEntity, bool>? cond = null)
    {
        var alreadyCacheValue = Get<TEntity>(GetCacheKeyWithPrefix(key));

        if (alreadyCacheValue != null) return alreadyCacheValue;

        var data = func();
        return GetSet(key, data, expireAt, cond);
    }

    public TEntity GetSet<TEntity>(string key, TEntity data, TimeSpan expireAt, Func<TEntity, bool>? cond = null)
    {
        try
        {
            var alreadyCacheValue = Get<TEntity>(GetCacheKeyWithPrefix(key));

            if (alreadyCacheValue != null)
                return alreadyCacheValue;

            var isValid = cond?.Invoke(data) ?? true;
            if (isValid)
                Set(GetCacheKeyWithPrefix(key), data, expireAt);

            return data;
        }
        catch (RedisConnectionException)
        {
            return data;
        }
    }

    public async Task<TEntity> GetSetAsync<TEntity>(string key, Func<Task<TEntity>> func, TimeSpan expireAt, Func<TEntity, bool>? cond = null)
    {
        var cachedValue = await GetAsync<TEntity>(key);
        if (cachedValue != null) return cachedValue;

        var data = await func?.Invoke()!;

        return await GetSetAsync(key, data, expireAt, cond);
    }

    public async Task<TEntity> GetSetAsync<TEntity>(string key, TEntity data, TimeSpan expireAt, Func<TEntity, bool>? cond = null)
    {
        try
        {
            var alreadyCacheValue = await GetAsync<TEntity>(key);

            if (alreadyCacheValue != null)
                return alreadyCacheValue;

            var isValid = cond?.Invoke(data) ?? true;
            if (isValid)
                await SetAsync(key, data, expireAt);

            return data;
        }
        catch (RedisConnectionException)
        {
            return data;
        }
    }

    public void Set<TEntity>(string key, Func<TEntity> func, TimeSpan expireAt)
    {
        try
        {
            var db = GetDatabase();

            var value = func();

            if (value == null)
                return;

            var jsonValue = JsonSerializer.Serialize(value);
            db.StringSet(GetCacheKeyWithPrefix(key), jsonValue, expireAt);
        }
        catch (RedisConnectionException)
        {
            Debug.WriteLine("Error connecting to Redis.");
        }
    }

    public async Task SetAsync<TEntity>(string key, Func<Task<TEntity>> func, TimeSpan expireAt)
    {
        try
        {
            var db = GetDatabase();

            var value = await func?.Invoke()!;
            if (value == null) return;

            using var cacheLock = new ReaderWriterLockSlim();
            cacheLock.EnterWriteLock();
            try
            {
                var jsonValue = JsonSerializer.Serialize(value);
                await db.StringSetAsync(GetCacheKeyWithPrefix(key), jsonValue, expireAt);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }
        catch (RedisConnectionException)
        {
            Debug.WriteLine("Error connecting to Redis.");
        }
    }

    public void Set<TEntity>(string key, TEntity data, TimeSpan expireAt)
    {
        try
        {
            if (data == null) return;

            var db = GetDatabase();

            using var cacheLock = new ReaderWriterLockSlim();
            cacheLock.EnterWriteLock();
            try
            {
                var jsonValue = JsonSerializer.Serialize(data);
                db?.StringSet(GetCacheKeyWithPrefix(key), jsonValue, expireAt);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }
        catch (RedisConnectionException)
        {
            Debug.WriteLine("Error connecting to redis.");
        }
    }

    public async Task SetAsync<TEntity>(string key, TEntity data, TimeSpan expireAt)
    {
        try
        {
            if (data == null) return;

            var db = GetDatabase();

            var cacheKey = GetCacheKeyWithPrefix(key);
            var jsonValue = JsonSerializer.Serialize(data);

            await db.StringSetAsync(cacheKey, jsonValue, expireAt);
        }
        catch (RedisConnectionException ex)
        {
            Debug.WriteLine($"Error connecting to Redis. {ex}");
        }
    }

    public void Remove(string key)
    {
        try
        {
            var db = GetDatabase();

            if (!key.EndsWith("*"))
            {
                db.KeyDelete(GetCacheKeyWithPrefix(key));
            }
            else
            {
                const string script = "return redis.call('del', 'defaultKey', unpack(redis.call('keys', ARGV[1])))";
                db.ScriptEvaluate(
                    script,
                    [],
                    new RedisValue[] { GetCacheKeyWithPrefix(key) },
                    CommandFlags.None
                );
            }
        }
        catch (RedisConnectionException rEx)
        {
            throw new Exception($"Error on removing the key {key}", rEx);
        }
    }

    public async Task RemoveAsync(string key)
    {
        try
        {
            var db = GetDatabase();

            if (!key.EndsWith("*"))
            {
                await db.KeyDeleteAsync(GetCacheKeyWithPrefix(key));
            }
            else
            {
                const string script = "return redis.call('del', 'defaultKey', unpack(redis.call('keys', ARGV[1])))";
                await db.ScriptEvaluateAsync(
                    script,
                    [],
                    new RedisValue[] { GetCacheKeyWithPrefix(key) },
                    CommandFlags.None
                );
            }
        }
        catch (RedisConnectionException rEx)
        {
            throw new Exception($"Error on removing the key {key}", rEx);
        }
    }

    public TEntity Get<TEntity>(string key)
    {
        try
        {
            var db = GetDatabase();
            var redisValue = db?.StringGet(GetCacheKeyWithPrefix(key));

            if (redisValue.HasValue)
            {
                var valueToReturn = !string.IsNullOrEmpty(redisValue) ? JsonSerializer.Deserialize<TEntity>(redisValue!) : default;
                return valueToReturn!;
            }

            return default!;
        }
        catch (RedisConnectionException)
        {
            return default!;
        }
    }

    public async Task<TEntity> GetAsync<TEntity>(string key)
    {
        try
        {
            var db = GetDatabase();

            var redisValue = await db.StringGetAsync(GetCacheKeyWithPrefix(key));
            if (redisValue.HasValue)
            {
                var valueToReturn = !string.IsNullOrEmpty(redisValue) ? JsonSerializer.Deserialize<TEntity>(redisValue!) : default;
                return valueToReturn!;
            }

            return default!;
        }
        catch (RedisConnectionException)
        {
            return default!;
        }
    }

    

    public string GetCacheKeyWithPrefix(string clientKey)
    {
        var cacheKeyPrefix = clientKey;

        var options = ConfigurationOptions.Parse(_options.Value.ConnectionString!);

        if (!string.IsNullOrEmpty(options.ClientName))
            cacheKeyPrefix = $"{options.ClientName}";

        if (!string.IsNullOrWhiteSpace(cacheKeyPrefix) && !string.IsNullOrWhiteSpace(clientKey))
            return $"{cacheKeyPrefix}:{clientKey}";

        return !string.IsNullOrWhiteSpace(clientKey) ? $"{clientKey}" : cacheKeyPrefix;
    }

    public bool Exists(string key)
    {
        var db = GetDatabase();
        return db.KeyExists(GetCacheKeyWithPrefix(key));
    }
}