using API.Contracts;
using API.DTO.Rooms;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            // Returns a 404 Not Found response with code, status and message if the result is empty.
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        // Converts each Room object in the result to a RoomDto object and assigns the result to a local variable named data.
        var data = result.Select(x => (RoomDto)x);
        // Returns a 200 OK response with the data if it is not empty.
        return Ok(new ResponseOkHandler<IEnumerable<RoomDto>>(data));
    }

    [HttpGet("{guid}")]
    // Declares a public method named GetByGuid that takes a Guid parameter and returns an IActionResult.
    public IActionResult GetByGuid(Guid guid)
    {
        // Calls the GetByGuid method of the _roomRepository field with the guid parameter and assigns the result to a local variable named result.
        var result = _roomRepository.GetByGuid(guid);
        if (result is null)
        {
            // Returns a 404 Not Found response with code, status and message if the result is empty.
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        // Returns a 200 OK response with the RoomDto object created from the result object.
        return Ok(new ResponseOkHandler<RoomDto>((RoomDto)result));
    }

    [HttpPost]
    // Declares a public method named Create that takes a CreateRoomDto parameter and returns an IActionResult.
    public IActionResult Create(CreateRoomDto roomDto)
    {
        try
        {
            // Calls the Create method of the _roomRepository field with the roomDto parameter and assigns the result to a local variable named result.
            var result = _roomRepository.Create(roomDto);
            // Returns a 200 OK response with the RoomDto object created from the result object.
            return Ok(new ResponseOkHandler<RoomDto>((RoomDto)result));
        }
        catch (ExceptionHandler ex)
        {
            // Returns a 500 Internal Server Error response with a new ResponseErrorHandler object.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to create data",
                Error = ex.Message
            });
        }
    }

    [HttpPut]
    // Declares a public method named Update that takes a RoomDto parameter and returns an IActionResult.
    public IActionResult Update(RoomDto roomDto)
    {
        try
        {
            // Calls the GetByGuid method of the _roomRepository field with the Guid property of the roomDto parameter.
            // Assigns the result to a local variable named entity.
            var entity = _roomRepository.GetByGuid(roomDto.Guid);
            if (entity is null)
            {
                // Returns a 404 Not Found response with a new ResponseErrorHandler object.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            // Assigns the roomDto parameter to a new Room object named toUpdate.
            Room toUpdate = roomDto;
            // Assigns the CreatedDate property of the entity object to the CreatedDate property of the toUpdate object.
            toUpdate.CreatedDate = entity.CreatedDate;
            // Calls the Update method of the _roomRepository field with the toUpdate object.
            _roomRepository.Update(toUpdate);
            // Returns a 200 OK response if it is not false.
            return Ok(new ResponseOkHandler<string>("Data Updated"));
        }
        catch (ExceptionHandler ex)
        {
            // Returns a 500 Internal Server Error response with a new ResponseErrorHandler object.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to update data",
                Error = ex.Message
            });
        }
    }

    [HttpDelete]
    // Declares a public method named Delete that takes a Guid parameter and returns an IActionResult.
    public IActionResult Delete(Guid guid)
    {
        try
        {
            var entity = _roomRepository.GetByGuid(guid);
            if (entity is null)
            {
                // Returns a 404 Not Found response with a new ResponseErrorHandler object.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            // Calls the Delete method of the _roomRepository field with the entity parameter.
            _roomRepository.Delete(entity);
            // Returns a 200 OK response with a message if it is not null.
            return Ok(new ResponseOkHandler<string>("Data Deleted"));
        }
        catch (ExceptionHandler ex)
        {
            // Returns a 500 Internal Server Error response with a new ResponseErrorHandler object.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to delete data",
                Error = ex.Message
            });
        }
    }
}
