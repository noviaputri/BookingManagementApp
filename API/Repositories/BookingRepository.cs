using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named BookingRepository that inherits from GeneralRepository<Booking> and implements the IBookingRepository interface.
public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
{
    // Declares a public constructor that takes a BookingManagementDbContext parameter and calls the base constructor with the context parameter.
    public BookingRepository(BookingManagementDbContext context) : base(context) { }
}
