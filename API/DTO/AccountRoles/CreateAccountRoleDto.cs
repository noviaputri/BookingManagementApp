using API.Models;

namespace API.DTO.AccountRoles;

// Declares a new public class named CreateAccountRoleDto.
public class CreateAccountRoleDto
{
    public Guid AccountGuid { get; set; } // Declares a public property named AccountGuid of type Guid.
    public Guid RoleGuid { get; set; } // Declares a public property named RoleGuid of type Guid.

    // Declares a public static implicit conversion operator that takes a CreateAccountRoleDto parameter and returns a AccountRole object.
    public static implicit operator AccountRole(CreateAccountRoleDto createAccountRoleDto)
    {
        // Returns a new AccountRole object with the properties of the createAccountRoleDto parameter.
        return new AccountRole
        {
            AccountGuid = createAccountRoleDto.AccountGuid,
            RoleGuid = createAccountRoleDto.RoleGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
