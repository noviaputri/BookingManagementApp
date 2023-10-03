using API.Contracts;
using API.DTO.AccountRoles;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            // Returns a 404 Not Found response with code, status and message if the result is empty.
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        // Converts each AccountRole object in the result to a AccountRoleDto object and assigns the result to a local variable named data.
        var data = result.Select(x => (AccountRoleDto)x);
        // Returns a 200 OK response with the data if it is not empty.
        return Ok(new ResponseOkHandler<IEnumerable<AccountRoleDto>>(data));
    }

    [HttpGet("{guid}")] // This attribute specifies that this method should handle HTTP GET requests with a GUID parameter in the URL.
    // Declares a public method named GetByGuid that takes a Guid parameter and returns an IActionResult.
    public IActionResult GetByGuid(Guid guid)
    {
        // Calls the GetByGuid method of the _accountRoleRepository field with the guid parameter and assigns the result to a local variable named result.
        var result = _accountRoleRepository.GetByGuid(guid);
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
        // Returns a 200 OK response with the AccountRoleDto object created from the result object.
        return Ok(new ResponseOkHandler<AccountRoleDto>((AccountRoleDto)result));
    }

    [HttpPost] // This attribute specifies that this method should handle HTTP POST requests.
    // Declares a public method named Create that takes an CreateAccountRoleDto parameter and returns an IActionResult.
    public IActionResult Create(CreateAccountRoleDto accountRoleDto)
    {
        try
        {
            // Calls the Create method of the _accountRoleRepository field with the accountRole parameter and assigns the result to a local variable named result.
            var result = _accountRoleRepository.Create(accountRoleDto);
            // Returns a 200 OK response with the AccountRoleDto object created from the result object.
            return Ok(new ResponseOkHandler<AccountRoleDto>((AccountRoleDto)result));
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
    // Declares a public method named Update that takes an AccountRoleDto parameter and returns an IActionResult.
    public IActionResult Update(AccountRoleDto accountRoleDto)
    {
        try
        {
            // Calls the GetByGuid method of the _accountRoleRepository field with the Guid property of the accountRoleDto parameter.
            // Assigns the result to a local variable named entity.
            var entity = _accountRoleRepository.GetByGuid(accountRoleDto.Guid);
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
            // Assigns the accountRoleDto parameter to a new AccountRole object named toUpdate.
            AccountRole toUpdate = accountRoleDto;
            // Assigns the CreatedDate property of the entity object to the CreatedDate property of the toUpdate object.
            toUpdate.CreatedDate = entity.CreatedDate;
            // Calls the Update method of the _accountRoleRepository field with the toUpdate object and assigns the result to a local variable named result.
            _accountRoleRepository.Update(toUpdate);
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

    [HttpDelete] 
    // Declares a public method named Delete that takes a Guid parameter and returns an IActionResult.
    public IActionResult Delete(Guid guid)
    {
        try
        {
            var entity = _accountRoleRepository.GetByGuid(guid);
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
            // Calls the Delete method of the _accountRoleRepository field with the entity parameter and assigns the result to a local variable named result.
            _accountRoleRepository.Delete(entity);
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
