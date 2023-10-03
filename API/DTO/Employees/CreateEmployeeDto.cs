using API.Models;
using API.Utilities.Enums;

namespace API.DTO.Employees;

// Declares a new public class named CreateEmployeeDto.
public class CreateEmployeeDto
{
    public string Nik { get; set; } // Declares a public property named Nik of type string.
    public string FirstName { get; set; } // Declares a public property named FirstName of type string.
    public string? LastName { get; set; } // Declares a public property named LastName of type string.
    public DateTime BirthDate { get; set; } // Declares a public property named BirthDate of type DateTime.
    public GenderLevel Gender { get; set; } // Declares a public property named Gender of type Enums.
    public DateTime HiringDate { get; set; } // Declares a public property named HiringDate of type DateTime.
    public string Email { get; set; } // Declares a public property named Email of type string.
    public string PhoneNumber { get; set; } // Declares a public property named PhoneNumber of type string.

    // Declares a public static implicit conversion operator that takes a CreateEmployeeDto parameter and returns a Employee object.
    public static implicit operator Employee(CreateEmployeeDto createEmployeeDto)
    {
        // Returns a new Employee object with the properties of the createEmployeeDto parameter.
        return new Employee
        {
            Nik = createEmployeeDto.Nik,
            FirstName = createEmployeeDto.FirstName,
            LastName = createEmployeeDto.LastName,
            BirthDate = createEmployeeDto.BirthDate,
            Gender = createEmployeeDto.Gender,
            HiringDate = createEmployeeDto.HiringDate,
            Email = createEmployeeDto.Email,
            PhoneNumber = createEmployeeDto.PhoneNumber,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
