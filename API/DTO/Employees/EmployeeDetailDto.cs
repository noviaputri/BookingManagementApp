using API.Utilities.Enums;

namespace API.DTO.Employees;

// Declares a new public class named EmployeeDetailDto.
public class EmployeeDetailDto
{
    public Guid Guid { get; set; } // Declares a public property named Guid of type Guid.
    public string Nik { get; set; } // Declares a public property named Nik of type string.
    public string FullName { get; set; } // Declares a public property named FullName of type string.
    public DateTime BirthDate { get; set; } // Declares a public property named BirthDate of type DateTime.
    public string Gender { get; set; } // Declares a public property named Gender of type string.
    public DateTime HiringDate { get; set; } // Declares a public property named HiringDate of type DateTime.
    public string Email { get; set; } // Declares a public property named Email of type string.
    public string PhoneNumber { get; set; } // Declares a public property named PhoneNumber of type string.
    public string Major { get; set; } // Declares a public property named Major of type string.
    public string Degree { get; set; } // Declares a public property named Degree of type string.
    public float Gpa { get; set; } // Declares a public property named Gpa of type float.
    public string University { get; set; } // Declares a public property named University of type string.
}
