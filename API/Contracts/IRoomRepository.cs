using API.Models;

namespace API.Contracts;

// Declares a new public interface named IRoomRepository.
public interface IRoomRepository
{
    // Declares all method in IRoomRepository interface
    IEnumerable<Room> GetAll();
    Room? GetByGuid(Guid guid);
    Room? Create(Room room);
    bool Update(Room room);
    bool Delete(Room room);
}
