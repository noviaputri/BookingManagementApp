using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // This attribute indicates that the class is an API controller.
[Route("api/[controller]")] // This attribute specifies the route prefix for the controller
// Declares a new class named AccountRoleController that inherits from ControllerBase.
public class AccountRoleController : ControllerBase
{
    // Declares a private field of type IAccountRoleRepository.
    private readonly IAccountRoleRepository _accountRoleRepository;

    // Declares a public constructor that takes an IAccountRoleRepository parameter.
    public AccountRoleController(IAccountRoleRepository accountRoleRepository)
    {
        _accountRoleRepository = accountRoleRepository;
    }

    [HttpGet] // This attribute specifies that this method should handle HTTP GET requests.
    // Declares a public method named GetAll that returns an IActionResult.
    public IActionResult GetAll()
    {
        // Calls the GetAll method of the _accountRoleRepository field and assigns the result to a local variable named result.
        var result = _accountRoleRepository.GetAll();
        if (!result.Any())
        {
            // Returns a 404 Not Found response with a message if the result is empty.
            return NotFound("Data Not Found");
        }
        // Returns a 200 OK response with the result if it is not empty.
        return Ok(result);
    }

    [HttpGet("{guid}")] // This attribute specifies that this method should handle HTTP GET requests with a GUID parameter in the URL.
    // Declares a public method named GetByGuid that takes a Guid parameter and returns an IActionResult.
    public IActionResult GetByGuid(Guid guid)
    {
        // Calls the GetByGuid method of the _accountRoleRepository field with the guid parameter and assigns the result to a local variable named result.
        var result = _accountRoleRepository.GetByGuid(guid);
        if (result is null)
        {
            // Returns a 404 Not Found response with a message if the result is empty.
            return NotFound("Id Not Found");
        }
        // Returns a 200 OK response with the result if it is not empty.
        return Ok(result);
    }

    [HttpPost] // This attribute specifies that this method should handle HTTP POST requests.
    // Declares a public method named Create that takes an AccountRole parameter and returns an IActionResult.
    public IActionResult Create(AccountRole accountRole)
    {
        // Calls the Create method of the _accountRoleRepository field with the accountRole parameter and assigns the result to a local variable named result.
        var result = _accountRoleRepository.Create(accountRole);
        if (result is null)
        {
            // Returns a 400 Bad Request response with a message if the result is null.
            return BadRequest("Failed to create data");
        }
        // Returns a 200 OK response with the result if it is not null.
        return Ok(result);
    }

    [HttpPut] // This attribute specifies that this method should handle HTTP PUT requests.
    // Declares a public method named Update that takes an AccountRole parameter and returns an IActionResult.
    public IActionResult Update(AccountRole accountRole)
    {
        // Calls the Update method of the _accountRoleRepository field with the accountRole parameter and assigns the result to a local variable named result.
        var result = _accountRoleRepository.Update(accountRole);
        if (result is false)
        {
            // Returns a 400 Bad Request response with a message if the result is false.
            return BadRequest("Failed to update data");
        }
        // Returns a 200 OK response with the result if it is not false.
        return Ok(result);
    }

    [HttpDelete] // This attribute specifies that this method should handle HTTP DELETE requests.
    public IActionResult Delete(AccountRole accountRole)
    {
        // Calls the Delete method of the _accountRoleRepository field with the accountRole parameter and assigns the result to a local variable named result.
        var result = _accountRoleRepository.Delete(accountRole);
        if (result is false)
        {
            // Returns a 400 Bad Request response with a message if the result is false.
            return BadRequest("Failed to delete data");
        }
        // Returns a 200 OK response with the result if it is not false.
        return Ok(result);
    }
}
