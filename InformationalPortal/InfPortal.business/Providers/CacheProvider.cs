using System.Web;
using InfPortal.business.Interfaces;

namespace InfPortal.business.Providers
{
    public class CacheProvider : ICacheProvider
    {
        public void Insert(string cacheKey, object value)
        {
            HttpRuntime.Cache.Insert(cacheKey, value);
        }

        public object Get(string cacheKey)
        {
            return HttpRuntime.Cache.Get(cacheKey);
        }

        public void Remove(string cacheKey)
        {
            if (Get(cacheKey) != null)
            {
                HttpRuntime.Cache.Remove(cacheKey);
            }
        }
    }
}