using API.Models;

namespace API.DTO.Rooms;

// Declares a new public class named RoomDto.
public class RoomDto
{
    public Guid Guid { get; set; } // Declares a public property named Guid of type Guid.
    public string Name { get; set; } // Declares a public property named Name of type string.
    public int Floor { get; set; } // Declares a public property named Floor of type int.
    public int Capacity { get; set; } // Declares a public property named Capacity of type int.

    // Declares a public static explicit conversion operator that takes a Room parameter and returns a RoomDto object.
    public static explicit operator RoomDto(Room room)
    {
        // Returns a new RoomDto object with the properties of the room parameter
        return new RoomDto
        {
            Guid = room.Guid,
            Name = room.Name,
            Floor = room.Floor,
            Capacity = room.Capacity
        };
    }

    // Declares a public static implicit conversion operator that takes a RoomDto parameter and returns a Room object.
    public static implicit operator Room(RoomDto roomDto)
    {
        // Returns a new Room object with the properties of the roomDto parameter.
        return new Room
        {
            Guid = roomDto.Guid,
            Name = roomDto.Name,
            Floor = roomDto.Floor,
            Capacity = roomDto.Capacity,
            ModifiedDate = DateTime.Now
        };
    }
}
