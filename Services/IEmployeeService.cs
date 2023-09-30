using NetCore_InMemoryCash.Models;

namespace NetCore_InMemoryCash.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetAll(); 
        Employee GetById(int id);
        Employee Add(Employee employee);
        bool Delete(int id);

    }
  
}
