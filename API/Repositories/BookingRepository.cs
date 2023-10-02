using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named BookingRepository that implements the IBookingRepository interface.
public class BookingRepository : IBookingRepository
{
    // Declares a private field of type BookingManagementDbContext.
    private readonly BookingManagementDbContext _context;

    // Declares a public constructor that takes a BookingManagementDbContext parameter.
    public BookingRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    // Declares a public method named GetAll that returns an IEnumerable<Booking>.
    public IEnumerable<Booking> GetAll()
    {
        // Returns all the bookings in the database as a list.
        return _context.Set<Booking>().ToList();
    }

    // Declares a public method named GetByGuid that takes a Guid parameter and returns a Booking object.
    public Booking? GetByGuid(Guid guid)
    {
        // Finds a booking in the database with the specified GUID and returns it.
        return _context.Set<Booking>().Find(guid);
    }

    // Declares a public method named Create that takes a Booking parameter and returns a Booking object.
    public Booking? Create(Booking booking)
    {
        try
        {
            _context.Set<Booking>().Add(booking); // Adds the booking to the database.
            _context.SaveChanges();
            return booking;
        }
        catch
        {
            return null;
        }
    }

    // Declares a public method named Update that takes a Booking parameter and returns a boolean value.
    public bool Update(Booking booking)
    {
        try
        {
            _context.Set<Booking>().Update(booking); // Updates the specified booking in the database.
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    // Declares a public method named Delete that takes a Booking parameter and returns a boolean value.
    public bool Delete(Booking booking)
    {
        try
        {
            _context.Set<Booking>().Remove(booking); // Removes the specified booking from the database.
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
