using API.Models;

namespace API.Contracts;

// Declares a new public interface named IEmployeeRepository.
public interface IEmployeeRepository : IGeneralRepository<Employee>
{
    // Declares method GetLastNik
    Employee GetLastNik();
}
