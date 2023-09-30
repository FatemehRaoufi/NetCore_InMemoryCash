using NetCore_InMemoryCash.Models;


namespace NetCore_InMemoryCash.Services
{
    public interface ICacheProvider
    {      
        Task<IEnumerable<Employee>> GetCachedResponse();
        Task<IEnumerable<Employee>> GetCachedResponse(string cacheKey, SemaphoreSlim semaphore);
    }
  
}
