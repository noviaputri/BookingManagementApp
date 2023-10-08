using API.Data;
using API.Models;

namespace API.Contracts;

// Declares a new public interface named IAccountRepository.
public interface IAccountRepository : IGeneralRepository<Account>
{
    // Defines a method for getting the context.
    BookingManagementDbContext GetContext();
}
