using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named RoomRepository that inherits from GeneralRepository<Room> and implements the IRoomRepository interface.
public class RoomRepository : GeneralRepository<Room>, IRoomRepository
{
    // Declares a public constructor that takes a BookingManagementDbContext parameter and calls the base constructor with the context parameter.
    public RoomRepository(BookingManagementDbContext context) : base(context) { }
}
