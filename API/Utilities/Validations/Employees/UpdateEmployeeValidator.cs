using API.DTO.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Employees;

// Declares public class UpdateEmployeeValidator that inherits from the AbstractValidator class with a generic type parameter of EmployeeDto.
public class UpdateEmployeeValidator : AbstractValidator<EmployeeDto>
{
    public UpdateEmployeeValidator()
    {
        // Declares a rule for the Guid property must not be empty.
        RuleFor(e => e.Guid).NotEmpty().WithMessage("Guid must not be empty");

        // Declares a rule for the Nik property must not be empty and have at most 6 characters.
        RuleFor(e => e.Nik)
            .NotEmpty().WithMessage("Nik must not be empty")
            .MaximumLength(6).WithMessage("Nik must have at most 6 characters");

        // Declares a rule for the FirstName property.
        RuleFor(e => e.FirstName)
            .NotEmpty().WithMessage("FirstName must not be empty")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Major must only contain letters and spaces")
            .MaximumLength(100).WithMessage("Major must have at most 100 characters");

        // Declares a rule for the BirthDate property.
        RuleFor(e => e.BirthDate)
           .NotEmpty().WithMessage("BirthDate must not be empty")
           .LessThanOrEqualTo(DateTime.Now.AddYears(-18)).WithMessage("Age must be greater than or equal to 18 years old");

        // Declares a rule for the Gender property must not be empty and must be a valid enumeration value.
        RuleFor(e => e.Gender)
           .NotEmpty().WithMessage("Gender must not be empty")
           .IsInEnum();

        // Declares a rule for the HiringDate property.
        RuleFor(e => e.HiringDate)
            .NotEmpty().WithMessage("HiringDate must not be empty")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("HiringDate not valid");

        // Declares a rule for the Email property must not be empty and in a valid email format.
        RuleFor(e => e.Email)
           .NotEmpty().WithMessage("Email must not be empty")
           .EmailAddress().WithMessage("Incorrect email format")
           .MaximumLength(100).WithMessage("Email must have at most 100 characters");

        // Declares a rule for the PhoneNumber property.
        RuleFor(e => e.PhoneNumber)
           .NotEmpty().WithMessage("PhoneNumber must not be empty")
           .Matches(@"^[0-9]+$").WithMessage("PhoneNumber must only contain numbers")
           .MinimumLength(10)
           .MaximumLength(20);
    }
}
