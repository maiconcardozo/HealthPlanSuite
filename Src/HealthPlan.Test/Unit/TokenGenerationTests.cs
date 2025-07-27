using FluentAssertions;
using Moq;
using Xunit;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Authentication.Tests.Unit;

public class TokenGenerationTests
{
    [Fact]
    public void CreateJwtToken_WithValidClaims_ShouldGenerateValidToken()
    {
        // Arrange
        var secretKey = "this-is-a-very-long-secret-key-for-testing-jwt-tokens-generation";
        var issuer = "TestIssuer";
        var audience = "TestAudience";
        var userName = "testuser";
        var userId = "123";

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim("custom_claim", "custom_value")
        };

        // Act
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(60),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        // Assert
        tokenString.Should().NotBeNullOrEmpty();
        tokenString.Split('.').Should().HaveCount(3); // JWT has 3 parts: header.payload.signature

        // Verify token can be read back
        var readToken = tokenHandler.ReadJwtToken(tokenString);
        readToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Name && c.Value == userName);
        readToken.Claims.Should().Contain(c => c.Type == ClaimTypes.NameIdentifier && c.Value == userId);
        readToken.Claims.Should().Contain(c => c.Type == "custom_claim" && c.Value == "custom_value");
    }

    [Fact]
    public void ValidateJwtToken_WithValidToken_ShouldReturnTrueAndClaims()
    {
        // Arrange
        var secretKey = "this-is-a-very-long-secret-key-for-testing-jwt-tokens-validation";
        var issuer = "TestIssuer";
        var audience = "TestAudience";
        var userName = "testuser";

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, "123")
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(60),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero
        };

        // Act
        var result = tokenHandler.ValidateToken(tokenString, validationParameters, out var validatedToken);

        // Assert
        result.Should().NotBeNull();
        validatedToken.Should().NotBeNull();
        result.Identity?.Name.Should().Be(userName);
        result.Claims.Should().Contain(c => c.Type == ClaimTypes.Name && c.Value == userName);
    }

    [Fact]
    public void ValidateJwtToken_WithExpiredToken_ShouldThrowSecurityTokenExpiredException()
    {
        // Arrange
        var secretKey = "this-is-a-very-long-secret-key-for-testing-expired-tokens";
        var issuer = "TestIssuer";
        var audience = "TestAudience";

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "testuser"),
            new Claim(ClaimTypes.NameIdentifier, "123")
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(-1), // Expired token
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero
        };

        // Act & Assert
        Assert.Throws<SecurityTokenExpiredException>(() =>
            tokenHandler.ValidateToken(tokenString, validationParameters, out var validatedToken));
    }

    [Fact]
    public void ValidateJwtToken_WithInvalidSignature_ShouldThrowSecurityTokenInvalidSignatureException()
    {
        // Arrange
        var correctSecretKey = "this-is-the-correct-secret-key-for-testing";
        var wrongSecretKey = "this-is-a-wrong-secret-key-for-testing";
        var issuer = "TestIssuer";
        var audience = "TestAudience";

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "testuser"),
            new Claim(ClaimTypes.NameIdentifier, "123")
        };

        // Create token with correct key
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(60),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(correctSecretKey)),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        // Validate with wrong key
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(wrongSecretKey)),
            ClockSkew = TimeSpan.Zero
        };

        // Act & Assert
        Assert.Throws<SecurityTokenInvalidSignatureException>(() =>
            tokenHandler.ValidateToken(tokenString, validationParameters, out var validatedToken));
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid.token")]
    [InlineData("not.a.jwt.token.at.all")]
    public void ValidateJwtToken_WithMalformedToken_ShouldThrowException(string malformedToken)
    {
        // Arrange
        var secretKey = "this-is-a-secret-key-for-testing-malformed-tokens";
        var issuer = "TestIssuer";
        var audience = "TestAudience";

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        // Act & Assert
        Assert.ThrowsAny<Exception>(() =>
            tokenHandler.ValidateToken(malformedToken, validationParameters, out var validatedToken));
    }
}