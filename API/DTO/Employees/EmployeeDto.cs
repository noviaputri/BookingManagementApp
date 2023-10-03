using API.Models;
using API.Utilities.Enums;

namespace API.DTO.Employees;

// Declares a new public class named EmployeeDto.
public class EmployeeDto
{
    public Guid Guid { get; set; } // Declares a public property named Guid of type Guid.
    public string Nik { get; set; } // Declares a public property named Nik of type string.
    public string FirstName { get; set; } // Declares a public property named FirstName of type string.
    public string? LastName { get; set; } // Declares a public property named LastName of type string.
    public DateTime BirthDate { get; set; } // Declares a public property named BirthDate of type DateTime.
    public GenderLevel Gender { get; set; } // Declares a public property named Gender of type Enums.
    public DateTime HiringDate { get; set; } // Declares a public property named HiringDate of type DateTime.
    public string Email { get; set; } // Declares a public property named Email of type string.
    public string PhoneNumber { get; set; } // Declares a public property named PhoneNumber of type string.

    // Declares a public static explicit conversion operator that takes a Employee parameter and returns a EmployeeDto object.
    public static explicit operator EmployeeDto(Employee employee)
    {
        // Returns a new EmployeeDto object with the properties of the employee parameter
        return new EmployeeDto
        {
            Guid = employee.Guid,
            Nik = employee.Nik,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber
        };
    }

    // Declares a public static implicit conversion operator that takes a EmployeeDto parameter and returns a Employee object.
    public static implicit operator Employee(EmployeeDto employeeDto)
    {
        // Returns a new Employee object with the properties of the employeeDto parameter.
        return new Employee
        {
            Guid = employeeDto.Guid,
            Nik = employeeDto.Nik,
            FirstName = employeeDto.FirstName,
            LastName = employeeDto.LastName,
            BirthDate = employeeDto.BirthDate,
            Gender = employeeDto.Gender,
            HiringDate = employeeDto.HiringDate,
            Email = employeeDto.Email,
            PhoneNumber = employeeDto.PhoneNumber,
            ModifiedDate = DateTime.Now
        };
    }
}
