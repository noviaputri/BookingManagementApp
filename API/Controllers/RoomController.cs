using API.Contracts;
using API.DTO.Rooms;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // This attribute indicates that the class is an API controller.
[Route("api/[controller]")] // This attribute specifies the route prefix for the controller
// Declares a new class named RoomController that inherits from ControllerBase.
public class RoomController : ControllerBase
{
    // Declares a private field of type IRoomRepository.
    private readonly IRoomRepository _roomRepository;

    // Declares a public constructor that takes an IRoomRepository parameter.
    public RoomController(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    [HttpGet]
    // Declares a public method named GetAll that returns an IActionResult.
    public IActionResult GetAll()
    {
        // Calls the GetAll method of the _roomRepository field and assigns the result to a local variable named result.
        var result = _roomRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found"); // Returns a 404 Not Found response with a message if the result is empty.
        }
        // Converts each Room object in the result to a RoomDto object and assigns the result to a local variable named data.
        var data = result.Select(x => (RoomDto)x);
        // Returns a 200 OK response with the data if it is not empty.
        return Ok(data);
    }

    [HttpGet("{guid}")]
    // Declares a public method named GetByGuid that takes a Guid parameter and returns an IActionResult.
    public IActionResult GetByGuid(Guid guid)
    {
        // Calls the GetByGuid method of the _roomRepository field with the guid parameter and assigns the result to a local variable named result.
        var result = _roomRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        // Returns a 200 OK response with the RoomDto object created from the result object.
        return Ok((RoomDto)result);
    }

    [HttpPost]
    // Declares a public method named Create that takes a CreateRoomDto parameter and returns an IActionResult.
    public IActionResult Create(CreateRoomDto roomDto)
    {
        // Calls the Create method of the _roomRepository field with the roomDto parameter and assigns the result to a local variable named result.
        var result = _roomRepository.Create(roomDto);
        if (result is null)
        {
            return BadRequest("Failed to create data"); // Returns a 400 Bad Request response with a message if the result is null.
        }
        // Returns a 200 OK response with the RoomDto object created from the result object.
        return Ok((RoomDto)result);
    }

    [HttpPut]
    // Declares a public method named Update that takes a RoomDto parameter and returns an IActionResult.
    public IActionResult Update(RoomDto roomDto)
    {
        // Calls the GetByGuid method of the _roomRepository field with the Guid property of the roomDto parameter.
        // Assigns the result to a local variable named entity.
        var entity = _roomRepository.GetByGuid(roomDto.Guid);
        if (entity is null)
        {
            return NotFound("Id Not Found"); // Returns a 404 Not Found response with a message if the entity is null.
        }
        // Assigns the roomDto parameter to a new Room object named toUpdate.
        Room toUpdate = roomDto;
        // Assigns the CreatedDate property of the entity object to the CreatedDate property of the toUpdate object.
        toUpdate.CreatedDate = entity.CreatedDate;
        // Calls the Update method of the _roomRepository field with the toUpdate object and assigns the result to a local variable named result.
        var result = _roomRepository.Update(toUpdate);
        if (!result)
        {
            return BadRequest("Failed to update data"); // Returns a 400 Bad Request response with a message if the result is false.
        }
        return Ok("Data Updated"); // Returns a 200 OK response with a message if the update was successful.
    }

    [HttpDelete]
    // Declares a public method named Delete that takes a Guid parameter and returns an IActionResult.
    public IActionResult Delete(Guid guid)
    {
        var entity = _roomRepository.GetByGuid(guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }
        // Calls the Delete method of the _roomRepository field with the entity parameter and assigns the result to a local variable named result.
        var result = _roomRepository.Delete(entity);
        if (!result)
        {
            return BadRequest("Failed to delete data"); // Returns a 400 Bad Request response with a message if the result is false.
        }
        return Ok("Data Deleted"); // Returns a 200 OK response with a message if it is not false.
    }
}
