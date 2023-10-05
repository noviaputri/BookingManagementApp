using API.DTO.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts;

// Declares public class ChangePasswordValidator that inherits from the AbstractValidator class with a generic type parameter of ChangePasswordDto.
public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordValidator()
    {
        // Declares a rule for the Email property must not be empty and in a valid email format.
        RuleFor(a => a.Email)
           .NotEmpty().WithMessage("Email must not be empty")
           .EmailAddress().WithMessage("Incorrect email format")
           .MaximumLength(100).WithMessage("Email must have at most 100 characters");

        // Declares a rule for the Otp property must not be null.
        RuleFor(a => a.Otp).NotNull().WithMessage("Otp must not be null");

        // Declares a rule for the NewPassword property.
        RuleFor(a => a.NewPassword)
            .NotEmpty().WithMessage("Password must not be empty")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

        // Declares a rule for the ConfirmPassword property.
        RuleFor(a => a.ConfirmPassword)
            .NotEmpty().WithMessage("Password must not be empty")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(a => a.NewPassword).WithMessage("ConfirmPassword must matches with NewPassword");
    }
}
