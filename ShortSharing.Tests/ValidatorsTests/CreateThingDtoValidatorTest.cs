using FluentValidation.TestHelper;
using ShortSharing.API.Dtos.ThingDtos;
using ShortSharing.API.Dtos.Validators;
using Shouldly;
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
    public void Validate_ExpectedPriceNotCorrect_ReturnsFalse()
    {
        var validator = new CreateThingDtoValidator();

        var model = new CreateThingDto()
        {
            Name = "Thing",
            Description = "valid description",
            Price = -10.0,
            CategoryId = Guid.NewGuid(),
            TypeId = Guid.NewGuid()
        };

        // Act
        var result = validator.Validate(model);

        // Assert
        result.Errors.ShouldNotBeEmpty();
        result.Errors.ShouldContain(x => x.PropertyName == nameof(model.Price));
    }

    [Fact]
    public void Validator_ShouldValidateCreateThingDto()
    {
        var validator = new CreateThingDtoValidator();

        var model = new CreateThingDto()
        {
            Name = "Thing",
            Description = "valid description",
            Price = 100.0,
            CategoryId = Guid.NewGuid(),
            TypeId = Guid.NewGuid()
        };

        // Act
        var result = validator.Validate(model);

        // Assert
        result.Errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_InvalidCreateThingDto_MissingName_ShouldHaveValidationError()
    {
        var validator = new CreateThingDtoValidator();

        var model = new CreateThingDto()
        {
            Name = string.Empty,
            Description = "valid description",
            Price = 100.0,
            CategoryId = Guid.NewGuid(),
            TypeId = Guid.NewGuid()
        };

        // Act
        var result = validator.Validate(model);

        // Assert
        result.Errors.ShouldNotBeEmpty();
        result.Errors.ShouldContain(x => x.PropertyName == nameof(model.Name));
    }

    [Fact]
    public void Validate_InvalidCreateThingDto_NegativePrice_ShouldHaveValidationError()
    {
        var validator = new CreateThingDtoValidator();

        var model = new CreateThingDto()
        {
            Name = "Thing",
            Description = "valid description",
            Price = -10.0,
            CategoryId = Guid.NewGuid(),
            TypeId = Guid.NewGuid()
        };

        // Act
        var result = validator.Validate(model);

        // Assert
        result.Errors.ShouldNotBeEmpty();
        result.Errors.ShouldContain(x => x.PropertyName == nameof(model.Price));
    }

    [Fact]
    public void Validate_InvalidCreateThingDto_EmptyDescription_ShouldHaveValidationError()
    {
        var validator = new CreateThingDtoValidator();

        var model = new CreateThingDto()
        {
            Name = "Thing",
            Description = "",
            Price = 100.0,
            CategoryId = Guid.NewGuid(),
            TypeId = Guid.NewGuid()
        };

        // Act
        var result = validator.Validate(model);

        // Assert
        result.Errors.ShouldNotBeEmpty();
        result.Errors.ShouldContain(x => x.PropertyName == nameof(model.Description));
    }

    [Fact]
    public void Validate_InvalidCreateThingDto_InvalidTypeId_ShouldHaveValidationError()
    {
        var validator = new CreateThingDtoValidator();

        var model = new CreateThingDto()
        {
            Name = "Thing",
            Description = "",
            Price = 100.0,
            CategoryId = Guid.NewGuid(),
            TypeId = Guid.Empty
        };

        // Act
        var result = validator.Validate(model);

        // Assert
        result.Errors.ShouldNotBeEmpty();
        result.Errors.ShouldContain(x => x.PropertyName == nameof(model.TypeId));
    }

    [Fact]
    public void Validate_InvalidCreateThingDto_InvalidCategoryId_ShouldHaveValidationError()
    {
        var validator = new CreateThingDtoValidator();

        var model = new CreateThingDto()
        {
            Name = "Thing",
            Description = "",
            Price = 100.0,
            CategoryId = Guid.Empty,
            TypeId = Guid.NewGuid()
        };

        // Act
        var result = validator.Validate(model);

        // Assert
        result.Errors.ShouldNotBeEmpty();
        result.Errors.ShouldContain(x => x.PropertyName == nameof(model.CategoryId));
    }
}
