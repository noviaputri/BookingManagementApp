using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named EmployeeRepository that implements the IEmployeeRepository interface.
public class EmployeeRepository : IEmployeeRepository
{
    // Declares a private field of type BookingManagementDbContext.
    private readonly BookingManagementDbContext _context;

    // Declares a public constructor that takes a BookingManagementDbContext parameter.
    public EmployeeRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    // Declares a public method named GetAll that returns an IEnumerable<Employee>.
    public IEnumerable<Employee> GetAll()
    {
        return _context.Set<Employee>().ToList();
    }

    // Declares a public method named GetByGuid that takes a Guid parameter and returns an Employee object.
    public Employee? GetByGuid(Guid guid)
    {
        return _context.Set<Employee>().Find(guid);
    }

    // Declares a public method named Create that takes an Employee parameter and returns an Employee object.
    public Employee? Create(Employee employee)
    {
        try
        {
            _context.Set<Employee>().Add(employee);
            _context.SaveChanges();
            return employee;
        }
        catch
        {
            return null; // Returns null if there is an exception.
        }
    }

    // Declares a public method named Update that takes an Employee parameter and returns a boolean value.
    public bool Update(Employee employee)
    {
        try
        {
            _context.Set<Employee>().Update(employee);
            _context.SaveChanges();
            return true; // Returns true if the update was successful.
        }
        catch
        {
            return false; // Returns false if there is an exception.
        }
    }

    // Declares a public method named Delete that takes an Employee parameter and returns a boolean value.
    public bool Delete(Employee employee)
    {
        try
        {
            _context.Set<Employee>().Remove(employee);
            _context.SaveChanges();
            return true; // Returns true if the deletion was successful.
        }
        catch
        {
            return false; // Returns false if there is an exception.
        }
    }
}
