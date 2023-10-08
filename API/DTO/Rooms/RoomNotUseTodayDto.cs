namespace API.DTO.Rooms;

// Declares a new public class named RoomNotUseTodayDto.
public class RoomNotUseTodayDto
{
    public Guid RoomGuid { get; set; } // Declares a public property named RoomGuid of type Guid.
    public string RoomName { get; set; } // Declares a public property named RoomName of type string.
    public int Floor { get; set; } // Declares a public property named Floor of type int.
    public int Capacity { get; set; } // Declares a public property named Capacity of type int.
}
