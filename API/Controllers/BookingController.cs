using API.Contracts;
using API.DTO.Bookings;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // This attribute indicates that the class is an API controller.
[Route("api/[controller]")] // This attribute specifies the route prefix for the controller
// Declares a new class named BookingController that inherits from ControllerBase.
public class BookingController : ControllerBase
{
    // Declares a private field of type IBookingRepository.
    private readonly IBookingRepository _bookingRepository;

    // Declares a public constructor that takes an IBookingRepository parameter.
    public BookingController(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    [HttpGet]
    // Declares a public method named GetAll that returns an IActionResult.
    public IActionResult GetAll()
    {
        // Calls the GetAll method of the _bookingRepository field and assigns the result to a local variable named result.
        var result = _bookingRepository.GetAll();
        if (!result.Any())
        {
            // Returns a 404 Not Found response with a message if the result is empty.
            return NotFound("Data Not Found");
        }
        // Converts each Booking object in the result to a BookingDto object and assigns the result to a local variable named data.
        var data = result.Select(x => (BookingDto)x);
        // Returns a 200 OK response with the data if it is not empty.
        return Ok(data);
    }

    [HttpGet("{guid}")]
    // Declares a public method named GetByGuid that takes a Guid parameter and returns an IActionResult.
    public IActionResult GetByGuid(Guid guid)
    {
        // Calls the GetByGuid method of the _bookingRepository field with the guid parameter and assigns the result to a local variable named result.
        var result = _bookingRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        // Returns a 200 OK response with the BookingDto object created from the result object.
        return Ok((BookingDto)result);
    }

    [HttpPost]
    // Declares a public method named Create that takes a Booking parameter and returns an IActionResult.
    public IActionResult Create(CreateBookingDto bookingDto)
    {
        // Calls the Create method of the _bookingRepository field with the bookingDto parameter and assigns the result to a local variable named result.
        var result = _bookingRepository.Create(bookingDto);
        if (result is null)
        {
            // Returns a 400 Bad Request response with a message if the result is null.
            return BadRequest("Failed to create data");
        }
        // Returns a 200 OK response with the UniversityDto object created from the result object.
        return Ok((BookingDto)result);
    }

    [HttpPut]
    // Declares a public method named Update that takes a BookingDto parameter and returns an IActionResult.
    public IActionResult Update(BookingDto bookingDto)
    {
        // Calls the GetByGuid method of the _bookingRepository field with the Guid property of the bookingDto parameter.
        // Assigns the result to a local variable named entity.
        var entity = _bookingRepository.GetByGuid(bookingDto.Guid);
        if (entity is null)
        {
            return NotFound("Id Not Found"); // Returns a 404 Not Found response with a message if the entity is null.
        }
        // Assigns the bookingDto parameter to a new Booking object named toUpdate.
        Booking toUpdate = bookingDto;
        // Assigns the CreatedDate property of the entity object to the CreatedDate property of the toUpdate object.
        toUpdate.CreatedDate = entity.CreatedDate;
        // Calls the Update method of the _bookingRepository field with the toUpdate object and assigns the result to a local variable named result.
        var result = _bookingRepository.Update(toUpdate);
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
        var entity = _bookingRepository.GetByGuid(guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }
        // Calls the Delete method of the _bookingRepository field with the entity parameter and assigns the result to a local variable named result.
        var result = _bookingRepository.Delete(entity);
        if (!result)
        {
            return BadRequest("Failed to delete data"); // Returns a 400 Bad Request response with a message if the result is false.
        }
        return Ok("Data Deleted"); // Returns a 200 OK response with a message if it is not false.
    }
}
