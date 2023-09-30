

using LazyCache;
using Microsoft.Extensions.Caching.Memory; //Installing "Microsoft.Extensions.Caching.Memory" from Nuget

using NetCore_InMemoryCash.Models;
using System.Threading;

namespace NetCore_InMemoryCash.Services
{
    public class CacheProvider : ICacheProvider
    {
        private static readonly SemaphoreSlim GetUsersSemaphore = new SemaphoreSlim(1, 1);
        private readonly IMemoryCache _cache;
        public CacheProvider(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        
        public  Task<IEnumerable<Employee>> GetCachedResponse()
        {
            try
            {
                return  GetCachedResponse(CacheKeys.Employees, GetUsersSemaphore);
            }
            catch
            {
                throw;
            }
        }



        public async Task<IEnumerable<Employee>> GetCachedResponse(string cacheKey, SemaphoreSlim semaphore)
        {
            //First, we are checking if the cache has value or not.
            bool isAvaiable = _cache.TryGetValue(cacheKey, out List<Employee> employees);
            if (isAvaiable) return employees; //If the value is available in the cache, then getting the value from the cache and send response to the user

            //else then asynchronously wait to enter the Semaphore:

            try
            {
                await semaphore.WaitAsync();
                /*
                   Once a thread has been granted access to the Semaphore, 
                  recheck if the value has been populated previously for safety (Avoid concurrent thread access):
                */

                isAvaiable = _cache.TryGetValue(cacheKey, out employees);
                if (isAvaiable) return employees;

                //Still don’t have a value in the cache then, call the database and store the value in the cache:
                //employees = EmployeeService.GetEmployeesDeatilsFromDB();
                EmployeeService employee = new EmployeeService();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024,
                };
                _cache.Set(cacheKey, employees, cacheEntryOptions);
            }
            catch
            {
                throw;
            }
            finally
            {
                semaphore.Release();
            }
            return employees; //Send response to the user.
        }

       
    }
}