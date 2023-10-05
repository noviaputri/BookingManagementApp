using API.Contracts;
using API.DTO.Employees;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController] // This attribute indicates that the class is an API controller.
[Route("api/[controller]")] // This attribute specifies the route prefix for the controller
// Declares a new class named EmployeeController that inherits from ControllerBase.
public class EmployeeController : ControllerBase
{
    // Declares a private field of type IEmployeeRepository, IEducationRepository and IUniversityRepository.
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;

    // Declares a public constructor that takes an IEmployeeRepository, IEducationRepository and IUniversityRepository parameter.
    public EmployeeController(IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository)
    {
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
    }

    [HttpGet("details")]
    public IActionResult GetDetails()
    {
        var employees = _employeeRepository.GetAll();
        var educations = _educationRepository.GetAll();
        var universities = _universityRepository.GetAll();

        if (!(employees.Any() && educations.Any() && universities.Any()))
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        var employeeDetails = from emp in employees
                              join edu in educations on emp.Guid equals edu.Guid
                              join unv in universities on edu.UniversityGuid equals unv.Guid
                              select new EmployeeDetailDto
                              {
                                  Guid = emp.Guid,
                                  Nik = emp.Nik,
                                  FullName = string.Concat(emp.FirstName, " ", emp.LastName),
                                  BirthDate = emp.BirthDate,
                                  Gender = emp.Gender.ToString(),
                                  HiringDate = emp.HiringDate,
                                  Email = emp.Email,
                                  PhoneNumber = emp.PhoneNumber,
                                  Major = edu.Major,
                                  Degree = edu.Degree,
                                  Gpa = edu.Gpa,
                                  University = unv.Name
                              };
        return Ok(new ResponseOkHandler<IEnumerable<EmployeeDetailDto>>(employeeDetails));
    }

    [HttpGet]
    // Declares a public method named GetAll that returns an IActionResult.
    public IActionResult GetAll()
    {
        // Calls the GetAll method of the _employeeRepository field and assigns the result to a local variable named result.
        var result = _employeeRepository.GetAll();
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
        // Converts each Employee object in the result to a EmployeeDto object and assigns the result to a local variable named data.
        var data = result.Select(x => (EmployeeDto)x);
        // Returns a 200 OK response with the data if it is not empty.
        return Ok(new ResponseOkHandler<IEnumerable<EmployeeDto>>(data));
    }

    [HttpGet("{guid}")]
    // Declares a public method named GetByGuid that takes a Guid parameter and returns an IActionResult.
    public IActionResult GetByGuid(Guid guid)
    {
        // Calls the GetByGuid method of the _employeeRepository field with the guid parameter and assigns the result to a local variable named result.
        var result = _employeeRepository.GetByGuid(guid);
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
        return Ok(new ResponseOkHandler<EmployeeDto>((EmployeeDto)result));
    }

    [HttpPost]
    // Declares a public method named Create that takes a CreateEmployeeDto parameter and returns an IActionResult.
    public IActionResult Create(CreateEmployeeDto employeeDto)
    {
        try
        {
            Employee toCreate = employeeDto;
            // Auto generate Nik
            toCreate.Nik = GenerateHandler.GenerateNik(_employeeRepository.GetLastNik());
            // Calls the Create method of the _employeeRepository field with the employeeDto parameter and assigns the result to a local variable named result.
            var result = _employeeRepository.Create(toCreate);
            // Returns a 200 OK response with the EmployeeDto object created from the result object.
            return Ok(new ResponseOkHandler<EmployeeDto>((EmployeeDto)result));
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

    [HttpPut]
    // Declares a public method named Update that takes an EmployeeDto parameter and returns an IActionResult.
    public IActionResult Update(EmployeeDto employeeDto)
    {
        try
        {
            // Calls the GetByGuid method of the _employeeRepository field with the Guid property of the employeeDto parameter.
            // Assigns the result to a local variable named entity.
            var entity = _employeeRepository.GetByGuid(employeeDto.Guid);
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
            Employee toUpdate = employeeDto; // Assigns the employeeDto parameter to a new Employee object named toUpdate.
            toUpdate.Nik = entity.Nik; // Assigns Nik property of the entity object to Nik property of the toUpdate object.
            toUpdate.CreatedDate = entity.CreatedDate; // Assigns CreatedDate property of the entity object to CreatedDate property of the toUpdate object.
            _employeeRepository.Update(toUpdate); // Calls the Update method of the _employeeRepository field with the toUpdate object.
            return Ok(new ResponseOkHandler<string>("Data Updated")); // Returns a 200 OK response if it is not false.
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
            // Calls the GetByGuid method of the _employeeRepository field with the guid parameter and assigns the result to a local variable named entity.
            var entity = _employeeRepository.GetByGuid(guid);
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
            // Calls the Delete method of the _employeeRepository field with the entity parameter.
            _employeeRepository.Delete(entity);
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
