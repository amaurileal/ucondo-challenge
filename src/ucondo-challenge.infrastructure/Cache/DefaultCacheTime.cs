namespace ucondo_challenge.infrastructure.Cache
{
    public static class DefaultCacheTime
    {
        public static TimeSpan ExpiresInYear = new(365, 0, 0, 0);
        public static TimeSpan Month = new(30, 0, 0, 0);
    }
}
