using API.Utilities.Enums;

namespace API.DTO.Bookings;

// Declares a new public class named BookingDetailDto.
public class BookingDetailDto
{
    public Guid Guid { get; set; } // Declares a public property named Guid of type Guid.
    public string BookedNIK { get; set; } // Declares a public property named BookedNIK of type string.
    public string BookedBy { get; set; } // Declares a public property named BookedBy of type string.
    public string RoomName { get; set; } // Declares a public property named RoomName of type string.
    public DateTime StartDate { get; set; } // Declares a public property named StartDate of type DateTime.
    public DateTime EndDate { get; set; } // Declares a public property named EndDate of type DateTime.
    public string Status { get; set; } // Declares a public property named Status of type string.
    public string Remarks { get; set; } // Declares a public property named Remarks of type string.
}
