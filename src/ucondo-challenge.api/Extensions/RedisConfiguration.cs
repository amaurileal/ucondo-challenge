using ucondo_challenge.infrastructure.Cache;

namespace ucondo_challenge.api.Extensions;

public static class RedisConfiguration
{
    public static void AddRedisConfiguration(this IServiceCollection services, IConfiguration configuration)
    {        
        services.Configure<RedisOptions>(options => configuration.GetSection("RedisCacheConfiguration").Bind(options));
    }
}