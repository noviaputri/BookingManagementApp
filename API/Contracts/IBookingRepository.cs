using API.Models;

namespace API.Contracts;

// Declares a new public interface named IBookingRepository.
public interface IBookingRepository : IGeneralRepository<Booking>
{
    // Declares method GetBookingLength
    int GetBookingLength(DateTime startDate, DateTime endDate);
}
