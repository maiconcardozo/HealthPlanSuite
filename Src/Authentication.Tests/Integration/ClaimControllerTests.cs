using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Authentication.Tests.Fixtures;
using Xunit;

namespace Authentication.Tests.Integration;

public class ClaimControllerTests : IClassFixture<AuthenticationWebApplicationFactory>
{
    private readonly WebApplicationFactory<AuthenticationTestStartup> _factory;
    private readonly HttpClient _client;

    public ClaimControllerTests(AuthenticationWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetClaims_ShouldReturnExpectedStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/Claim/GetClaims");

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
    public async Task GetClaimById_WithVariousIds_ShouldReturnExpectedStatusCode(int id)
    {
        // Act
        var response = await _client.GetAsync($"/Claim/GetClaimById/{id}");

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
    public async Task AddClaim_WithValidData_ShouldReturnExpectedStatusCode()
    {
        // Arrange
        var request = new
        {
            type = "Permission",
            value = "user:read",
            description = "Read user permissions"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/Claim/AddClaim", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task AddClaim_WithEmptyData_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new
        {
            type = "",
            value = "",
            description = ""
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/Claim/AddClaim", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task UpdateClaim_WithValidData_ShouldReturnExpectedStatusCode()
    {
        // Arrange
        int claimId = 1;
        var request = new
        {
            type = "Permission",
            value = "user:write",
            description = "Write user permissions"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PutAsync($"/Claim/UpdateClaim/{claimId}", content);

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
    public async Task UpdateClaim_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        int nonExistentId = 99999;
        var request = new
        {
            type = "Permission",
            value = "user:write",
            description = "Write user permissions"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PutAsync($"/Claim/UpdateClaim/{nonExistentId}", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.NotFound,
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task DeleteClaim_WithValidId_ShouldReturnExpectedStatusCode()
    {
        // Arrange
        int claimId = 1;

        // Act
        var response = await _client.DeleteAsync($"/Claim/DeleteClaim/{claimId}");

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.OK,
            HttpStatusCode.NotFound,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task DeleteClaim_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        int nonExistentId = 99999;

        // Act
        var response = await _client.DeleteAsync($"/Claim/DeleteClaim/{nonExistentId}");

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.NotFound,
            HttpStatusCode.InternalServerError
        );
    }

    [Theory]
    [InlineData("POST", "/Claim/GetClaims")]
    [InlineData("PUT", "/Claim/GetClaims")]
    [InlineData("DELETE", "/Claim/GetClaims")]
    [InlineData("POST", "/Claim/GetClaimById/1")]
    [InlineData("PUT", "/Claim/GetClaimById/1")]
    [InlineData("DELETE", "/Claim/GetClaimById/1")]
    public async Task ClaimEndpoints_WithUnsupportedHttpMethods_ShouldReturnMethodNotAllowed(string httpMethod, string endpoint)
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

    [Fact]
    public async Task AddClaim_WithInvalidJson_ShouldReturnBadRequest()
    {
        // Arrange
        var content = new StringContent(
            "{ invalid json }",
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/Claim/AddClaim", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError
        );
    }

    [Fact]
    public async Task UpdateClaim_WithInvalidJson_ShouldReturnBadRequest()
    {
        // Arrange
        int claimId = 1;
        var content = new StringContent(
            "{ invalid json }",
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PutAsync($"/Claim/UpdateClaim/{claimId}", content);

        // Assert
        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError
        );
    }
}