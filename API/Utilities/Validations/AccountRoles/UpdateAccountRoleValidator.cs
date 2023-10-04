using API.DTO.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles;

// Declares public class UpdateAccountRoleValidator that inherits from the AbstractValidator class with a generic type parameter of AccountRoleDto.
public class UpdateAccountRoleValidator : AbstractValidator<AccountRoleDto>
{
    // Declares a public constructor for the UpdateAccountRoleValidator class.
    public UpdateAccountRoleValidator()
    {
        // Declares a rule for the Guid property must not be empty.
        RuleFor(ar => ar.Guid).NotEmpty().WithMessage("Guid must not be empty");

        // Declares a rule for the AccountGuid property must not be empty.
        RuleFor(ar => ar.AccountGuid).NotEmpty().WithMessage("AccountGuid must not be empty");

        // Declares a rule for the RoleGuid property must not be empty.
        RuleFor(ar => ar.RoleGuid).NotEmpty().WithMessage("RoleGuid must not be empty");
    }
}
