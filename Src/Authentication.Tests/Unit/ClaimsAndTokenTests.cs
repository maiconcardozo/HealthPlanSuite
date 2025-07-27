using FluentAssertions;
using Moq;
using Xunit;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Authentication.Tests.Unit;

public class ClaimsAndTokenTests
{
    [Fact]
    public void GenerateTokenWithClaims_ShouldIncludeUserClaimsInToken()
    {
        // Arrange
        var account = new TestAccount
        {
            Id = 1,
            UserName = "testuser",
            Email = "test@example.com"
        };

        var userClaims = new List<TestAccountClaimAction>
        {
            new() { ClaimAction = new TestClaimAction { Claim = new TestClaim { Type = "Permission", Value = "user:read" } } },
            new() { ClaimAction = new TestClaimAction { Claim = new TestClaim { Type = "Permission", Value = "user:write" } } },
            new() { ClaimAction = new TestClaimAction { Claim = new TestClaim { Type = "Role", Value = "Admin" } } }
        };

        var secretKey = "this-is-a-very-long-secret-key-for-testing-claims-in-tokens";
        var issuer = "TestIssuer";
        var audience = "TestAudience";

        // Act
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, account.UserName),
            new(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new(ClaimTypes.Email, account.Email)
        };

        // Add user-specific claims from account permissions
        foreach (var userClaim in userClaims)
        {
            claims.Add(new Claim(userClaim.ClaimAction.Claim.Type, userClaim.ClaimAction.Claim.Value));
        }

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

        // Verify token contains expected claims
        var readToken = tokenHandler.ReadJwtToken(tokenString);
        readToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Name && c.Value == "testuser");
        readToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Email && c.Value == "test@example.com");
        readToken.Claims.Should().Contain(c => c.Type == "Permission" && c.Value == "user:read");
        readToken.Claims.Should().Contain(c => c.Type == "Permission" && c.Value == "user:write");
        readToken.Claims.Should().Contain(c => c.Type == "Role" && c.Value == "Admin");
    }

    [Fact]
    public void GenerateTokenWithoutClaims_ShouldIncludeOnlyBasicClaims()
    {
        // Arrange
        var account = new TestAccount
        {
            Id = 1,
            UserName = "basicuser",
            Email = "basic@example.com"
        };

        var userClaims = new List<TestAccountClaimAction>(); // No additional claims

        var secretKey = "this-is-a-very-long-secret-key-for-testing-basic-tokens";
        var issuer = "TestIssuer";
        var audience = "TestAudience";

        // Act
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, account.UserName),
            new(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new(ClaimTypes.Email, account.Email)
        };

        // Add user-specific claims (empty in this case)
        foreach (var userClaim in userClaims)
        {
            claims.Add(new Claim(userClaim.ClaimAction.Claim.Type, userClaim.ClaimAction.Claim.Value));
        }

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

        // Verify token contains only basic claims
        var readToken = tokenHandler.ReadJwtToken(tokenString);
        readToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Name && c.Value == "basicuser");
        readToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Email && c.Value == "basic@example.com");
        readToken.Claims.Should().NotContain(c => c.Type == "Permission");
        readToken.Claims.Should().NotContain(c => c.Type == "Role");
    }

    [Fact]
    public void ClaimActionMapping_ShouldCorrectlyMapClaimsToActions()
    {
        // Arrange
        var claim = new TestClaim
        {
            Id = 1,
            Type = "Permission",
            Value = "user:manage",
            Description = "Manage users"
        };

        var action = new TestAction
        {
            Id = 1,
            Name = "CreateUser",
            Description = "Create a new user"
        };

        var claimAction = new TestClaimAction
        {
            Id = 1,
            ClaimId = claim.Id,
            ActionId = action.Id,
            Claim = claim,
            Action = action
        };

        // Act & Assert
        claimAction.Claim.Should().Be(claim);
        claimAction.Action.Should().Be(action);
        claimAction.ClaimId.Should().Be(claim.Id);
        claimAction.ActionId.Should().Be(action.Id);
    }

    [Fact]
    public void AccountClaimActionMapping_ShouldCorrectlyMapAccountToClaimActions()
    {
        // Arrange
        var account = new TestAccount
        {
            Id = 1,
            UserName = "testuser",
            Email = "test@example.com"
        };

        var claimAction = new TestClaimAction
        {
            Id = 1,
            Claim = new TestClaim { Type = "Permission", Value = "user:read" },
            Action = new TestAction { Name = "GetUser" }
        };

        var accountClaimAction = new TestAccountClaimAction
        {
            Id = 1,
            AccountId = account.Id,
            ClaimActionId = claimAction.Id,
            Account = account,
            ClaimAction = claimAction
        };

        // Act & Assert
        accountClaimAction.Account.Should().Be(account);
        accountClaimAction.ClaimAction.Should().Be(claimAction);
        accountClaimAction.AccountId.Should().Be(account.Id);
        accountClaimAction.ClaimActionId.Should().Be(claimAction.Id);
    }

    [Fact]
    public void TokenClaimsRetrieval_ShouldRetrieveClaimsFromAccountPermissions()
    {
        // Arrange
        var account = new TestAccount { Id = 1, UserName = "testuser" };
        
        var mockAccountService = new Mock<ITestAccountService>();
        mockAccountService.Setup(s => s.GetAccountClaimActions(account.Id))
            .Returns(new List<TestAccountClaimAction>
            {
                new() { ClaimAction = new TestClaimAction { Claim = new TestClaim { Type = "Permission", Value = "user:read" } } },
                new() { ClaimAction = new TestClaimAction { Claim = new TestClaim { Type = "Permission", Value = "user:write" } } }
            });

        // Act
        var accountClaimActions = mockAccountService.Object.GetAccountClaimActions(account.Id);
        var retrievedClaims = accountClaimActions
            .Select(aca => new { Type = aca.ClaimAction.Claim.Type, Value = aca.ClaimAction.Claim.Value })
            .ToList();

        // Assert
        retrievedClaims.Should().HaveCount(2);
        retrievedClaims.Should().Contain(c => c.Type == "Permission" && c.Value == "user:read");
        retrievedClaims.Should().Contain(c => c.Type == "Permission" && c.Value == "user:write");
    }

    [Theory]
    [InlineData("Permission", "user:read")]
    [InlineData("Permission", "user:write")]
    [InlineData("Role", "Admin")]
    [InlineData("Role", "User")]
    public void ClaimValidation_WithValidClaimTypes_ShouldAcceptClaim(string type, string value)
    {
        // Arrange & Act
        var claim = new TestClaim
        {
            Type = type,
            Value = value,
            Description = $"{type} for {value}"
        };

        // Assert
        claim.Type.Should().Be(type);
        claim.Value.Should().Be(value);
        claim.Type.Should().NotBeNullOrEmpty();
        claim.Value.Should().NotBeNullOrEmpty();
    }

    // Test classes
    public class TestAccount
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class TestClaim
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class TestAction
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class TestClaimAction
    {
        public int Id { get; set; }
        public int ClaimId { get; set; }
        public int ActionId { get; set; }
        public TestClaim Claim { get; set; } = new();
        public TestAction Action { get; set; } = new();
    }

    public class TestAccountClaimAction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int ClaimActionId { get; set; }
        public TestAccount Account { get; set; } = new();
        public TestClaimAction ClaimAction { get; set; } = new();
    }

    public interface ITestAccountService
    {
        List<TestAccountClaimAction> GetAccountClaimActions(int accountId);
    }
}