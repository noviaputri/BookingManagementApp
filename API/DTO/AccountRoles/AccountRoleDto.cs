using API.Models;

namespace API.DTO.AccountRoles;

// Declares a new public class named AccountRoleDto.
public class AccountRoleDto
{
    public Guid Guid { get; set; } // Declares a public property named Guid of type Guid.
    public Guid AccountGuid { get; set; } // Declares a public property named AccountGuid of type Guid.
    public Guid RoleGuid { get; set; } // Declares a public property named RoleGuid of type Guid.

    // Declares a public static explicit conversion operator that takes a AccountRole parameter and returns a AccountRoleDto object.
    public static explicit operator AccountRoleDto(AccountRole accountRole)
    {
        // Returns a new AccountRoleDto object with the properties of the accountRole parameter
        return new AccountRoleDto
        {
            Guid = accountRole.Guid,
            AccountGuid = accountRole.AccountGuid,
            RoleGuid = accountRole.RoleGuid
        };
    }

    // Declares a public static implicit conversion operator that takes a AccountRoleDto parameter and returns a AccountRole object.
    public static implicit operator AccountRole(AccountRoleDto accountRoleDto)
    {
        // Returns a new AccountRole object with the properties of the accountRoleDto parameter.
        return new AccountRole
        {
            Guid = accountRoleDto.Guid,
            AccountGuid = accountRoleDto.AccountGuid,
            RoleGuid = accountRoleDto.RoleGuid,
            ModifiedDate = DateTime.Now
        };
    }
}
