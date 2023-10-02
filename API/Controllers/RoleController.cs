using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // This attribute indicates that the class is an API controller.
[Route("api/[controller]")] // This attribute specifies the route prefix for the controller
// Declares a new class named RoleController that inherits from ControllerBase.
public class RoleController : ControllerBase
{
    // Declares a private field of type IRoleRepository.
    private readonly IRoleRepository _roleRepository;

    // Declares a public constructor that takes an IRoleRepository parameter.
    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet]
    // Declares a public method named GetAll that returns an IActionResult.
    public IActionResult GetAll()
    {
        // Calls the GetAll method of the _roleRepository field and assigns the result to a local variable named result.
        var result = _roleRepository.GetAll();
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
        // Calls the GetByGuid method of the _roleRepository field with the guid parameter and assigns the result to a local variable named result.
        var result = _roleRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok(result);
    }

    [HttpPost]
    // Declares a public method named Create that takes a Role parameter and returns an IActionResult.
    public IActionResult Create(Role role)
    {
        // Calls the Create method of the _roleRepository field with the role parameter and assigns the result to a local variable named result.
        var result = _roleRepository.Create(role);
        if (result is null)
        {
            return BadRequest("Failed to create data"); // Returns a 400 Bad Request response with a message if the result is null.
        }
        return Ok(result); // Returns a 200 OK response with the result if it is not null.
    }

    [HttpPut]
    // Declares a public method named Update that takes a Role parameter and returns an IActionResult.
    public IActionResult Update(Role role)
    {
        // Calls the Update method of the _roleRepository field with the role parameter and assigns the result to a local variable named result.
        var result = _roleRepository.Update(role);
        if (!result)
        {
            return BadRequest("Failed to update data");
        }
        return Ok("Data Updated");
    }

    [HttpDelete]
    // Declares a public method named Delete that takes a Guid parameter and returns an IActionResult.
    public IActionResult Delete(Guid guid)
    {
        var entity = _roleRepository.GetByGuid(guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }
        // Calls the Delete method of the _roleRepository field with the entity parameter and assigns the result to a local variable named result.
        var result = _roleRepository.Delete(entity);
        if (!result)
        {
            return BadRequest("Failed to delete data"); // Returns a 400 Bad Request response with a message if the result is false.
        }
        return Ok("Data Deleted"); // Returns a 200 OK response with a message if it is not false.
    }
}
