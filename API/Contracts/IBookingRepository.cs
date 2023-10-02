using API.Models;

namespace API.Contracts;

// Declares a new public interface named IBookingRepository.
public interface IBookingRepository
{
    IEnumerable<Booking> GetAll(); // Declares a method named GetAll that returns an IEnumerable<Booking>.
    Booking? GetByGuid(Guid guid); // Declares a method named GetByGuid that takes a Guid parameter and returns a Booking object.
    Booking? Create(Booking booking); // Declares a method named Create that takes a Booking parameter and returns a Booking object.
    bool Update(Booking booking); // Declares a method named Update that takes a Booking parameter and returns a boolean value.
    bool Delete(Booking booking); // Declares a method named Delete that takes a Booking parameter and returns a boolean value.
}
