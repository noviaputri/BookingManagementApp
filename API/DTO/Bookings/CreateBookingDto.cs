using API.Models;
using API.Utilities.Enums;

namespace API.DTO.Bookings;

// Declares a new public class named CreateBookingDto.
public class CreateBookingDto
{
    public DateTime StartDate { get; set; } // Declares a public property named StartDate of type DateTime.
    public DateTime EndDate { get; set; } // Declares a public property named EndDate of type DateTime.
    public StatusLevel Status { get; set; } // Declares a public property named Status of type Enums.
    public string Remarks { get; set; } // Declares a public property named Remarks of type string.
    public Guid RoomGuid { get; set; } // Declares a public property named RoomGuid of type Guid.
    public Guid EmployeeGuid { get; set; } // Declares a public property named EmployeeGuid of type Guid.

    // Declares a public static implicit conversion operator that takes a CreateBookingDto parameter and returns a Booking object.
    public static implicit operator Booking(CreateBookingDto createBookingDto)
    {
        // Returns a new Booking object with the properties of the createBookingDto parameter.
        return new Booking
        {
            StartDate = createBookingDto.StartDate,
            EndDate = createBookingDto.EndDate,
            Status = createBookingDto.Status,
            Remarks = createBookingDto.Remarks,
            RoomGuid = createBookingDto.RoomGuid,
            EmployeeGuid = createBookingDto.EmployeeGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
