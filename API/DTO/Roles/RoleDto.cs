using API.Models;

namespace API.DTO.Roles;

// Declares a new public class named RoleDto.
public class RoleDto
{
    public Guid Guid { get; set; } // Declares a public property named Guid of type Guid.
    public string Name { get; set; } // Declares a public property named Name of type string.

    // Declares a public static explicit conversion operator that takes a Role parameter and returns a RoleDto object.
    public static explicit operator RoleDto(Role role)
    {
        // Returns a new RoleDto object with the properties of the role parameter
        return new RoleDto
        {
            Guid = role.Guid,
            Name = role.Name
        };
    }

    // Declares a public static implicit conversion operator that takes a RoleDto parameter and returns a Role object.
    public static implicit operator Role(RoleDto roleDto)
    {
        // Returns a new Role object with the properties of the roleDto parameter.
        return new Role
        {
            Guid = roleDto.Guid,
            Name = roleDto.Name,
            ModifiedDate = DateTime.Now
        };
    }
}
