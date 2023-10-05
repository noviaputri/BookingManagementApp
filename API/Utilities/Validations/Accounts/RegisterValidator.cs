using API.DTO.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts;

// Declares public class RegisterValidator that inherits from the AbstractValidator class with a generic type parameter of RegisterDto.
public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator() 
    {
        // Declares a rule for the FirstName property.
        RuleFor(r => r.FirstName)
            .NotEmpty().WithMessage("FirstName must not be empty")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Major must only contain letters and spaces")
            .MaximumLength(100).WithMessage("Major must have at most 100 characters");

        // Declares a rule for the BirthDate property.
        RuleFor(r => r.BirthDate)
           .NotEmpty().WithMessage("BirthDate must not be empty")
           .LessThanOrEqualTo(DateTime.Now.AddYears(-18)).WithMessage("Age must be greater than or equal to 18 years old");

        // Declares a rule for the Gender property must not be null and must be a valid enumeration value.
        RuleFor(r => r.Gender)
           .NotNull().WithMessage("Gender must not be null")
           .IsInEnum();

        // Declares a rule for the HiringDate property.
        RuleFor(r => r.HiringDate)
            .NotEmpty().WithMessage("HiringDate must not be empty")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("HiringDate not valid");

        // Declares a rule for the Email property must not be empty and in a valid email format.
        RuleFor(r => r.Email)
           .NotEmpty().WithMessage("Email must not be empty")
           .EmailAddress().WithMessage("Incorrect email format")
           .MaximumLength(100).WithMessage("Email must have at most 100 characters");

        // Declares a rule for the PhoneNumber property.
        RuleFor(r => r.PhoneNumber)
           .NotEmpty().WithMessage("PhoneNumber must not be empty")
           .Matches(@"^[0-9]+$").WithMessage("PhoneNumber must only contain numbers")
           .MinimumLength(10)
           .MaximumLength(20);

        // Declares a rule for the Major property.
        RuleFor(r => r.Major)
            .NotEmpty().WithMessage("Major must not be empty")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Major must only contain letters and spaces")
            .MaximumLength(100).WithMessage("Major must have at most 100 characters");

        // Declares a rule for the Degree property.
        RuleFor(r => r.Degree)
            .NotEmpty().WithMessage("Degree must not be empty")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Degree must only contain letters and spaces")
            .MaximumLength(100).WithMessage("Degree must have at most 100 characters");

        // Declares a rule for the Gpa property.
        RuleFor(r => r.Gpa)
            .NotEmpty().WithMessage("GPA must not be empty")
            .InclusiveBetween(0, 4).WithMessage("GPA must be between 0 and 4");

        // Declares a rule for the Code property.
        RuleFor(r => r.Code)
            .NotEmpty().WithMessage("Code must not be empty")
            .MaximumLength(50).WithMessage("Code Name must have at most 50 characters");

        // Declares a rule for the Name property.
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Name must not be empty")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Name must only contain letters and spaces")
            .MaximumLength(100).WithMessage("Name must have at most 100 characters");

        // Declares a rule for the Password property.
        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password must not be empty")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

        // Declares a rule for the ConfirmPassword property.
        RuleFor(r => r.ConfirmPassword)
            .NotEmpty().WithMessage("Password must not be empty")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(a => a.Password).WithMessage("ConfirmPassword must matches with NewPassword");
    }
}
