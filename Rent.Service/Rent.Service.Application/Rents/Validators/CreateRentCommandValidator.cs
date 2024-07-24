using FluentValidation;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Common.Constants;
using Rent.Service.Application.Rents.Commands;

namespace Rent.Service.Application.Rents.Validators;

public class CreateRentCommandValidator : AbstractValidator<CreateRentCommand>
{
    public CreateRentCommandValidator(IRentRepository rentRepository)
    {
        RuleFor(v => v.StartRentDate)
            .NotEmpty().WithMessage(ValidationMessages.StartDateRequired)
            .Must(BeAValidDate).WithMessage(ValidationMessages.StartDateInvalid)
            .Must(BeInFuture).WithMessage(ValidationMessages.StartDateInFuture);

        RuleFor(v => v.EndRentDate)
            .NotEmpty().WithMessage(ValidationMessages.EndDateRequired)
            .Must(BeAValidDate).WithMessage(ValidationMessages.EndDateInvalid)
            .GreaterThan(v => v.StartRentDate).WithMessage(ValidationMessages.EndDateAfterStartDate);

        RuleFor(v => v.ThingId)
            .NotEmpty().WithMessage(ValidationMessages.ThingIdRequired)
            .Must(BeAValidGuid).WithMessage(ValidationMessages.ThingIdInvalid);

        RuleFor(v => v.UserId)
            .NotEmpty().WithMessage(ValidationMessages.UserIdRequired)
            .Must(BeAValidGuid).WithMessage(ValidationMessages.UserIdInvalid);

        RuleFor(x => x)
           .MustAsync(async (command, cancellationToken) =>
               await rentRepository.IsAvailableAsync(command.ThingId, command.StartRentDate, command.EndRentDate))
           .WithMessage(ValidationMessages.NotAvailableToRent);
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
