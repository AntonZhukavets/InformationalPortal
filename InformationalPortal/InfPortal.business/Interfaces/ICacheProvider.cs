namespace InfPortal.business.Interfaces
{
    public interface ICacheProvider
    {
        void Insert(string cacheKey, object value);
        object Get(string cacheKey);
        void Remove(string cacheKey);
    }
}
