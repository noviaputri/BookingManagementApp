using API.Models;

namespace API.DTO.Universities;

// Declares a new public class named UniversityDto.
public class UniversityDto
{
    public Guid Guid { get; set; } // Declares a public property named Guid of type Guid.
    public string Code { get; set; } // Declares a public property named Code of type string.
    public string Name { get; set; } // Declares a public property named Name of type string.

    // Declares a public static explicit conversion operator that takes a University parameter and returns a UniversityDto object.
    public static explicit operator UniversityDto(University university)
    {
        // Returns a new UniversityDto object with the properties of the university parameter
        return new UniversityDto
        {
            Guid = university.Guid,
            Code = university.Code,
            Name = university.Name
        };
    }

    // Declares a public static implicit conversion operator that takes a UniversityDto parameter and returns a University object.
    public static implicit operator University(UniversityDto universityDto)
    {
        // Returns a new University object with the properties of the universityDto parameter.
        return new University
        {
            Guid = universityDto.Guid,
            Code = universityDto.Code,
            Name = universityDto.Name,
            ModifiedDate = DateTime.Now
        };
    }
}
