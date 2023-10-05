using API.Contracts;
using API.Data;
using API.DTO.Accounts;
using API.DTO.Educations;
using API.DTO.Employees;
using API.DTO.Universities;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API.Controllers;

[ApiController] // This attribute indicates that the class is an API controller.
[Route("api/[controller]")] // This attribute specifies the route prefix for the controller
// Declares a new class named AccountController that inherits from ControllerBase.
public class AccountController : ControllerBase
{
    // Declares a private field of type IAccountRepository, IEmployeeRepository, IUniversityRepository, IEducationRepository and BookingManagementDbContext.
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUniversityRepository _universityRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IEmailHandler _emailHandler;
    private readonly BookingManagementDbContext _context;

    // Declares a public constructor that takes an IAccountRepository parameter.
    public AccountController(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IUniversityRepository universityRepository, IEducationRepository educationRepository, IEmailHandler emailHandler, BookingManagementDbContext bookingManagementDbContext)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _universityRepository = universityRepository;
        _educationRepository = educationRepository;
        _emailHandler = emailHandler;
        _context = bookingManagementDbContext;
    }

    [HttpPost("forgotpassword")] // This attribute specifies that this method should handle HTTP POST requests for forgotpassword.
    public IActionResult ForgotPassword(string email)
    {
        // Check if email already exist or not
        var employees = _employeeRepository.GetAll();
        var employee = employees.FirstOrDefault(e => e.Email == email);
        if (employee is null)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Email not found"
            });
        }
        // Check if account already exist or not
        var account = _accountRepository.GetByGuid(employee.Guid);
        if (account is null)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Account not found"
            });
        }
        // Generate 6 random numbers
        Random random = new Random();
        int otp = random.Next(100000, 999999);
        // Store the OTP in the database along with email and expiration time
        account.Otp = otp;
        account.ExpiredTime = DateTime.Now.AddMinutes(5);
        account.IsUsed = false;
        _accountRepository.Update(account);
        // Send OTP to smtp
        _emailHandler.Send("Forgot password", $"Your OTP is {account.Otp}", email);
        // Return the OTP in the response body
        return Ok(new ResponseOkHandler<string>("OTP has been send to your email"));
    }

    [HttpPost("changepassword")] // This attribute specifies that this method should handle HTTP POST requests for changepassword.
    public IActionResult ChangePassword(ChangePasswordDto changePasswordDto) 
    {
        try
        {
            // Get account by email
            var employees = _employeeRepository.GetAll();
            var employee = employees.FirstOrDefault(e => e.Email == changePasswordDto.Email);
            var account = _accountRepository.GetByGuid(employee.Guid);
            // Check if OTP is valid
            if (account == null || account.Otp != changePasswordDto.Otp)
            {
                return BadRequest(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Invalid OTP"
                });
            }
            // Check if OTP has been used
            if (account.IsUsed)
            {
                return BadRequest(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "OTP has been used"
                });
            }
            // Check if OTP has expired
            if (DateTime.Now > account.ExpiredTime)
            {
                return BadRequest(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "OTP has expired"
                });
            }
            // Check if new password and confirm password match
            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
            {
                return BadRequest(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "New password and confirm password do not match"
                });
            }
            // Assigns the changePasswordDto parameter to a new Account object named toChangePassword.
            Account toChangePassword = changePasswordDto;
            // Assigns the CreatedDate property of the entity object to the Guid, CreatedDate, and Password property of the toChangePassword object.
            toChangePassword.Guid = account.Guid;
            toChangePassword.CreatedDate = account.CreatedDate;
            toChangePassword.Password = HashHandler.HashPassword(changePasswordDto.NewPassword);
            // Calls the Update method of the _accountRepository field with the toChangePassword object.
            _accountRepository.Update(toChangePassword);
            // Returns a 200 OK response if it is not false.
            return Ok(new ResponseOkHandler<string>("Password updated successfully"));
        }
        catch (ExceptionHandler ex)
        {
            // Returns a 500 Internal Server Error response with a new ResponseErrorHandler object.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to change password",
                Error = ex.Message
            });
        }
    }

    [HttpPost("register")] // This attribute specifies that this method should handle HTTP POST requests for register.
    public IActionResult Register(RegisterDto registerDto)
    {
        using var connection = _context.Database.GetDbConnection();
        connection.Open();
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                // Check if university exists in the database
                var universities = _universityRepository.GetAll();
                var university = universities.FirstOrDefault(u => u.Code == registerDto.Code);
                if (university is null)
                {
                    university = new CreateUniversityDto
                    {
                        Code = registerDto.Code,
                        Name = registerDto.Name
                    };
                    _universityRepository.Create(university);
                }
                // Check if email already exists in the database
                var employeeExists = _employeeRepository.GetAll();
                var employeeExist = employeeExists.FirstOrDefault(e => e.Email == registerDto.Email);
                if (employeeExist is not null)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Email already exists"
                    });
                }
                // Check if password and confirm password match
                if (registerDto.Password != registerDto.ConfirmPassword)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Password and confirm password do not match"
                    });
                }
                // Create a new employee object 
                var employeeToCreate = new CreateEmployeeDto
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    BirthDate = registerDto.BirthDate,
                    Gender = registerDto.Gender,
                    HiringDate = registerDto.HiringDate,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber
                };
                Employee createEmployee = employeeToCreate;
                createEmployee.Nik = GenerateHandler.GenerateNik(_employeeRepository.GetLastNik());
                var employeeResult = _employeeRepository.Create(createEmployee);
                // Create a new education object
                var educationToCreate = new CreateEducationDto
                {
                    Guid = employeeResult.Guid,
                    Major = registerDto.Major,
                    Degree = registerDto.Degree,
                    Gpa = registerDto.Gpa,
                    UniversityGuid = university.Guid
                };
                var educationResult = _educationRepository.Create(educationToCreate);
                // Create a new account object
                var accountToCreate = new CreateAccountDto
                {
                    Guid = employeeResult.Guid,
                    Password = registerDto.Password,
                    Otp = 0,
                    IsUsed = true,
                    ExpiredTime = DateTime.Now
                };
                Account createAccount = accountToCreate;
                createAccount.Password = HashHandler.HashPassword(accountToCreate.Password);
                var result = _accountRepository.Create(createAccount);
                // Save changes to database
                _context.SaveChanges();
                transaction.Commit();
                connection.Close();
                // Return a success response
                return Ok(new ResponseOkHandler<string>("User registered successfully"));
            }
            catch (Exception ex)
            {
                // Rollback transaction if there is an exception
                transaction.Rollback();
                // Returns a 500 Internal Server Error response with a new ResponseErrorHandler object.
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to Register Account",
                    Error = ex.Message
                });
            }
        }
    }

    [HttpPost("login")] // This attribute specifies that this method should handle HTTP POST requests for login.
    public IActionResult Login(string email, string password)
    {
        // Check if email already exist or not
        var employees = _employeeRepository.GetAll();
        var employee = employees.FirstOrDefault(e => e.Email == email);
        if (employee == null)
        {
            return BadRequest(new ResponseErrorHandler
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Email is invalid"
            });
        }
        // Check if password is correct
        var account = _accountRepository.GetByGuid(employee.Guid);
        var verifyPassword = HashHandler.VerifyPassword(password, account.Password);
        if (!verifyPassword)
        {
            return BadRequest(new ResponseErrorHandler
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Password is invalid"
            });
        }
        // Return a success response
        return Ok(new ResponseOkHandler<string>("Login successfully"));
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
