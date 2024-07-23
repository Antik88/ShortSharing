using FluentValidation;
using Rent.Service.Application.Common.Constants;
using Rent.Service.Application.Rents.Commands;

namespace Rent.Service.Application.Rents.Validators;

public class UpdateRentCommandValidator : AbstractValidator<UpdateRentCommand>
{
    public UpdateRentCommandValidator()
    {
        RuleFor(v => v.StartRentDate)
               .NotEmpty().WithMessage(ValidationMessages.StartDateRequired)
               .Must(v => v.Date > DateTime.Now).WithMessage(ValidationMessages.StartDateInFuture);

        RuleFor(v => v.EndRentDate)
            .NotEmpty().WithMessage(ValidationMessages.EndDateRequired)
            .GreaterThan(v => v.StartRentDate).WithMessage(ValidationMessages.EndDateAfterStartDate);
    }
}
