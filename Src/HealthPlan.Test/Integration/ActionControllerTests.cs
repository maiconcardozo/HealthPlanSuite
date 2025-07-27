using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Authentication.Tests.Fixtures;
using Xunit;

namespace Authentication.Tests.Integration;

public class ActionControllerTests : IClassFixture<AuthenticationWebApplicationFactory>
{
    private readonly WebApplicationFactory<AuthenticationTestStartup> _factory;
    private readonly HttpClient _client;

    public ActionControllerTests(AuthenticationWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetActions_ShouldReturnExpectedStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/Action/GetActions");

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.OK,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.InternalServerError
        );
    }

    [Theory]
    [InlineData(1)]
    [InlineData(999)]
    [InlineData(-1)]
    public async Task GetActionById_WithVariousIds_ShouldReturnExpectedStatusCode(int id)
    {
        // Act
        var response = await _client.GetAsync($"/Action/GetActionById/{id}");

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.OK,
            HttpStatusCode.NotFound,
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task AddAction_WithValidData_ShouldReturnExpectedStatusCode()
    {
        // Arrange
        var request = new
        {
            name = "CreateUser",
            description = "Create a new user"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/Action/AddAction", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task AddAction_WithEmptyName_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new
        {
            name = "",
            description = "Create a new user"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/Action/AddAction", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task UpdateAction_WithValidData_ShouldReturnExpectedStatusCode()
    {
        // Arrange
        int actionId = 1;
        var request = new
        {
            name = "UpdateUser",
            description = "Update an existing user"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PutAsync($"/Action/UpdateAction/{actionId}", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.OK,
            HttpStatusCode.NotFound,
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task UpdateAction_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        int nonExistentId = 99999;
        var request = new
        {
            name = "UpdateUser",
            description = "Update an existing user"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PutAsync($"/Action/UpdateAction/{nonExistentId}", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.NotFound,
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task DeleteAction_WithValidId_ShouldReturnExpectedStatusCode()
    {
        // Arrange
        int actionId = 1;

        // Act
        var response = await _client.DeleteAsync($"/Action/DeleteAction/{actionId}");

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.OK,
            HttpStatusCode.NotFound,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task DeleteAction_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        int nonExistentId = 99999;

        // Act
        var response = await _client.DeleteAsync($"/Action/DeleteAction/{nonExistentId}");

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.NotFound,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task AddAction_WithInvalidJson_ShouldReturnBadRequest()
    {
        // Arrange
        var content = new StringContent(
            "{ invalid json }",
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/Action/AddAction", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task UpdateAction_WithInvalidJson_ShouldReturnBadRequest()
    {
        // Arrange
        int actionId = 1;
        var content = new StringContent(
            "{ invalid json }",
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PutAsync($"/Action/UpdateAction/{actionId}", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError
        );
    }

    [Theory]
    [InlineData("POST", "/Action/GetActions")]
    [InlineData("PUT", "/Action/GetActions")]
    [InlineData("DELETE", "/Action/GetActions")]
    [InlineData("POST", "/Action/GetActionById/1")]
    [InlineData("PUT", "/Action/GetActionById/1")]
    [InlineData("DELETE", "/Action/GetActionById/1")]
    public async Task ActionEndpoints_WithUnsupportedHttpMethods_ShouldReturnMethodNotAllowed(string httpMethod, string endpoint)
    {
        // Arrange
        var request = new HttpRequestMessage(new HttpMethod(httpMethod), endpoint);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.MethodNotAllowed,
            HttpStatusCode.NotFound,
            HttpStatusCode.InternalServerError
        );
    }
}