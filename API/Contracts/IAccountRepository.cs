using API.Models;

namespace API.Contracts;

// Declares a new public interface named IAccountRepository.
public interface IAccountRepository : IGeneralRepository<Account>
{
}
