using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named AccountRoleRepository that implements the IAccountRoleRepository interface.
public class AccountRoleRepository : IAccountRoleRepository
{
    // Declares a private field of type BookingManagementDbContext.
    private readonly BookingManagementDbContext _context;

    // Declares a public constructor that takes a BookingManagementDbContext parameter.
    public AccountRoleRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    // Declares a public method named GetAll that returns an IEnumerable<AccountRole>.
    public IEnumerable<AccountRole> GetAll()
    {
        // Returns all the account roles in the database as a list.
        return _context.Set<AccountRole>().ToList();
    }

    // Declares a public method named GetByGuid that takes a Guid parameter and returns an AccountRole object.
    public AccountRole? GetByGuid(Guid guid)
    {
        // Finds an account role in the database with the specified GUID and returns it.
        return _context.Set<AccountRole>().Find(guid);
    }

    // Declares a public method named Create that takes an AccountRole parameter and returns an AccountRole object.
    public AccountRole? Create(AccountRole accountRole)
    {
        try
        {
            _context.Set<AccountRole>().Add(accountRole); // Adds the accountRole to the database.
            _context.SaveChanges(); // Saves changes to the database.
            return accountRole; // Returns the accountRole object.
        }
        catch
        {
            return null; // Returns null if there is an exception.
        }
    }

    // Declares a public method named Update that takes an AccountRole parameter and returns a boolean value.
    public bool Update(AccountRole accountRole)
    {
        try
        {
            _context.Set<AccountRole>().Update(accountRole); // Updates the specified accountRole in the database.
            _context.SaveChanges(); // Saves changes to the database.
            return true; // Returns true if the update was successful.
        }
        catch
        {
            return false; // Returns false if there is an exception.
        }
    }

    // Declares a public method named Delete that takes an AccountRole parameter and returns a boolean value.
    public bool Delete(AccountRole accountRole)
    {
        try
        {
            _context.Set<AccountRole>().Remove(accountRole); // Removes the specified accountRole from the database.
            _context.SaveChanges(); // Saves changes to the database.
            return true; // Returns true if the deletion was successful.
        }
        catch
        {
            return false; // Returns false if there is an exception.
        }
    }
}
