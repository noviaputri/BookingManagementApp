using API.Models;

namespace API.DTO.AccountRoles;

// Declares a new public class named GuidAccountRoleDto.
public class GuidAccountRoleDto
{
    public Guid Guid { get; set; } // Declares a public property named Guid of type Guid.

    // Declares a public static explicit conversion operator that takes a AccountRole parameter and returns a GuidAccountRoleDto object.
    public static explicit operator GuidAccountRoleDto(AccountRole accountRole)
    {
        // Returns a new GuidAccountRoleDto object with the properties of the accountRole parameter
        return new GuidAccountRoleDto
        {
            Guid = accountRole.Guid
        };
    }
}
