using API.DTO.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles;

public class GuidAccountRoleValidator : AbstractValidator<GuidAccountRoleDto>
{
    public GuidAccountRoleValidator()
    {
        // Declares a rule for the Guid property must not be empty.
        RuleFor(ar => ar.Guid).NotEmpty().WithMessage("Guid must not be empty");
    }
}
