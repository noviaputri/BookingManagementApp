using API.Contracts;
using API.DTO.Accounts;
using API.Models;
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

    [HttpGet] // This attribute specifies that this method should handle HTTP GET requests.
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
        // Converts each Account object in the result to a AccountDto object and assigns the result to a local variable named data.
        var data = result.Select(x => (AccountDto)x);
        // Returns a 200 OK response with the data if it is not empty.
        return Ok(data);
    }

    [HttpGet("{guid}")] // This attribute specifies that this method should handle HTTP GET requests with a GUID parameter in the URL.
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
        // Returns a 200 OK response with the AccountDto object created from the result object.
        return Ok((AccountDto)result);
    }

    [HttpPost] // This attribute specifies that this method should handle HTTP POST requests.
    // Declares a public method named Create that takes an CreateAccountDto parameter and returns an IActionResult.
    public IActionResult Create(CreateAccountDto accountDto)
    {
        // Calls the Create method of the _accountRepository field with the accountDto parameter and assigns the result to a local variable named result.
        var result = _accountRepository.Create(accountDto);
        if (result is null)
        {
            // Returns a 400 Bad Request response with a message if the result is null.
            return BadRequest("Failed to create data");
        }
        // Returns a 200 OK response with the AccountDto object created from the result object.
        return Ok((AccountDto)result);
    }

    [HttpPut] // This attribute specifies that this method should handle HTTP PUT requests.
    // Declares a public method named Update that takes an AccountDto parameter and returns an IActionResult.
    public IActionResult Update(UpdateAccountDto accountDto)
    {
        // Calls the GetByGuid method of the _accountRepository field with the Guid property of the accountDto parameter.
        // Assigns the result to a local variable named entity.
        var entity = _accountRepository.GetByGuid(accountDto.Guid);
        if (entity is null)
        {
            return NotFound("Id Not Found"); // Returns a 404 Not Found response with a message if the entity is null.
        }
        // Assigns the accountDto parameter to a new Account object named toUpdate.
        Account toUpdate = accountDto;
        // Assigns the CreatedDate property of the entity object to the CreatedDate property of the toUpdate object.
        toUpdate.CreatedDate = entity.CreatedDate;
        // Calls the Update method of the _accountRepository field with the toUpdate object and assigns the result to a local variable named result.
        var result = _accountRepository.Update(toUpdate);
        if (!result)
        {
            return BadRequest("Failed to update data"); // Returns a 400 Bad Request response with a message if the result is false.
        }
        return Ok("Data Updated"); // Returns a 200 OK response with a message if the update was successful.
    }

    [HttpDelete] // This attribute specifies that this method should handle HTTP DELETE requests.
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
