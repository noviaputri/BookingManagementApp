using API.DTO.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts;

// Declares public class UpdateAccountValidator that inherits from the AbstractValidator class with a generic type parameter of AccountDto.
public class UpdateAccountValidator : AbstractValidator<AccountDto>
{
    public UpdateAccountValidator()
    {
        // Declares a rule for the Guid property must not be empty.
        RuleFor(a => a.Guid).NotEmpty().WithMessage("Guid must not be empty");

        // Declares a rule for the Password property.
        RuleFor(a => a.Password)
            .NotEmpty().WithMessage("Password must not be empty")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

        // Declares a rule for the Otp property must not be empty.
        RuleFor(a => a.Otp).NotEmpty().WithMessage("Otp must not be empty");

        // Declares a rule for the IsUsed property must not be empty.
        RuleFor(a => a.IsUsed).NotEmpty().WithMessage("IsUsed must not be empty");

        // Declares a rule for the ExpiredTime property must not be empty.
        RuleFor(a => a.ExpiredTime).NotEmpty().WithMessage("ExpiredTime must not be empty");
    }
}
