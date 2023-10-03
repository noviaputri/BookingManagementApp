using API.Contracts;
using API.DTO.AccountRoles;
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
        // Converts each AccountRole object in the result to a AccountRoleDto object and assigns the result to a local variable named data.
        var data = result.Select(x => (AccountRoleDto)x);
        // Returns a 200 OK response with the data if it is not empty.
        return Ok(data);
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
        // Returns a 200 OK response with the AccountRoleDto object created from the result object.
        return Ok((AccountRoleDto)result);
    }

    [HttpPost] // This attribute specifies that this method should handle HTTP POST requests.
    // Declares a public method named Create that takes an CreateAccountRoleDto parameter and returns an IActionResult.
    public IActionResult Create(CreateAccountRoleDto accountRoleDto)
    {
        // Calls the Create method of the _accountRoleRepository field with the accountRole parameter and assigns the result to a local variable named result.
        var result = _accountRoleRepository.Create(accountRoleDto);
        if (result is null)
        {
            // Returns a 400 Bad Request response with a message if the result is null.
            return BadRequest("Failed to create data");
        }
        // Returns a 200 OK response with the AccountRoleDto object created from the result object.
        return Ok((AccountRoleDto)result);
    }

    [HttpPut] // This attribute specifies that this method should handle HTTP PUT requests.
    // Declares a public method named Update that takes an AccountRoleDto parameter and returns an IActionResult.
    public IActionResult Update(AccountRoleDto accountRoleDto)
    {
        // Calls the GetByGuid method of the _accountRoleRepository field with the Guid property of the accountRoleDto parameter.
        // Assigns the result to a local variable named entity.
        var entity = _accountRoleRepository.GetByGuid(accountRoleDto.Guid);
        if (entity is null)
        {
            return NotFound("Id Not Found"); // Returns a 404 Not Found response with a message if the entity is null.
        }
        // Assigns the accountRoleDto parameter to a new AccountRole object named toUpdate.
        AccountRole toUpdate = accountRoleDto;
        // Assigns the CreatedDate property of the entity object to the CreatedDate property of the toUpdate object.
        toUpdate.CreatedDate = entity.CreatedDate;
        // Calls the Update method of the _accountRoleRepository field with the toUpdate object and assigns the result to a local variable named result.
        var result = _accountRoleRepository.Update(toUpdate);
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
        var entity = _accountRoleRepository.GetByGuid(guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }
        // Calls the Delete method of the _accountRoleRepository field with the entity parameter and assigns the result to a local variable named result.
        var result = _accountRoleRepository.Delete(entity);
        if (!result)
        {
            return BadRequest("Failed to delete data"); // Returns a 400 Bad Request response with a message if the result is false.
        }
        return Ok("Data Deleted"); // Returns a 200 OK response with a message if it is not false.
    }
}
