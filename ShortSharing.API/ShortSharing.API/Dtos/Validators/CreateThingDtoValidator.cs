using FluentValidation;
using ShortSharing.API.Dtos.ThingDtos;

namespace ShortSharing.API.Dtos.Validators;

public class CreateThingDtoValidator : AbstractValidator<CreateThingDto>
{
    public CreateThingDtoValidator()
    {
        RuleFor(x => x.Price).NotNull().GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.TypeId).NotEmpty();
    }
}
