using API.DTO.Educations;
using FluentValidation;

namespace API.Utilities.Validations.Educations;

// Declares public class CreateEducationValidator that inherits from the AbstractValidator class with a generic type parameter of CreateEducationDto.
public class CreateEducationValidator : AbstractValidator<CreateEducationDto>
{
    // Declares a public constructor for the CreateEducationValidator class.
    public CreateEducationValidator()
    {
        // Declares a rule for the Guid property must not be empty.
        RuleFor(e => e.Guid).NotEmpty().WithMessage("Guid must not be empty");

        // Declares a rule for the Major property.
        RuleFor(e => e.Major)
            .NotEmpty().WithMessage("Major must not be empty")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Major must only contain letters and spaces")
            .MaximumLength(100).WithMessage("Major must have at most 100 characters");

        // Declares a rule for the Degree property.
        RuleFor(e => e.Degree)
            .NotEmpty().WithMessage("Degree must not be empty")
            .Matches(@"^[a-zA-Z ]+$").WithMessage("Degree must only contain letters and spaces")
            .MaximumLength(100).WithMessage("Degree must have at most 100 characters");

        // Declares a rule for the Gpa property.
        RuleFor(e => e.Gpa)
            .NotEmpty().WithMessage("GPA must not be empty")
            .InclusiveBetween(0, 4).WithMessage("GPA must be between 0 and 4");

        // Declares a rule for the UniversityGuid property must not be empty.
        RuleFor(e => e.UniversityGuid).NotEmpty().WithMessage("UniversityGuid must not be empty");
    }
}
