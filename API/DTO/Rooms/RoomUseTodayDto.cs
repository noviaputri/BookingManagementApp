namespace API.DTO.Rooms;

// Declares a new public class named RoomUseTodayDto.
public class RoomUseTodayDto
{
    public Guid BookingGuid { get; set; } // Declares a public property named BookingGuid of type Guid.
    public string RoomName { get; set; } // Declares a public property named RoomName of type string.
    public string Status { get; set; } // Declares a public property named Status of type string.
    public int Floor { get; set; } // Declares a public property named Floor of type int.
    public string BookBy { get; set; } // Declares a public property named BookBy of type string.
}
