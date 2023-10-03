using API.Contracts;
using API.DTO.Employees;
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
        // Converts each Employee object in the result to a EmployeeDto object and assigns the result to a local variable named data.
        var data = result.Select(x => (EmployeeDto)x);
        // Returns a 200 OK response with the data if it is not empty.
        return Ok(data); 
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
        // Returns a 200 OK response with the EmployeeDto object created from the result object.
        return Ok((EmployeeDto)result);
    }

    [HttpPost]
    // Declares a public method named Create that takes a CreateEmployeeDto parameter and returns an IActionResult.
    public IActionResult Create(CreateEmployeeDto employeeDto)
    {
        // Calls the Create method of the _employeeRepository field with the employeeDto parameter and assigns the result to a local variable named result.
        var result = _employeeRepository.Create(employeeDto);
        if (result is null)
        {
            return BadRequest("Failed to create data"); // Returns a 400 Bad Request response with a message if the result is null.
        }
        // Returns a 200 OK response with the EmployeeDto object created from the result object.
        return Ok((EmployeeDto)result);
    }

    [HttpPut]
    // Declares a public method named Update that takes an EmployeeDto parameter and returns an IActionResult.
    public IActionResult Update(EmployeeDto employeeDto)
    {
        // Calls the GetByGuid method of the _employeeRepository field with the Guid property of the employeeDto parameter.
        // Assigns the result to a local variable named entity.
        var entity = _employeeRepository.GetByGuid(employeeDto.Guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }
        // Assigns the employeeDto parameter to a new Employee object named toUpdate.
        Employee toUpdate = employeeDto;
        // Assigns the CreatedDate property of the entity object to the CreatedDate property of the toUpdate object.
        toUpdate.CreatedDate = entity.CreatedDate;
        // Calls the Update method of the _employeeRepository field with the toUpdate object and assigns the result to a local variable named result.
        var result = _employeeRepository.Update(toUpdate);
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
        // Calls the GetByGuid method of the _employeeRepository field with the guid parameter and assigns the result to a local variable named entity.
        var entity = _employeeRepository.GetByGuid(guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }
        // Calls the Delete method of the _employeeRepository field with the entity parameter and assigns the result to a local variable named result.
        var result = _employeeRepository.Delete(entity);
        if (!result)
        {
            return BadRequest("Failed to delete data"); // Returns a 400 Bad Request response with a message if the result is false.
        }
        return Ok("Data Deleted"); // Returns a 200 OK response with a message if it is not false.
    }
}
