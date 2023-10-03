using API.Models;

namespace API.DTO.Roles;

// Declares a new public class named CreateRoleDto.
public class CreateRoleDto
{
    public string Name { get; set; } // Declares a public property named Name of type string.

    // Declares a public static implicit conversion operator that takes a CreateUniversityDto parameter and returns a University object.
    public static implicit operator Role(CreateRoleDto createRoleDto)
    {
        // Returns a new Role object with the properties of the createRoleDto parameter.
        return new Role
        {
            Name = createRoleDto.Name,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
