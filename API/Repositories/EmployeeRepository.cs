using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named EmployeeRepository that inherits from GeneralRepository<Employee> and implements the IEmployeeRepository interface.
public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
    private readonly BookingManagementDbContext _context;

    // Declares a public constructor that takes a BookingManagementDbContext parameter and calls the base constructor with the context parameter.
    public EmployeeRepository(BookingManagementDbContext context) : base(context) 
    { 
        _context = context;
    }

    // Get last Nik
    Employee IEmployeeRepository.GetLastNik()
    {
        var lastNik = _context.Employees.OrderByDescending(e => e.Nik).FirstOrDefault();
        return lastNik;
    }
}
