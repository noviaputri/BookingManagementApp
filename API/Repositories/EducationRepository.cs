using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named EducationRepository that implements the IEducationRepository interface.
public class EducationRepository : IEducationRepository
{
    // The context used to interact with the database
    private readonly BookingManagementDbContext _context;

    // Initializes a new instance of the EducationRepository class with BookingManagementDbContext object as a parameter
    public EducationRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    // Declares a public method named GetAll that returns an IEnumerable<Education>.
    public IEnumerable<Education> GetAll()
    {
        // Returns all the educations in the database as a list.
        return _context.Set<Education>().ToList();
    }

    // Declares a public method named GetByGuid that takes a Guid parameter and returns an Education object.
    public Education? GetByGuid(Guid guid)
    {
        // Finds an education in the database with the specified GUID and returns it.
        return _context.Set<Education>().Find(guid);
    }

    // Declares a public method named Create that takes an Education parameter and returns an Education object.
    public Education? Create(Education education)
    {
        try
        {
            _context.Set<Education>().Add(education); // Adds the education to the database.
            _context.SaveChanges(); // Saves changes to the database.
            return education; // Returns the education object.
        }
        catch
        {
            return null; // Returns null if there is an exception.
        }
    }

    // Declares a public method named Update that takes an Education parameter and returns a boolean value.
    public bool Update(Education education)
    {
        try
        {
            _context.Set<Education>().Update(education); // Updates the specified education in the database.
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    // Declares a public method named Delete that takes an Education parameter and returns a boolean value.
    public bool Delete(Education education)
    {
        try
        {
            _context.Set<Education>().Remove(education); // Removes the specified education from the database.
            _context.SaveChanges(); // Saves changes to the database.
            return true; // Returns true if the deletion was successful.
        }
        catch
        {
            return false; // Returns false if there is an exception.
        }
    }
}
