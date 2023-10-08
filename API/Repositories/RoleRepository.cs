using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named RoleRepository that inherits from GeneralRepository<Role> and implements the IRoleRepository interface.
public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    // Declares a public constructor that takes a BookingManagementDbContext parameter and calls the base constructor with the context parameter.
    public RoleRepository(BookingManagementDbContext context) : base(context) { }

    // Declares a public method GetDefaultRoleGuid that returns RoleGuid.
    public Guid? GetDefaultRoleGuid()
    {
        return _context.Set<Role>().FirstOrDefault(r => r.Name == "User")!.Guid;
    }
}
