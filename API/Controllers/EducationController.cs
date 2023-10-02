using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // This attribute indicates that the class is an API controller.
[Route("api/[controller]")] // This attribute specifies the route prefix for the controller
// Declares a new class named EducationController that inherits from ControllerBase.
public class EducationController : ControllerBase
{
    // Declares a private field of type IEducationRepository.
    private readonly IEducationRepository _educationRepository;

    // Declares a public constructor that takes an IEducationRepository parameter.
    public EducationController(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }

    [HttpGet]
    // Declares a public method named GetAll that returns an IActionResult.
    public IActionResult GetAll()
    {
        // Calls the GetAll method of the _educationRepository field and assigns the result to a local variable named result.
        var result = _educationRepository.GetAll();
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
        // Calls the GetByGuid method of the _educationRepository field with the guid parameter and assigns the result to a local variable named result.
        var result = _educationRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found"); // Returns a 404 Not Found response with a message if the result is empty.
        }
        return Ok(result); // Returns a 200 OK response with the result if it is not empty.
    }

    [HttpPost]
    // Declares a public method named Create that takes an Education parameter and returns an IActionResult.
    public IActionResult Create(Education education)
    {
        // Calls the Create method of the _educationRepository field with the education parameter and assigns the result to a local variable named result.
        var result = _educationRepository.Create(education);
        if (result is null)
        {
            return BadRequest("Failed to create data"); // Returns a 400 Bad Request response with a message if the result is null.
        }
        return Ok(result); // Returns a 200 OK response with the result if it is not null.
    }

    [HttpPut]
    // Declares a public method named Update that takes an Education parameter and returns an IActionResult.
    public IActionResult Update(Education education)
    {
        // Calls the Update method of the _educationRepository field with the education parameter and assigns the result to a local variable named result.
        var result = _educationRepository.Update(education);
        if (!result)
        {
            return BadRequest("Failed to update data"); // Returns a 400 Bad Request response with a message if the result is false.
        }
        return Ok("Data Updated"); // Returns a 200 OK response with a message if it is not false.
    }

    [HttpDelete]
    // Declares a public method named Delete that takes a Guid parameter and returns an IActionResult.
    public IActionResult Delete(Guid guid)
    {
        var entity = _educationRepository.GetByGuid(guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }
        // Calls the Delete method of the _educationRepository field with the entity parameter and assigns the result to a local variable named result.
        var result = _educationRepository.Delete(entity);
        if (!result)
        {
            return BadRequest("Failed to delete data"); // Returns a 400 Bad Request response with a message if the result is false.
        }
        return Ok("Data Deleted"); // Returns a 200 OK response with a message if it is not false.
    }
}
