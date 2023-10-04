using API.DTO.Bookings;
using FluentValidation;

namespace API.Utilities.Validations.Bookings;

// Declares public class CreateBookingValidator that inherits from the AbstractValidator class with a generic type parameter of CreateBookingDto.
public class CreateBookingValidator : AbstractValidator<CreateBookingDto>
{
    // Declares a public constructor for the CreateBookingValidator class.
    public CreateBookingValidator()
    {
        // Declares a rule for the StartDate property.
        RuleFor(b => b.StartDate).NotEmpty().WithMessage("StartDate must not be empty");

        // Declares a rule for the EndDate property.
        RuleFor(b => b.EndDate)
            .NotEmpty().WithMessage("EndDate must not be empty")
            .GreaterThanOrEqualTo(b => b.StartDate).WithMessage("EndDate must be greater than or equal to StartDate");

        // Declares a rule for the Status property must not be empty and must be a valid enumeration value.
        RuleFor(b => b.Status)
            .NotEmpty().WithMessage("Status must not be empty")
            .IsInEnum();

        // Declares a rule for the Remarks property must not be empty.
        RuleFor(b => b.Remarks).NotEmpty().WithMessage("Remarks must not be empty");

        // Declares a rule for the RoomGuid property must not be empty.
        RuleFor(b => b.RoomGuid).NotEmpty().WithMessage("RoomGuid must not be empty");

        // Declares a rule for the EmployeeGuid property must not be empty.
        RuleFor(b => b.EmployeeGuid).NotEmpty().WithMessage("EmployeeGuid must not be empty");
    }
}
