using API.Models;

namespace API.DTO.Rooms;

// Declares a new public class named CreateRoomDto.
public class CreateRoomDto
{
    public string Name { get; set; } // Declares a public property named Name of type string.
    public int Floor { get; set; } // Declares a public property named Floor of type int.
    public int Capacity { get; set; } // Declares a public property named Capacity of type int.

    // Declares a public static implicit conversion operator that takes a CreateRoomDto parameter and returns a Room object.
    public static implicit operator Room(CreateRoomDto createRoomDto)
    {
        // Returns a new Room object with the properties of the createRoomDto parameter.
        return new Room
        {
            Name = createRoomDto.Name,
            Floor = createRoomDto.Floor,
            Capacity = createRoomDto.Capacity,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
