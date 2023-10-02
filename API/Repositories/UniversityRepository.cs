using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class UniversityRepository : IUniversityRepository
{
    // The context used to interact with the database
    private readonly BookingManagementDbContext _context;

    // Initializes a new instance of the UniversityRepository class with BookingManagementDbContext object as a parameter
    public UniversityRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    // Retrieves all universities from the database.
    public IEnumerable<University> GetAll()
    {
        return _context.Set<University>().ToList();
    }

    // Retrieves a university by its guid
    public University? GetByGuid(Guid guid)
    {
        return _context.Set<University>().Find(guid);
    }

    // Creates a new university in the database
    public University? Create(University university)
    {
        try
        {
            _context.Set<University>().Add(university);
            _context.SaveChanges();
            return university;
        }
        catch
        {
            return null;
        }
    }

    // Updates an existing university in the database
    public bool Update(University university)
    {
        try
        {
            _context.Set<University>().Update(university);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    // Deletes an existing university from the database
    public bool Delete(University university)
    {
        try
        {
            _context.Set<University>().Remove(university);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
