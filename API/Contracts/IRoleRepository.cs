using API.Data;
using API.Models;

namespace API.Contracts;

// Declares a new public interface named IRoleRepository.
public interface IRoleRepository : IGeneralRepository<Role>
{
    // Declares method GetDefaultRoleGuid and GetContext.
    Guid? GetDefaultRoleGuid();
}
