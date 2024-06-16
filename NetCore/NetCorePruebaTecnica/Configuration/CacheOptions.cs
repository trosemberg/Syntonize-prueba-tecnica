using Microsoft.Extensions.Caching.Distributed;

namespace TechTest.Configuration
{
    public static class CacheOptions
    {
        public static DistributedCacheEntryOptions DefaultExpiration => new() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) };
    }
}
