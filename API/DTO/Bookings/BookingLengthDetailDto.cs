namespace API.DTO.Bookings;

// Declares a new public class named BookingLengthDetailDto.
public class BookingLengthDetailDto
{
    public Guid RoomGuid { get; set; } // Declares a public property named RoomGuid of type Guid.
    public string RoomName { get; set; } // Declares a public property named RoomName of type string.
    public int BookingLength { get; set; } // Declares a public property named BookingLength of type int.
}
