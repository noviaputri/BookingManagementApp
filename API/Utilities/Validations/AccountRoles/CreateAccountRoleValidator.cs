using API.DTO.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles;

// Declares public class CreateAccountRoleValidator that inherits from the AbstractValidator class with a generic type parameter of CreateAccountRoleDto.
public class CreateAccountRoleValidator : AbstractValidator<CreateAccountRoleDto>
{
    // Declares a public constructor for the CreateAccountRoleValidator class.
    public CreateAccountRoleValidator() 
    {
        // Declares a rule for the AccountGuid property must not be empty.
        RuleFor(ar => ar.AccountGuid).NotEmpty().WithMessage("AccountGuid must not be empty");

        // Declares a rule for the RoleGuid property must not be empty.
        RuleFor(ar => ar.RoleGuid).NotEmpty().WithMessage("RoleGuid must not be empty");
    }
}
