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
        CreateThingDtoValidator validator = new();

        validator.TestValidate(SeedData.GetInvalidCreateThingDto_MissingName())
            .ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_InvalidCreateThingDto_NegativePrice_ShouldHaveValidationError()
    {
        CreateThingDtoValidator validator = new();

        validator.TestValidate(SeedData.GetInvalidCreateThingDto_NegativePrice())
            .ShouldHaveValidationErrorFor(x => x.Price);
    }

    [Fact]
    public void Validate_InvalidCreateThingDto_EmptyDescription_ShouldHaveValidationError()
    {
        CreateThingDtoValidator validator = new();

        validator.TestValidate(SeedData.GetInvalidCreateThingDto_EmptyDescription())
           .ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void Validate_InvalidCreateThingDto_InvalidTypeId_ShouldHaveValidationError()
    {
        CreateThingDtoValidator validator = new();
        validator.TestValidate(SeedData.GetInvalidCreateThingDto_InvalidTypeId())
           .ShouldHaveValidationErrorFor(x => x.TypeId);
    }

    [Fact]
    public void Validate_InvalidCreateThingDto_InvalidCategoryId_ShouldHaveValidationError()
    {
        CreateThingDtoValidator validator = new();
        validator.TestValidate(SeedData.GetInvalidCreateThingDto_InvalidCategoryId())
           .ShouldHaveValidationErrorFor(x => x.CategoryId);
    }
}
