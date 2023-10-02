using API.Contracts;
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
        return Ok(result); // Returns a 200 OK response with the result if it is not empty.
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
        return Ok(result);
    }

    [HttpPost]
    // Declares a public method named Create that takes a Room parameter and returns an IActionResult.
    public IActionResult Create(Room room)
    {
        // Calls the Create method of the _roomRepository field with the room parameter and assigns the result to a local variable named result.
        var result = _roomRepository.Create(room);
        if (result is null)
        {
            return BadRequest("Failed to create data"); // Returns a 400 Bad Request response with a message if the result is null.
        }
        return Ok(result); // Returns a 200 OK response with the result if it is not null.
    }

    [HttpPut]
    // Declares a public method named Update that takes a Room parameter and returns an IActionResult.
    public IActionResult Update(Room room)
    {
        // Calls the Update method of the _roomRepository field with the room parameter and assigns the result to a local variable named result.
        var result = _roomRepository.Update(room);
        if (result is false)
        {
            return BadRequest("Failed to update data");
        }
        return Ok(result);
    }

    [HttpDelete]
    // Declares a public method named Delete that takes a Room parameter and returns an IActionResult.
    public IActionResult Delete(Room room)
    {
        // Calls the Delete method of the _roomRepository field with the room parameter and assigns the result to a local variable named result.
        var result = _roomRepository.Delete(room);
        if (result is false)
        {
            return BadRequest("Failed to delete data"); // Returns a 400 Bad Request response with a message if the result is false.
        }
        return Ok(result); // Returns a 200 OK response with the result if it is not false.
    }
}
