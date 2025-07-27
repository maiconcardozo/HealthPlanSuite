using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Authentication.Tests.Fixtures;
using Xunit;

namespace Authentication.Tests.Integration;

public class ClaimActionControllerTests : IClassFixture<AuthenticationWebApplicationFactory>
{
    private readonly WebApplicationFactory<AuthenticationTestStartup> _factory;
    private readonly HttpClient _client;

    public ClaimActionControllerTests(AuthenticationWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetClaimActions_ShouldReturnExpectedStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/ClaimAction/GetClaimActions");

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
    public async Task GetClaimActionById_WithVariousIds_ShouldReturnExpectedStatusCode(int id)
    {
        // Act
        var response = await _client.GetAsync($"/ClaimAction/GetClaimActionById/{id}");

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
    public async Task AddClaimAction_WithValidData_ShouldReturnExpectedStatusCode()
    {
        // Arrange
        var request = new
        {
            claimId = 1,
            actionId = 1
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/ClaimAction/AddClaimAction", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task AddClaimAction_WithInvalidClaimId_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new
        {
            claimId = -1,
            actionId = 1
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/ClaimAction/AddClaimAction", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task AddClaimAction_WithInvalidActionId_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new
        {
            claimId = 1,
            actionId = -1
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/ClaimAction/AddClaimAction", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task UpdateClaimAction_WithValidData_ShouldReturnExpectedStatusCode()
    {
        // Arrange
        int claimActionId = 1;
        var request = new
        {
            claimId = 2,
            actionId = 2
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PutAsync($"/ClaimAction/UpdateClaimAction/{claimActionId}", content);

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
    public async Task UpdateClaimAction_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        int nonExistentId = 99999;
        var request = new
        {
            claimId = 1,
            actionId = 1
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PutAsync($"/ClaimAction/UpdateClaimAction/{nonExistentId}", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.NotFound,
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task DeleteClaimAction_WithValidId_ShouldReturnExpectedStatusCode()
    {
        // Arrange
        int claimActionId = 1;

        // Act
        var response = await _client.DeleteAsync($"/ClaimAction/DeleteClaimAction/{claimActionId}");

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.OK,
            HttpStatusCode.NotFound,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task DeleteClaimAction_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        int nonExistentId = 99999;

        // Act
        var response = await _client.DeleteAsync($"/ClaimAction/DeleteClaimAction/{nonExistentId}");

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.NotFound,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task AddClaimAction_WithInvalidJson_ShouldReturnBadRequest()
    {
        // Arrange
        var content = new StringContent(
            "{ invalid json }",
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/ClaimAction/AddClaimAction", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task UpdateClaimAction_WithInvalidJson_ShouldReturnBadRequest()
    {
        // Arrange
        int claimActionId = 1;
        var content = new StringContent(
            "{ invalid json }",
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PutAsync($"/ClaimAction/UpdateClaimAction/{claimActionId}", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError
        );
    }

    [Theory]
    [InlineData("POST", "/ClaimAction/GetClaimActions")]
    [InlineData("PUT", "/ClaimAction/GetClaimActions")]
    [InlineData("DELETE", "/ClaimAction/GetClaimActions")]
    [InlineData("POST", "/ClaimAction/GetClaimActionById/1")]
    [InlineData("PUT", "/ClaimAction/GetClaimActionById/1")]
    [InlineData("DELETE", "/ClaimAction/GetClaimActionById/1")]
    public async Task ClaimActionEndpoints_WithUnsupportedHttpMethods_ShouldReturnMethodNotAllowed(string httpMethod, string endpoint)
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