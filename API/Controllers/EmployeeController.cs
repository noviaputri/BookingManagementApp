using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // This attribute indicates that the class is an API controller.
[Route("api/[controller]")] // This attribute specifies the route prefix for the controller
// Declares a new class named EmployeeController that inherits from ControllerBase.
public class EmployeeController : ControllerBase
{
    // Declares a private field of type IEmployeeRepository.
    private readonly IEmployeeRepository _employeeRepository;

    // Declares a public constructor that takes an IEmployeeRepository parameter.
    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    // Declares a public method named GetAll that returns an IActionResult.
    public IActionResult GetAll()
    {
        // Calls the GetAll method of the _employeeRepository field and assigns the result to a local variable named result.
        var result = _employeeRepository.GetAll();
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
        // Calls the GetByGuid method of the _employeeRepository field with the guid parameter and assigns the result to a local variable named result.
        var result = _employeeRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Id Not Found");
        }
        return Ok(result);
    }

    [HttpPost]
    // Declares a public method named Create that takes an Employee parameter and returns an IActionResult.
    public IActionResult Create(Employee employee)
    {
        // Calls the Create method of the _employeeRepository field with the employee parameter and assigns the result to a local variable named result.
        var result = _employeeRepository.Create(employee);
        if (result is null)
        {
            return BadRequest("Failed to create data"); // Returns a 400 Bad Request response with a message if the result is null.
        }
        return Ok(result);
    }

    [HttpPut]
    // Declares a public method named Update that takes an Employee parameter and returns an IActionResult.
    public IActionResult Update(Employee employee)
    {
        // Calls the Update method of the _employeeRepository field with the employee parameter and assigns the result to a local variable named result.
        var result = _employeeRepository.Update(employee);
        if (result is false)
        {
            return BadRequest("Failed to update data"); // Returns a 400 Bad Request response with a message if the result is false.
        }
        return Ok(result); // Returns a 200 OK response with the result if it is not false.
    }

    [HttpDelete]
    // Declares a public method named Delete that takes an Employee parameter and returns an IActionResult.
    public IActionResult Delete(Employee employee)
    {
        // Calls the Delete method of the _employeeRepository field with the employee parameter and assigns the result to a local variable named result.
        var result = _employeeRepository.Delete(employee);
        if (result is false)
        {
            return BadRequest("Failed to delete data");
        }
        return Ok(result);
    }
}
