using API.Models;

namespace API.DTO.Educations;

// Declares a new public class named CreateEducationDto.
public class CreateEducationDto
{
    public Guid Guid { get; set; } // Declares a public property named Guid of type Guid.
    public string Major { get; set; } // Declares a public property named Major of type string.
    public string Degree { get; set; } // Declares a public property named Degree of type string.
    public float Gpa { get; set; } // Declares a public property named Gpa of type float.
    public Guid UniversityGuid { get; set; } // Declares a public property named UniversityGuid of type Guid.

    // Declares a public static implicit conversion operator that takes a CreateUniversityDto parameter and returns a University object.
    public static implicit operator Education(CreateEducationDto createEducationDto)
    {
        // Returns a new University object with the properties of the createUniversityDto parameter.
        return new Education
        {
            Guid = createEducationDto.Guid,
            Major = createEducationDto.Major,
            Degree = createEducationDto.Degree,
            Gpa = createEducationDto.Gpa,
            UniversityGuid = createEducationDto.UniversityGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
