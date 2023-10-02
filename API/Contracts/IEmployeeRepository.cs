using API.Models;

namespace API.Contracts;

// Declares a new public interface named IEmployeeRepository.
public interface IEmployeeRepository
{
    // Declares all method in IEmployeeRepository interface
    IEnumerable<Employee> GetAll();
    Employee? GetByGuid(Guid guid);
    Employee? Create(Employee employee);
    bool Update(Employee employee);
    bool Delete(Employee employee);
}
