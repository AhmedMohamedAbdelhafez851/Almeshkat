namespace BL.Healper
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CacheableAttribute : Attribute
    {
        public string CacheKey { get; }
        public int DurationInSeconds { get; }

        public CacheableAttribute(string cacheKey, int durationInSeconds = 600)
        {
            CacheKey = cacheKey;
            DurationInSeconds = durationInSeconds;
        }
    }
}
