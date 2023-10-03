using API.Models;
using API.Utilities.Enums;

namespace API.DTO.Bookings;

// Declares a new public class named BookingDto.
public class BookingDto
{
    public Guid Guid { get; set; } // Declares a public property named Guid of type Guid.
    public DateTime StartDate { get; set; } // Declares a public property named StartDate of type DateTime.
    public DateTime EndDate { get; set; } // Declares a public property named EndDate of type DateTime.
    public StatusLevel Status { get; set; } // Declares a public property named Status of type Enums.
    public string Remarks { get; set; } // Declares a public property named Remarks of type string.
    public Guid RoomGuid { get; set; } // Declares a public property named RoomGuid of type Guid.
    public Guid EmployeeGuid { get; set; } // Declares a public property named EmployeeGuid of type Guid.

    // Declares a public static explicit conversion operator that takes a Booking parameter and returns a BookingDto object.
    public static explicit operator BookingDto(Booking booking)
    {
        // Returns a new BookingDto object with the properties of the booking parameter
        return new BookingDto
        {
            Guid = booking.Guid,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            Status = booking.Status,
            Remarks = booking.Remarks,
            RoomGuid = booking.RoomGuid,
            EmployeeGuid = booking.EmployeeGuid
        };
    }

    // Declares a public static implicit conversion operator that takes a BookingDto parameter and returns a Booking object.
    public static implicit operator Booking(BookingDto bookingDto)
    {
        // Returns a new Booking object with the properties of the bookingDto parameter.
        return new Booking
        {
            Guid = bookingDto.Guid,
            StartDate = bookingDto.StartDate,
            EndDate = bookingDto.EndDate,
            Status = bookingDto.Status,
            Remarks = bookingDto.Remarks,
            RoomGuid = bookingDto.RoomGuid,
            EmployeeGuid = bookingDto.EmployeeGuid,
            ModifiedDate = DateTime.Now
        };
    }
}
