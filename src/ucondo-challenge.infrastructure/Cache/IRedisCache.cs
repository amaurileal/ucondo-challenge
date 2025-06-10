namespace ucondo_challenge.infrastructure.Cache;

public interface IRedisCache
{
    TEntity GetSet<TEntity>(string key, Func<TEntity> func, TimeSpan expireAt, Func<TEntity, bool>? cond = null);

    Task<TEntity> GetSetAsync<TEntity>(string key, Func<Task<TEntity>> entity, TimeSpan expireAt, Func<TEntity, bool>? cond = null);

    TEntity GetSet<TEntity>(string key, TEntity data, TimeSpan expireAt, Func<TEntity, bool>? cond = null);

    Task<TEntity> GetSetAsync<TEntity>(string key, TEntity data, TimeSpan expireAt, Func<TEntity, bool>? cond = null);

    void Set<TEntity>(string key, Func<TEntity> func, TimeSpan expireAt);

    Task SetAsync<TEntity>(string key, Func<Task<TEntity>> entity, TimeSpan expireAt);

    void Set<TEntity>(string key, TEntity data, TimeSpan expireAt);

    Task SetAsync<TEntity>(string key, TEntity data, TimeSpan expireAt);

    void Remove(string key);

    Task RemoveAsync(string key);

    TEntity Get<TEntity>(string key);

    Task<TEntity> GetAsync<TEntity>(string key);

    string GetCacheKeyWithPrefix(string clientKey);

    bool Exists(string key);
}