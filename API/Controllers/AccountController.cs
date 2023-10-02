using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // This attribute indicates that the class is an API controller.
[Route("api/[controller]")] // This attribute specifies the route prefix for the controller
// Declares a new class named AccountController that inherits from ControllerBase.
public class AccountController : ControllerBase
{
    // Declares a private field of type IAccountRepository.
    private readonly IAccountRepository _accountRepository;

    // Declares a public constructor that takes an IAccountRepository parameter.
    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    // This attribute specifies that this method should handle HTTP GET requests.
    [HttpGet]
    // Declares a public method named GetAll that returns an IActionResult.
    public IActionResult GetAll()
    {
        // Calls the GetAll method of the _accountRepository field and assigns the result to a local variable named result.
        var result = _accountRepository.GetAll();
        if (!result.Any())
        {
            // Returns a 404 Not Found response with a message if the result is empty.
            return NotFound("Data Not Found");
        }
        // Returns a 200 OK response with the result if it is not empty.
        return Ok(result);
    }

    // This attribute specifies that this method should handle HTTP GET requests with a GUID parameter in the URL.
    [HttpGet("{guid}")]
    // Declares a public method named GetByGuid that takes a Guid parameter and returns an IActionResult.
    public IActionResult GetByGuid(Guid guid)
    {
        // Calls the GetByGuid method of the _accountRepository field with the guid parameter and assigns the result to a local variable named result.
        var result = _accountRepository.GetByGuid(guid);
        if (result is null)
        {
            // Returns a 404 Not Found response with a message if the result is empty.
            return NotFound("Id Not Found");
        }
        // Returns a 200 OK response with the result if it is not empty.
        return Ok(result);
    }

    // This attribute specifies that this method should handle HTTP POST requests.
    [HttpPost]
    // Declares a public method named Create that takes an Account parameter and returns an IActionResult.
    public IActionResult Create(Account account)
    {
        // Calls the Create method of the _accountRepository field with the account parameter and assigns the result to a local variable named result.
        var result = _accountRepository.Create(account);
        if (result is null)
        {
            // Returns a 400 Bad Request response with a message if the result is null.
            return BadRequest("Failed to create data");
        }
        // Returns a 200 OK response with the result if it is not null.
        return Ok(result);
    }

    // This attribute specifies that this method should handle HTTP PUT requests.
    [HttpPut]
    // Declares a public method named Update that takes an Account parameter and returns an IActionResult.
    public IActionResult Update(Account account)
    {
        // Calls the Update method of the _accountRepository field with the account parameter and assigns the result to a local variable named result.
        var result = _accountRepository.Update(account);
        if (!result)
        {
            // Returns a 400 Bad Request response with a message if the result is false.
            return BadRequest("Failed to update data");
        }
        // Returns a 200 OK response with a message if it is not false.
        return Ok("Data Updated");
    }

    // This attribute specifies that this method should handle HTTP DELETE requests.
    [HttpDelete]
    // Declares a public method named Delete that takes a Guid parameter and returns an IActionResult.
    public IActionResult Delete(Guid guid)
    {
        var entity = _accountRepository.GetByGuid(guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }
        // Calls the Delete method of the _accountRepository field with the entity parameter and assigns the result to a local variable named result.
        var result = _accountRepository.Delete(entity);
        if (!result)
        {
            return BadRequest("Failed to delete data"); // Returns a 400 Bad Request response with a message if the result is false.
        }
        return Ok("Data Deleted"); // Returns a 200 OK response with a message if it is not false.
    }
}
