using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named AccountRepository that implements the IAccountRepository interface.
public class AccountRepository : IAccountRepository
{
    // Declares a private field of type BookingManagementDbContext.
    private readonly BookingManagementDbContext _context;

    // Declares a public constructor that takes a BookingManagementDbContext parameter.
    public AccountRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    // Declares a public method named GetAll that returns an IEnumerable<Account>.
    public IEnumerable<Account> GetAll()
    {
        // Returns all the accounts in the database as a list.
        return _context.Set<Account>().ToList();
    }

    // Declares a public method named GetByGuid that takes a Guid parameter and returns an Account object.
    public Account? GetByGuid(Guid guid)
    {
        // Finds an account in the database with the specified GUID and returns it.
        return _context.Set<Account>().Find(guid);
    }

    // Declares a public method named Create that takes an Account parameter and returns an Account object.
    public Account? Create(Account account)
    {
        try
        {
            _context.Set<Account>().Add(account); // Adds the account to the database.
            _context.SaveChanges(); // Saves changes to the database.
            return account; // Returns the account object.
        }
        catch
        {
            return null; // Returns null if there is an exception.
        };
    }

    // Declares a public method named Update that takes an Account parameter and returns a boolean value.
    public bool Update(Account account)
    {
        try
        {
            _context.Set<Account>().Update(account); // Updates the specified account in the database.
            _context.SaveChanges(); // Saves changes to the database.
            return true; // Returns true if the update was successful.
        }
        catch
        {
            return false; // Returns false if there is an exception.
        }
    }

    // Declares a public method named Delete that takes an Account parameter and returns a boolean value.
    public bool Delete(Account account)
    {
        try
        {
            _context.Set<Account>().Remove(account); // Removes the specified account from the database.
            _context.SaveChanges(); // Saves changes to the database.
            return true; // Returns true if the deletion was successful.
        }
        catch
        {
            return false; // Returns false if there is an exception.
        }
    }
}
