

using Microsoft.AspNetCore.Mvc;

using NetCore_InMemoryCash.Services;
/*
 In-Memory Cache Parameters

Size: This allows you to set the size of this particular cache entry, so that it doesn’t start consuming the server resources.

SlidingExpiration: How long the cache will be inactive. 
A cache entry will expire if it is not used by anyone for this particular time period. 
In our case, we have set SlidingExpiration is 2 minutes. 
If no requests for this cache entry for 2 minutes, then the cache will be deleted.

AbsoluteExpiration: Actual expiration time for cached value. Once the time is reached, 
then the cache entry will be removed. In our case, we have set AbsoluteExpiration is 5 minutes. 
The cache will be expired once the time is reached 5 minutes. Absolute expiration should not less than the Sliding Expiration.
 */
namespace NetCore_InMemoryCash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IEmployeeService   _employeeService;
        public EmployeeController(ICacheProvider cacheProvider)
        {          
            _cacheProvider =  cacheProvider;

        }
        //--------------------

        //[Route("getAllEmployee")]
        [HttpGet]
        public ActionResult GetAllEmployee()
        {
            //if (!_cacheProvider.TryGetValue(CacheKeys.Employees, out List<Employee> employees))
            //{
            //    // employees = GetEmployeesDeatilsFromDB(); // Get the data from database
            //    EmployeeService employee = new EmployeeService();
            //    var cacheEntryOptions = new MemoryCacheEntryOptions
            //    {
            //        AbsoluteExpiration = DateTime.Now.AddMinutes(5),
            //        SlidingExpiration = TimeSpan.FromMinutes(2),
            //        Size = 1024,
            //    };
            //    _cacheProvider.Set(CacheKeys.Employees, employees, cacheEntryOptions);
            //}
            //return Ok(employees);
            EmployeeService employee = new EmployeeService();
            try
            {
                var employees = _cacheProvider.GetCachedResponse();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    Content = "{ \n error : " + ex.Message + "}",
                    ContentType = "application/json"
                };
            }
        }

    }
}
