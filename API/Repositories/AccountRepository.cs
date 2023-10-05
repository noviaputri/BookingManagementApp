using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named AccountRepository that inherits from GeneralRepository<Account> and implements the IAccountRepository interface.
public class AccountRepository : GeneralRepository<Account>, IAccountRepository
{
    // Declares a public constructor that takes a BookingManagementDbContext parameter and calls the base constructor with the context parameter.
    public AccountRepository(BookingManagementDbContext context) : base(context) {}
}
