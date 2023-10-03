using API.Contracts;
using API.DTO.Accounts;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            // Returns a 404 Not Found response with code, status and message if the result is empty.
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        // Converts each Account object in the result to a AccountDto object and assigns the result to a local variable named data.
        var data = result.Select(x => (AccountDto)x);
        // Returns a 200 OK response with the data if it is not empty.
        return Ok(new ResponseOkHandler<IEnumerable<AccountDto>>(data));
    }

    [HttpGet("{guid}")] // This attribute specifies that this method should handle HTTP GET requests with a GUID parameter in the URL.
    // Declares a public method named GetByGuid that takes a Guid parameter and returns an IActionResult.
    public IActionResult GetByGuid(Guid guid)
    {
        // Calls the GetByGuid method of the _accountRepository field with the guid parameter and assigns the result to a local variable named result.
        var result = _accountRepository.GetByGuid(guid);
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
        // Returns a 200 OK response with the EmployeeDto object created from the result object.
        return Ok(new ResponseOkHandler<AccountDto>((AccountDto)result));
    }

    [HttpPost] // This attribute specifies that this method should handle HTTP POST requests.
    // Declares a public method named Create that takes an CreateAccountDto parameter and returns an IActionResult.
    public IActionResult Create(CreateAccountDto accountDto)
    {
        try
        {
            Account toCreate = accountDto;
            // Create hashing password
            toCreate.Password = HashHandler.HashPassword(accountDto.Password);
            // Calls the Create method of the _accountRepository field with the accountDto parameter and assigns the result to a local variable named result.
            var result = _accountRepository.Create(toCreate);
            // Returns a 200 OK response with the EmployeeDto object created from the result object.
            return Ok(new ResponseOkHandler<AccountDto>((AccountDto)result));
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
    // Declares a public method named Update that takes an AccountDto parameter and returns an IActionResult.
    public IActionResult Update(AccountDto accountDto)
    {
        try
        {
            // Calls the GetByGuid method of the _accountRepository field with the Guid property of the accountDto parameter.
            // Assigns the result to a local variable named entity.
            var entity = _accountRepository.GetByGuid(accountDto.Guid);
            if (entity is null)
            {
                // Returns a 404 Not Found response if the entity is null.
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            // Assigns the accountDto parameter to a new Account object named toUpdate.
            Account toUpdate = accountDto;
            // Assigns the CreatedDate property of the entity object to the CreatedDate property of the toUpdate object.
            toUpdate.CreatedDate = entity.CreatedDate;
            toUpdate.Password = HashHandler.HashPassword(accountDto.Password);
            // Calls the Update method of the _accountRepository field with the toUpdate object and assigns the result to a local variable named result.
            _accountRepository.Update(toUpdate);
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
                Message = "Failed to create data",
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
            var entity = _accountRepository.GetByGuid(guid);
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
            // Calls the Delete method of the _accountRepository field with the entity parameter and assigns the result to a local variable named result.
            _accountRepository.Delete(entity);
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
