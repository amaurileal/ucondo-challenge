namespace ucondo_challenge.infrastructure.Cache;

public class RedisOptions
{
    public string? ConnectionString { get; set; }

    public bool FailoverEnabled { get; set; } = false;
}