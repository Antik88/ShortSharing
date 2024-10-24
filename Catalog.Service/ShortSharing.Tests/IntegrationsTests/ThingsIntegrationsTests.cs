﻿using ShortSharing.API.Dtos.ThingDtos;
using ShortSharing.Tests.Constants;
using System;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace ShortSharing.Tests.IntegrationsTests;

public class ThingsIntegrationsTests(IntegrationTestWebAppFactory factory) : IClassFixture<IntegrationTestWebAppFactory>
{
    [Fact]
    public async Task OnGetThingById_ShouldReturnThing()
    {
        // Arrange
        var testId = Seeding.iPhoneId;
        var url = $"{ApiUrls.ById}/{testId}";

        // Act
        var response = await factory.Client.GetAsync(url);
        var result = await response.Content.ReadFromJsonAsync<ThingDto>();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ThingDto>(result);
        Assert.Equal(result.Name, Seeding.iPhoneName);
    }

    [Fact]
    public async Task CreateNewThing_ShouldReturn_NewThingDto_Ok()
    {
        // Arrange
        var newThing = new CreateThingDto
        {
            Name = "Test Thing",
            Description = "This is a test thing",
            Price = 100.0,
            CategoryId = Seeding.CategoryId,
            TypeId = Seeding.TypeId,
            OwnerId = Seeding.OwnerId,
        };


        // Act
        var response = await factory.Client.PostAsJsonAsync(ApiUrls.Create, newThing);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Delete_ShouldReturn_Ok()
    {
        // Arrange

        // Act
        var response = await factory.Client.DeleteAsync($"{ApiUrls.Delete}/{Seeding.MacBookId}");

        var getDeletedResponse = await factory.Client.GetAsync($"{ApiUrls.ById}/{Seeding.MacBookId}");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(HttpStatusCode.OK, getDeletedResponse.StatusCode);
    }
}
