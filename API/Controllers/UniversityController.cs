using API.Contracts;
using API.DTO.Universities;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController] // This attribute indicates that the class is an API controller.
[Route("api/[controller]")] // This attribute specifies the route prefix for the controller.
// Declares a new class named UniversityController that inherits from ControllerBase.
public class UniversityController : ControllerBase
{
    // Declares a private field of type IUniversityRepository.
    private readonly IUniversityRepository _universityRepository;

    // Declares a public constructor that takes an IUniversityRepository parameter.
    public UniversityController(IUniversityRepository universityRepository)
    {
        _universityRepository = universityRepository;
    }

    [HttpGet] // This attribute specifies that this method should handle HTTP GET requests.
    // Declares a public method named GetAll that returns an IActionResult.
    public IActionResult GetAll()
    {
        // Calls the GetAll method of the _universityRepository field and assigns the result to a local variable named result.
        var result = _universityRepository.GetAll();
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
        // Converts each University object in the result to a UniversityDto object and assigns the result to a local variable named data.
        var data = result.Select(x => (UniversityDto)x);
        // Returns a 200 OK response with the data if it is not empty.
        return Ok(new ResponseOkHandler<IEnumerable<UniversityDto>>(data));
    }

    [HttpGet("{guid}")] // This attribute specifies that this method should handle HTTP GET requests with a GUID parameter in the URL.
    // Declares a public method named GetByGuid that takes a Guid parameter and returns an IActionResult.
    public IActionResult GetByGuid(Guid guid)
    {
        // Calls the GetByGuid method of the _universityRepository field with the guid parameter and assigns the result to a local variable named result.
        var result = _universityRepository.GetByGuid(guid);
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
        // Returns a 200 OK response with the UniversityDto object created from the result object.
        return Ok(new ResponseOkHandler<UniversityDto>((UniversityDto)result));
    }

    [HttpPost] // This attribute specifies that this method should handle HTTP POST requests.
    // Declares a public method named Create that takes a CreateUniversityDto parameter and returns an IActionResult.
    public IActionResult Create(CreateUniversityDto universityDto)
    {
        try
        {
            // Calls the Create method of the _universityRepository field with the universityDto parameter and assigns the result to a local variable named result.
            var result = _universityRepository.Create(universityDto);
            // Returns a 200 OK response with the UniversityDto object created from the result object.
            return Ok(new ResponseOkHandler<UniversityDto>((UniversityDto)result));
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

    [HttpPut] // This attribute specifies that this method should handle HTTP PUT requests.
    // Declares a public method named Update that takes a UniversityDto parameter and returns an IActionResult.
    public IActionResult Update(UniversityDto universityDto)
    {
        try
        {
            // Calls the GetByGuid method of the _universityRepository field with the Guid property of the universityDto parameter.
            // Assigns the result to a local variable named entity.
            var entity = _universityRepository.GetByGuid(universityDto.Guid);
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
            // Assigns the universityDto parameter to a new University object named toUpdate.
            University toUpdate = universityDto;
            // Assigns the CreatedDate property of the entity object to the CreatedDate property of the toUpdate object.
            toUpdate.CreatedDate = entity.CreatedDate;
            // Calls the Update method of the _universityRepository field with the toUpdate object.
            _universityRepository.Update(toUpdate);
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

    [HttpDelete] // This attribute specifies that this method should handle HTTP DELETE requests.
    // Declares a public method named Delete that takes a Guid parameter and returns an IActionResult.
    public IActionResult Delete(Guid guid)
    {
        try
        {
            // Calls the GetByGuid method of the _universityRepository field with the guid parameter and assigns the result to a local variable named entity.
            var entity = _universityRepository.GetByGuid(guid);
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
            // Calls the Delete method of the _universityRepository field with the entity object.
            _universityRepository.Delete(entity);
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
