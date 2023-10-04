using API.Contracts;
using API.Data;
using API.Utilities.Handlers;

namespace API.Repositories;

// Declares a new public class named GeneralRepository with a generic type parameter TEntity that implements the IGeneralRepository interface.
public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : class
{
    // Declares a private field of type BookingManagementDbContext.
    private readonly BookingManagementDbContext _context;

    // Declares a protected constructor that takes a BookingManagementDbContext parameter.
    protected GeneralRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    // Declares a public method named GetAll that returns an IEnumerable of TEntity objects.
    public IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList(); // Returns all the entities in the database as a list.
    }

    // Declares a public method named GetByGuid that takes a Guid parameter and returns a TEntity object.
    public TEntity? GetByGuid(Guid guid)
    {
        var entity = _context.Set<TEntity>().Find(guid); // Finds an entity in the database with the specified GUID and assigns it to a local variable named entity.
        _context.ChangeTracker.Clear(); // Clears the change tracker to prevent any changes from being saved to the database.
        return entity; // Returns the entity object.
    }

    // Declares a public method named Create that takes a TEntity parameter and returns a TEntity object.
    public TEntity? Create(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity); // Adds the entity to the database.
            _context.SaveChanges(); // Saves changes to the database.
            return entity; // Returns the entity object.
        }
        catch (Exception ex)
        {
            // Exception for Nik, Email, and Phone Number employees if already exist.
            if (ex.InnerException is not null && ex.InnerException.Message.Contains("IX_tb_m_employees_nik"))
            {
                throw new ExceptionHandler("NIK already exists");
            }
            if (ex.InnerException is not null && ex.InnerException.Message.Contains("IX_tb_m_employees_email"))
            {
                throw new ExceptionHandler("Email already exists");
            }
            if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_tb_m_employees_phone_number"))
            {
                throw new ExceptionHandler("Phone number already exists");
            }
            // Throws a new ExceptionHandler exception with the InnerException.Message property.
            throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
        }
    }

    // Declares a public method named Update that takes a TEntity parameter and returns a boolean value.
    public bool Update(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Update(entity); // Updates the specified entity in the database.
            _context.SaveChanges(); // Saves changes to the database.
            return true; // Returns true if the update was successful.
        }
        catch (Exception ex)
        {
            // Throws a new ExceptionHandler exception with the InnerException.Message property.
            throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
        }
    }

    // Declares a public method named Delete that takes a TEntity parameter and returns a boolean value.
    public bool Delete(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Remove(entity); // Removes the specified entity from the database.
            _context.SaveChanges(); // Saves changes to the database.
            return true; // This line returns true if the deletion was successful.
        }
        catch (Exception ex)
        {
            // Throws a new ExceptionHandler exception with the InnerException.Message property.
            throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
        }
    }
}
