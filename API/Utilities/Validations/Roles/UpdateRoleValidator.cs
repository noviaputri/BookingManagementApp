using API.DTO.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles;

// Declares public class UpdateRoleValidator that inherits from the AbstractValidator class with a generic type parameter of RoleDto.
public class UpdateRoleValidator : AbstractValidator<RoleDto>
{
    // Declares a public constructor for the UpdateRoleValidator class.
    public UpdateRoleValidator() 
    {
        // Declares a rule for the Guid property must not be empty.
        RuleFor(r => r.Guid).NotEmpty().WithMessage("Guid must not be empty");

        // Declares a rule for the Name property.
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Name must not be empty")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Name must only contain letters and spaces")
            .MaximumLength(100).WithMessage("Name must have at most 100 characters");
    }
}
