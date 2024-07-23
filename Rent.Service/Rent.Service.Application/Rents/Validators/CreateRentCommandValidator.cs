using FluentValidation;
using Rent.Service.Application.Rents.Commands;

namespace Rent.Service.Application.Rents.Validators;

public class CreateRentCommandValidator : AbstractValidator<CreateRentCommand>
{
    public CreateRentCommandValidator()
    {
        RuleFor(v => v.StartRentDate)
            .NotEmpty().WithMessage("Start date is required")
            .Must(BeAValidDate).WithMessage("Start date must be a valid date")
            .Must(BeInFuture).WithMessage("Start date must be in the future");

        RuleFor(v => v.EndRentDate)
            .NotEmpty().WithMessage("End date is required")
            .Must(BeAValidDate).WithMessage("End date must be a valid date")
            .GreaterThan(v => v.StartRentDate).WithMessage("End date must be after the start date");

        RuleFor(v => v.ThingId)
            .NotEmpty().WithMessage("Thing ID is required")
            .Must(BeAValidGuid).WithMessage("Thing ID must be a valid GUID");

        RuleFor(v => v.UserId)
            .NotEmpty().WithMessage("User ID is required")
            .Must(BeAValidGuid).WithMessage("User ID must be a valid GUID");
    }

    private bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }

    private bool BeInFuture(DateTime date)
    {
        return date > DateTime.Now;
    }

    private bool BeAValidGuid(Guid guid)
    {
        return guid != Guid.Empty;
    }
}
