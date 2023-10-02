using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named RoomRepository that implements the IRoomRepository interface.
public class RoomRepository : IRoomRepository
{
    // Declares a private field of type BookingManagementDbContext.
    private readonly BookingManagementDbContext _context;

    // Declares a public constructor that takes a BookingManagementDbContext parameter.
    public RoomRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    // Declares a public method named GetAll that returns an IEnumerable<Room>.
    public IEnumerable<Room> GetAll()
    {
        return _context.Set<Room>().ToList();
    }

    // Declares a public method named GetByGuid that takes a Guid parameter and returns a Room object.
    public Room? GetByGuid(Guid guid)
    {
        return _context.Set<Room>().Find(guid);
    }

    // Declares a public method named Create that takes a Room parameter and returns a Room object.
    public Room? Create(Room room)
    {
        try
        {
            _context.Set<Room>().Add(room); // Adds the room to the database.
            _context.SaveChanges(); // Saves changes to the database.
            return room; // Returns the room object.
        }
        catch
        {
            return null; // Returns null if there is an exception.
        }
    }

    // Declares a public method named Update that takes a Room parameter and returns a boolean value.
    public bool Update(Room room)
    {
        try
        {
            _context.Set<Room>().Update(room); // Updates the specified room in the database.
            _context.SaveChanges();
            return true; // Returns true if the update was successful.
        }
        catch
        {
            return false; // Returns false if there is an exception.
        }
    }

    // Declares a public method named Delete that takes a Room parameter and returns a boolean value.
    public bool Delete(Room room)
    {
        try
        {
            _context.Set<Room>().Remove(room); // Removes the specified room from the database.
            _context.SaveChanges();
            return true; // Returns true if the deletion was successful.
        }
        catch
        {
            return false; // Returns false if there is an exception.
        }
    }
}
