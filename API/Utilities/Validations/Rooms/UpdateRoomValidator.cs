using API.DTO.Rooms;
using FluentValidation;

namespace API.Utilities.Validations.Rooms;

// Declares public class UpdateRoomValidator that inherits from the AbstractValidator class with a generic type parameter of RoomDto.
public class UpdateRoomValidator : AbstractValidator<RoomDto>
{
    // Declares a public constructor for the UpdateRoomValidator class.
    public UpdateRoomValidator()
    {
        // Declares a rule for the Guid property must not be empty.
        RuleFor(r => r.Guid).NotEmpty().WithMessage("Guid must not be empty");

        // Declares a rule for the Name property.
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Room Name must not be empty")
            .MaximumLength(100).WithMessage("Room Name must have at most 100 characters");

        // Declares a rule for the Floor property.
        RuleFor(r => r.Floor)
            .NotEmpty().WithMessage("Floor must not be empty")
            .GreaterThanOrEqualTo(1).WithMessage("Floor must greater or equal to 1");

        // Declares a rule for the Capacity property.
        RuleFor(r => r.Capacity)
            .NotEmpty().WithMessage("Capacity must not be empty")
            .GreaterThanOrEqualTo(1).WithMessage("Capacity must greater or equal to 1");
    }
}
