using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    // Handling GetAll requests to retrieves all universities from the repository
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _universityRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }
        return Ok(result);
    }

    // Handling GetById requests to retrieves a university by its guid
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _universityRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok(result);
    }

    // Handling CREATE requests to creates a new university in the repository
    [HttpPost]
    public IActionResult Create(University university)
    {
        var result = _universityRepository.Create(university);
        if (result is null)
        {
            return BadRequest("Failed to create data");
        }
        return Ok(result);
    }

    // Handling UPDATE requests to updates an existing university in the repository
    [HttpPut]
    public IActionResult Update(University university)
    {
        var result = _universityRepository.Update(university);
        if(result is false)
        {
            return BadRequest("Failed to update data");
        }
        return Ok(result);
    }

    // Handling DELETE requests to delete an existing university from the repository
    [HttpDelete]
    public IActionResult Delete(University university)
    {
        var result = _universityRepository.Delete(university);
        if (result is false)
        {
            return BadRequest("Failed to delete data");
        }
        return Ok(result);
    }
}
