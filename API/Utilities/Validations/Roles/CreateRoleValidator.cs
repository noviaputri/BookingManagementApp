using API.DTO.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles;

// Declares public class CreateRoleValidator that inherits from the AbstractValidator class with a generic type parameter of CreateRoleDto.
public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
{
    // Declares a public constructor for the CreateRoleValidator class.
    public CreateRoleValidator() 
    {
        // Declares a rule for the Name property.
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Name must not be empty")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Name must only contain letters and spaces")
            .MaximumLength(100).WithMessage("Name must have at most 100 characters");
    }
}
