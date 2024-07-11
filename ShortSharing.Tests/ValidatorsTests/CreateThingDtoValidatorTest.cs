using FluentValidation.TestHelper;
using ShortSharing.API.Dtos.Validators;
using Xunit;

namespace ShortSharing.Tests.ValidatorsTests;

public class CreateThingDtoValidatorTest
{
    private readonly CreateThingDtoValidator _validator;

    public CreateThingDtoValidatorTest()
    {
        _validator = new CreateThingDtoValidator();
    }

    [Fact]
    public void Validator_ShouldValidateCreateThingDto()
    {
        CreateThingDtoValidator validator = new();

        validator.TestValidate(SeedData.GetValidCreateThingDto())
            .ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_InvalidCreateThingDto_MissingName_ShouldHaveValidationError()
    {
        _validator.TestValidate(SeedData.GetInvalidCreateThingDto_MissingName())
            .ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_InvalidCreateThingDto_NegativePrice_ShouldHaveValidationError()
    {
        _validator.TestValidate(SeedData.GetInvalidCreateThingDto_NegativePrice())
            .ShouldHaveValidationErrorFor(x => x.Price);
    }

    [Fact]
    public void Validate_InvalidCreateThingDto_EmptyDescription_ShouldHaveValidationError()
    {
        _validator.TestValidate(SeedData.GetInvalidCreateThingDto_EmptyDescription())
           .ShouldHaveValidationErrorFor(x => x.Description);
    }
}
