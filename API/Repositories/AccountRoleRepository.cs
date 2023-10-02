using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named AccountRoleRepository that inherits from GeneralRepository<AccountRole> and implements the IAccountRoleRepository interface.
public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
{
    // Declares a public constructor that takes a BookingManagementDbContext parameter and calls the base constructor with the context parameter.
    public AccountRoleRepository(BookingManagementDbContext context) : base(context) { }
}
