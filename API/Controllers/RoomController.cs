using API.Contracts;
using API.DTO.Rooms;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController] // This attribute indicates that the class is an API controller.
[Route("api/[controller]")] // This attribute specifies the route prefix for the controller
[Authorize] // Implement authorization
// Declares a new class named RoomController that inherits from ControllerBase.
public class RoomController : ControllerBase
{
    // Declares a private field of type IRoomRepository.
    private readonly IRoomRepository _roomRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IEmployeeRepository _employeeRepository;

    // Declares a public constructor that takes an IRoomRepository parameter.
    public RoomController(IRoomRepository roomRepository, IBookingRepository bookingRepository, IEmployeeRepository employeeRepository)
    {
        _roomRepository = roomRepository;
        _bookingRepository = bookingRepository;
        _employeeRepository = employeeRepository;
    }

    [HttpGet("roomsUsed")]
    public IActionResult GetRoomsUsed()
    {
        // Check if any room, booking, and employee or not
        var rooms = _roomRepository.GetAll();
        var bookings = _bookingRepository.GetAll();
        var employees = _employeeRepository.GetAll();
        if (!(rooms.Any() && bookings.Any() && employees.Any()))
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        // Join data rooms, bookings, and employees to get roomsUseToday details
        var roomsUseToday = from r in rooms
                            join b in bookings on r.Guid equals b.RoomGuid
                            join e in employees on b.EmployeeGuid equals e.Guid
                            where (b.StartDate <= DateTime.Today) && (b.EndDate > DateTime.Today)
                            select new RoomUseTodayDto
                            {
                                BookingGuid = b.Guid,
                                RoomName = r.Name,
                                Status = b.Status.ToString(),
                                Floor = r.Floor,
                                BookBy = string.Concat(e.FirstName, " ", e.LastName)
                            };
        if(!roomsUseToday.Any())
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Room Not Found"
            });
        }
        return Ok(new ResponseOkHandler<IEnumerable<RoomUseTodayDto>>(roomsUseToday));
    }

    [HttpGet("roomsNotUsed")]
    public IActionResult GetRoomsNotUsed()
    {
        // Check if any room and booking or not
        var rooms = _roomRepository.GetAll();
        var bookings = _bookingRepository.GetAll();
        if (!(rooms.Any() && bookings.Any()))
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        // Join data rooms and bookings to get roomsNotUseToday details
        var roomsNotUseToday = from r in rooms
                               join b in bookings on r.Guid equals b.RoomGuid
                               where (b.StartDate > DateTime.Today) || (b.EndDate <= DateTime.Today)
                               select new RoomNotUseTodayDto
                               {
                                   RoomGuid = r.Guid,
                                   RoomName = r.Name,
                                   Floor = r.Floor,
                                   Capacity = r.Capacity
                               };
        if (!roomsNotUseToday.Any())
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Room Not Found"
            });
        }
        return Ok(new ResponseOkHandler<IEnumerable<RoomNotUseTodayDto>>(roomsNotUseToday));
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
