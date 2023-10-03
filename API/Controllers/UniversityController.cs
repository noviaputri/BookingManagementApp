using API.Contracts;
using API.DTO.Universities;
using API.Models;
using Microsoft.AspNetCore.Mvc;

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
            return NotFound("Data Not Found"); // Returns a 404 Not Found response with a message if the result is empty.
        }
        // Converts each University object in the result to a UniversityDto object and assigns the result to a local variable named data.
        var data = result.Select(x => (UniversityDto)x);
        // Returns a 200 OK response with the data if it is not empty.
        return Ok(data);
    }

    [HttpGet("{guid}")] // This attribute specifies that this method should handle HTTP GET requests with a GUID parameter in the URL.
    // Declares a public method named GetByGuid that takes a Guid parameter and returns an IActionResult.
    public IActionResult GetByGuid(Guid guid)
    {
        // Calls the GetByGuid method of the _universityRepository field with the guid parameter and assigns the result to a local variable named result.
        var result = _universityRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found"); // Returns a 404 Not Found response with a message if the result is null.
        }
        // Returns a 200 OK response with the UniversityDto object created from the result object.
        return Ok((UniversityDto)result);
    }

    [HttpPost] // This attribute specifies that this method should handle HTTP POST requests.
    // Declares a public method named Create that takes a CreateUniversityDto parameter and returns an IActionResult.
    public IActionResult Create(CreateUniversityDto universityDto)
    {
        // Calls the Create method of the _universityRepository field with the universityDto parameter and assigns the result to a local variable named result.
        var result = _universityRepository.Create(universityDto);
        if (result is null)
        {
            return BadRequest("Failed to create data"); // Returns a 400 Bad Request response with a message if the result is null.
        }
        // Returns a 200 OK response with the UniversityDto object created from the result object.
        return Ok((UniversityDto)result);
    }

    [HttpPut] // This attribute specifies that this method should handle HTTP PUT requests.
    // Declares a public method named Update that takes a UniversityDto parameter and returns an IActionResult.
    public IActionResult Update(UniversityDto universityDto)
    {
        // Calls the GetByGuid method of the _universityRepository field with the Guid property of the universityDto parameter.
        // Assigns the result to a local variable named entity.
        var entity = _universityRepository.GetByGuid(universityDto.Guid);
        if (entity is null)
        {
            return NotFound("Id Not Found"); // Returns a 404 Not Found response with a message if the entity is null.
        }
        // Assigns the universityDto parameter to a new University object named toUpdate.
        University toUpdate = universityDto; 
        // Assigns the CreatedDate property of the entity object to the CreatedDate property of the toUpdate object.
        toUpdate.CreatedDate = entity.CreatedDate; 
        // Calls the Update method of the _universityRepository field with the toUpdate object and assigns the result to a local variable named result.
        var result = _universityRepository.Update(toUpdate); 
        if(!result)
        {
            return BadRequest("Failed to update data"); // Returns a 400 Bad Request response with a message if the result is false.
        }
        return Ok("Data Updated"); // Returns a 200 OK response with a message if the update was successful.
    }

    [HttpDelete] // This attribute specifies that this method should handle HTTP DELETE requests.
    // Declares a public method named Delete that takes a Guid parameter and returns an IActionResult.
    public IActionResult Delete(Guid guid)
    {
        // Calls the GetByGuid method of the _universityRepository field with the guid parameter and assigns the result to a local variable named entity.
        var entity = _universityRepository.GetByGuid(guid);
        if (entity is null)
        {
            return NotFound("Id Not Found"); // Returns a 404 Not Found response with a message if the entity is null.
        }
        // Calls the Delete method of the _universityRepository field with the entity object and assigns the result to a local variable named result.
        var result = _universityRepository.Delete(entity);
        if (!result)
        {
            return BadRequest("Failed to delete data"); // Returns a 400 Bad Request response with a message if the result is false.
        }
        return Ok("Data Deleted"); // Returns a 200 OK response with a message if the deletion was successful.
    }
}
