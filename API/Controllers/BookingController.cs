using API.Contracts;
using API.DTO.Bookings;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController] // This attribute indicates that the class is an API controller.
[Route("api/[controller]")] // This attribute specifies the route prefix for the controller.
[Authorize] // Implement authorization.
// Declares a new class named BookingController that inherits from ControllerBase.
public class BookingController : ControllerBase
{
    // Declares a private field of type IBookingRepository, IEmployeeRepository and IRoomRepository.
    private readonly IBookingRepository _bookingRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IRoomRepository _roomRepository;

    // Declares a public constructor that takes an IBookingRepository, IEmployeeRepository and IRoomRepository parameter.
    public BookingController(IBookingRepository bookingRepository, IEmployeeRepository employeeRepository, IRoomRepository roomRepository)
    {
        _bookingRepository = bookingRepository;
        _employeeRepository = employeeRepository;
        _roomRepository = roomRepository;
    }

    [HttpGet("bookingDetails")]
    // Declares a public method named GetBookingDetails and returns an IActionResult.
    public IActionResult GetBookingDetails()
    {
        try
        {
            var bookings = _bookingRepository.GetAll();
            var employees = _employeeRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            // Check if any data bookings, employees and rooms or not
            if (!(bookings.Any() && employees.Any() && rooms.Any()))
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            // Join data bookings, employees and rooms to get booking details
            var bookingDetails = from b in bookings
                                 join e in employees on b.EmployeeGuid equals e.Guid
                                 join r in rooms on b.RoomGuid equals r.Guid
                                 select new BookingDetailDto
                                 {
                                     Guid = b.Guid,
                                     BookedNIK = e.Nik,
                                     BookedBy = string.Concat(e.FirstName, " ", e.LastName),
                                     RoomName = r.Name,
                                     StartDate = b.StartDate,
                                     EndDate = b.EndDate,
                                     Status = b.Status.ToString(),
                                     Remarks = b.Remarks
                                 };
            // Returns a 200 OK response with the BookingDetailDto object created from the result object.
            return Ok(new ResponseOkHandler<IEnumerable<BookingDetailDto>>(bookingDetails));
        }
        catch (ExceptionHandler ex)
        {
            // Returns a 500 Internal Server Error response with a new ResponseErrorHandler object.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to get details booking",
                Error = ex.Message
            });
        }
    }

    [HttpGet("bookingDetail/{bookingGuid}")]
    // Declares a public method named GetBookingDetailGuid that takes a Guid parameter and returns an IActionResult.
    public IActionResult GetBookingDetailGuid(Guid bookingGuid)
    {
        try
        {
            var bookings = _bookingRepository.GetAll();
            var employees = _employeeRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            // Check if any data bookings, employees, and rooms or not
            if (!(bookings.Any() && employees.Any() && rooms.Any()))
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            // Join data bookings, employees, and rooms to get booking detail by bookingGuid
            var bookingDetailByGuid = from b in bookings
                                      join e in employees on b.EmployeeGuid equals e.Guid
                                      join r in rooms on b.RoomGuid equals r.Guid
                                      where b.Guid == bookingGuid
                                      select new BookingDetailDto
                                      {
                                          Guid = bookingGuid,
                                          BookedNIK = e.Nik,
                                          BookedBy = string.Concat(e.FirstName, " ", e.LastName),
                                          RoomName = r.Name,
                                          StartDate = b.StartDate,
                                          EndDate = b.EndDate,
                                          Status = b.Status.ToString(),
                                          Remarks = b.Remarks
                                      };
            // Returns a 200 OK response with the BookingDetailDto object created from the result object.
            return Ok(new ResponseOkHandler<IEnumerable<BookingDetailDto>>(bookingDetailByGuid));
        }
        catch (ExceptionHandler ex)
        {
            // Returns a 500 Internal Server Error response with a new ResponseErrorHandler object.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to get detail booking",
                Error = ex.Message
            });
        }
    }

    [HttpGet("bookingLengthDetails")]
    // Declares a public method named GetBookingLengthDetails and returns an IActionResult.
    public IActionResult GetBookingLengthDetails()
    {
        try
        {
            var bookings = _bookingRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            // Check if any data bookings and rooms or not
            if (!(bookings.Any() && rooms.Any()))
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            // Join data bookings and rooms to get booking length details
            var bookingLengthDetails = from b in bookings
                                       join r in rooms on b.RoomGuid equals r.Guid
                                       select new BookingLengthDetailDto
                                       {
                                           RoomGuid = b.Guid,
                                           RoomName = r.Name,
                                           BookingLength = _bookingRepository.GetBookingLength(b.StartDate, b.EndDate)
                                       };
            // Returns a 200 OK response with the BookingLengthDetailDto object created from the result object.
            return Ok(new ResponseOkHandler<IEnumerable<BookingLengthDetailDto>>(bookingLengthDetails));
        }
        catch (ExceptionHandler ex)
        {
            // Returns a 500 Internal Server Error response with a new ResponseErrorHandler object.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to get length details booking",
                Error = ex.Message
            });
        }
    }

    [HttpGet]
    // Declares a public method named GetAll that returns an IActionResult.
    public IActionResult GetAll()
    {
        // Calls the GetAll method of the _bookingRepository field and assigns the result to a local variable named result.
        var result = _bookingRepository.GetAll();
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
        // Converts each Booking object in the result to a BookingDto object and assigns the result to a local variable named data.
        var data = result.Select(x => (BookingDto)x);
        // Returns a 200 OK response with the data if it is not empty.
        return Ok(new ResponseOkHandler<IEnumerable<BookingDto>>(data));
    }

    [HttpGet("{guid}")]
    // Declares a public method named GetByGuid that takes a Guid parameter and returns an IActionResult.
    public IActionResult GetByGuid(Guid guid)
    {
        // Calls the GetByGuid method of the _bookingRepository field with the guid parameter and assigns the result to a local variable named result.
        var result = _bookingRepository.GetByGuid(guid);
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
        // Returns a 200 OK response with the BookingDto object created from the result object.
        return Ok(new ResponseOkHandler<BookingDto>((BookingDto)result));
    }

    [HttpPost]
    // Declares a public method named Create that takes a Booking parameter and returns an IActionResult.
    public IActionResult Create(CreateBookingDto bookingDto)
    {
        try
        {
            // Calls the Create method of the _bookingRepository field with the bookingDto parameter and assigns the result to a local variable named result.
            var result = _bookingRepository.Create(bookingDto);
            // Returns a 200 OK response with the BookingDto object created from the result object.
            return Ok(new ResponseOkHandler<BookingDto>((BookingDto)result));
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
    // Declares a public method named Update that takes a BookingDto parameter and returns an IActionResult.
    public IActionResult Update(BookingDto bookingDto)
    {
        try
        {
            // Calls the GetByGuid method of the _bookingRepository field with the Guid property of the bookingDto parameter.
            // Assigns the result to a local variable named entity.
            var entity = _bookingRepository.GetByGuid(bookingDto.Guid);
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
            // Assigns the bookingDto parameter to a new Booking object named toUpdate.
            Booking toUpdate = bookingDto;
            // Assigns the CreatedDate property of the entity object to the CreatedDate property of the toUpdate object.
            toUpdate.CreatedDate = entity.CreatedDate;
            // Calls the Update method of the _bookingRepository field with the toUpdate object.
            _bookingRepository.Update(toUpdate);
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
            var entity = _bookingRepository.GetByGuid(guid);
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
            // Calls the Delete method of the _bookingRepository field with the entity parameter.
            _bookingRepository.Delete(entity);
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
