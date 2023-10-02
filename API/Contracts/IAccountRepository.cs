using API.Models;

namespace API.Contracts;

// Declares a new public interface named IAccountRepository.
public interface IAccountRepository
{
    IEnumerable<Account> GetAll(); // Declares a method named GetAll that returns an IEnumerable<Account>.
    Account? GetByGuid(Guid guid); // Declares a method named GetByGuid that takes a Guid parameter and returns an Account object.
    Account? Create(Account account); // Declares a method named Create that takes an Account parameter and returns an Account object.
    bool Update(Account account); // Declares a method named Update that takes an Account parameter and returns a boolean value.
    bool Delete(Account account); // Declares a method named Delete that takes an Account parameter and returns a boolean value.
}
