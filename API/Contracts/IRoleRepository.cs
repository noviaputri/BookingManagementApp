using API.Models;

namespace API.Contracts;

// Declares a new public interface named IRoleRepository.
public interface IRoleRepository
{
    // Declares all method in IRoleRepository interface
    IEnumerable<Role> GetAll();
    Role? GetByGuid(Guid guid);
    Role? Create(Role role);
    bool Update(Role role);
    bool Delete(Role role);
}
