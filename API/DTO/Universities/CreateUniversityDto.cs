using API.Models;

namespace API.DTO.Universities;

// Declares a new public class named CreateUniversityDto.
public class CreateUniversityDto
{
    public string Code { get; set; } // Declares a public property named Code of type string.
    public string Name { get; set; } // Declares a public property named Name of type string.

    // Declares a public static implicit conversion operator that takes a CreateUniversityDto parameter and returns a University object.
    public static implicit operator University(CreateUniversityDto createUniversityDto)
    {
        // Returns a new University object with the properties of the createUniversityDto parameter.
        return new University
        {
            Code = createUniversityDto.Code,
            Name = createUniversityDto.Name,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
