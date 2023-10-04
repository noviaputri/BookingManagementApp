using API.DTO.Universities;
using FluentValidation;

namespace API.Utilities.Validations.Universities;

// Declares public class UpdateUniversityValidator that inherits from the AbstractValidator class with a generic type parameter of UniversityDto.
public class UpdateUniversityValidator : AbstractValidator<UniversityDto>
{
    // Declares a public constructor for the UpdateUniversityValidator class.
    public UpdateUniversityValidator()
    {
        // Declares a rule for the Guid property must not be empty.
        RuleFor(u => u.Guid).NotEmpty().WithMessage("Guid must not be empty");

        // Declares a rule for the Code property.
        RuleFor(u => u.Code)
            .NotEmpty().WithMessage("Code must not be empty")
            .MaximumLength(50).WithMessage("Code Name must have at most 50 characters");

        // Declares a rule for the Name property.
        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("Name must not be empty")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Name must only contain letters and spaces")
            .MaximumLength(100).WithMessage("Name must have at most 100 characters");
    }
}
