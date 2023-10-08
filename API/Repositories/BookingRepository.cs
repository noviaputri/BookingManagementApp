using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Declares a new public class named BookingRepository that inherits from GeneralRepository<Booking> and implements the IBookingRepository interface.
public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
{
    // Declares a public constructor that takes a BookingManagementDbContext parameter and calls the base constructor with the context parameter.
    public BookingRepository(BookingManagementDbContext context) : base(context) { }

    // Declares a public method GetBookingLength that returns booking length in int.
    public int GetBookingLength(DateTime startDate, DateTime endDate)
    {
        // Calculates the number of days between the start date and end date.
        int days = (int)(endDate - startDate).TotalDays;
        // Calculates the number of weekends in the booking period.
        int weekends = days / 7 * 2;
        // Calculates the number of remaining days after removing weekends.
        int remainingDays = days % 7;
        // Adjusts the remaining days to account for partial weekends.
        if (remainingDays > 0)
        {
            int startDayOfWeek = (int)startDate.DayOfWeek;
            int endDayOfWeek = (int)endDate.DayOfWeek;
            if (startDayOfWeek == 0)
            {
                remainingDays--;
            }
            else if (startDayOfWeek <= endDayOfWeek)
            {
                if (endDayOfWeek >= 6)
                {
                    remainingDays--;
                }
            }
            else
            {
                remainingDays -= 2;
            }
        }
        // Calculates the total length of the booking period in days.
        return days - weekends - remainingDays;
    }
}
