using API.Models;

namespace API.DTO.Educations;

// Declares a new public class named EducationDto.
public class EducationDto
{
    public Guid Guid { get; set; } // Declares a public property named Guid of type Guid.
    public string Major { get; set; } // Declares a public property named Major of type string.
    public string Degree { get; set; } // Declares a public property named Degree of type string.
    public float Gpa { get; set; } // Declares a public property named Gpa of type float.
    public Guid UniversityGuid { get; set; } // Declares a public property named UniversityGuid of type Guid.

    // Declares a public static explicit conversion operator that takes a Education parameter and returns a EducationDto object.
    public static explicit operator EducationDto(Education education)
    {
        // Returns a new EducationDto object with the properties of the education parameter
        return new EducationDto
        {
            Guid = education.Guid,
            Major = education.Major,
            Degree = education.Degree,
            Gpa = education.Gpa,
            UniversityGuid = education.UniversityGuid
        };
    }

    // Declares a public static implicit conversion operator that takes a EducationDto parameter and returns a Education object.
    public static implicit operator Education(EducationDto educationDto)
    {
        // Returns a new Education object with the properties of the educationDto parameter.
        return new Education
        {
            Guid = educationDto.Guid,
            Major = educationDto.Major,
            Degree = educationDto.Degree,
            Gpa = educationDto.Gpa,
            UniversityGuid = educationDto.UniversityGuid,
            ModifiedDate = DateTime.Now
        };
    }

}
