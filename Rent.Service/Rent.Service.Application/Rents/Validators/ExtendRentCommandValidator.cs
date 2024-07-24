using FluentValidation;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Common.Constants;
using Rent.Service.Application.Rents.Commands;

namespace Rent.Service.Application.Rents.Validators;

public class ExtendRentCommandValidator : AbstractValidator<ExtendRentCommand>
{
    private readonly IRentRepository _rentRepository;
    public ExtendRentCommandValidator(IRentRepository rentRepository)
    {
        _rentRepository = rentRepository;

        RuleFor(v => v.RentId)
            .NotEmpty().WithMessage(ValidationMessages.ThingIdRequired)
            .Must(BeAValidGuid).WithMessage(ValidationMessages.RentIdInvalid);

        RuleFor(v => v.NewEndRentDate)
            .NotEmpty().WithMessage(ValidationMessages.EndDateRequired)
            .Must(BeAValidDate).WithMessage(ValidationMessages.EndDateInvalid)
            .GreaterThan(DateTime.Now).WithMessage(ValidationMessages.EndDateInvalid);

        RuleFor(x => x)
            .MustAsync(BeAvailableForExtension).WithMessage(ValidationMessages.NotAvailableToExtend);
    }

    private bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }

    private bool BeAValidGuid(Guid guid)
    {
        return guid != Guid.Empty;
    }

    private async Task<bool> BeAvailableForExtension(ExtendRentCommand command, CancellationToken cancellationToken)
    {
        return await _rentRepository.IsAvailableForExtensionAsync(command.RentId, command.NewEndRentDate);
    }
}
