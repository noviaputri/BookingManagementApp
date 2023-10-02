using API.Models;

namespace API.Contracts;

// Declares a new public interface named IAccountRoleRepository.
public interface IAccountRoleRepository
{
    IEnumerable<AccountRole> GetAll(); // Declares a method named GetAll that returns an IEnumerable<AccountRole>.
    AccountRole? GetByGuid(Guid guid); // Declares a method named GetByGuid that takes a Guid parameter and returns an AccountRole object.
    AccountRole? Create(AccountRole accountRole); // Declares a method named Create that takes an AccountRole parameter and returns an AccountRole object.
    bool Update(AccountRole accountRole); // Declares a method named Update that takes an AccountRole parameter and returns a boolean value.
    bool Delete(AccountRole accountRole); // Declares a method named Delete that takes an AccountRole parameter and returns a boolean value.
}
