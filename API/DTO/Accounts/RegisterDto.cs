using API.Models;
using API.Utilities.Enums;

namespace API.DTO.Accounts;

// Declares a new public class named RegisterDto.
public class RegisterDto
{
    public string FirstName { get; set; } // Declares a public property named FirstName of type string.
    public string? LastName { get; set; } // Declares a public property named LastName of type string.
    public DateTime BirthDate { get; set; } // Declares a public property named BirthDate of type DateTime.
    public GenderLevel Gender { get; set; } // Declares a public property named Gender of type Enums.
    public DateTime HiringDate { get; set; } // Declares a public property named HiringDate of type DateTime.
    public string Email { get; set; } // Declares a public property named Email of type string.
    public string PhoneNumber { get; set; } // Declares a public property named PhoneNumber of type string.
    public string Major { get; set; } // Declares a public property named Major of type string.
    public string Degree { get; set; } // Declares a public property named Degree of type string.
    public float Gpa { get; set; } // Declares a public property named Gpa of type float.
    public string Code { get; set; } // Declares a public property named Code of type string.
    public string Name { get; set; } // Declares a public property named Name of type string.
    public string Password { get; set; } // Declares a public property named Password of type string.
    public string ConfirmPassword { get; set; } // Declares a public property named ConfirmPassword of type string.
}
