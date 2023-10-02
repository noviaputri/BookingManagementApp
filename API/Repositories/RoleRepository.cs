using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named RoleRepository that implements the IRoleRepository interface.
public class RoleRepository : IRoleRepository
{
    // Declares a private field of type BookingManagementDbContext.
    private readonly BookingManagementDbContext _context;

    // Declares a public constructor that takes a BookingManagementDbContext parameter.
    public RoleRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    // Declares a public method named GetAll that returns an IEnumerable<Role>.
    public IEnumerable<Role> GetAll()
    {
        return _context.Set<Role>().ToList();
    }

    // Declares a public method named GetByGuid that takes a Guid parameter and returns a Role object.
    public Role? GetByGuid(Guid guid)
    {
        return _context.Set<Role>().Find(guid);
    }

    // Declares a public method named Create that takes a Role parameter and returns a Role object.
    public Role? Create(Role role)
    {
        try
        {
            _context.Set<Role>().Add(role); // Adds the role to the database.
            _context.SaveChanges(); // Saves changes to the database.
            return role; // Returns the role object.
        }
        catch
        {
            return null; // Returns null if there is an exception.
        }
    }

    // Declares a public method named Update that takes a Role parameter and returns a boolean value.
    public bool Update(Role role)
    {
        try
        {
            _context.Set<Role>().Update(role); // Updates the specified role in the database.
            _context.SaveChanges();
            return true; // Returns true if the update was successful.
        }
        catch
        {
            return false; // Returns false if there is an exception.
        }
    }

    // Declares a public method named Delete that takes a Role parameter and returns a boolean value.
    public bool Delete(Role role)
    {
        try
        {
            _context.Set<Role>().Remove(role); // Removes the specified role from the database.
            _context.SaveChanges();
            return true; // Returns true if the deletion was successful.
        }
        catch
        {
            return false; // Returns false if there is an exception.
        }
    }
}
