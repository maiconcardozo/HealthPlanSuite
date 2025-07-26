# 🔐 JWT Authentication Pattern - Implementação Completa

Este documento detalha como implementar autenticação JWT seguindo os padrões estabelecidos no projeto.

## 📋 Componentes Necessários

### 1. **Entidade JwtSettings**
```csharp
// Domain/Implementation/JwtSettings.cs
using [SeuProjeto].Domain.Interface;

namespace [SeuProjeto].Domain.Implementation
{
    public class JwtSettings : IJwtSettings
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public int ExpirationMinutes { get; set; } = 60;
    }
}

// Domain/Interface/IJwtSettings.cs
namespace [SeuProjeto].Domain.Interface
{
    public interface IJwtSettings
    {
        string Issuer { get; set; }
        string Audience { get; set; }
        string SecretKey { get; set; }
        int ExpirationMinutes { get; set; }
    }
}
```

### 2. **Entidade Token**
```csharp
// Domain/Implementation/Token.cs
using [SeuProjeto].Domain.Interface;

namespace [SeuProjeto].Domain.Implementation
{
    public class Token : IToken
    {
        public string AccessToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresIn { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        
        public Token() { }
        
        public Token(string accessToken, int expiresIn, string userName)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
            UserName = userName;
            ExpirationDate = DateTime.UtcNow.AddSeconds(expiresIn);
        }
    }
}

// Domain/Interface/IToken.cs
namespace [SeuProjeto].Domain.Interface
{
    public interface IToken
    {
        string AccessToken { get; set; }
        string TokenType { get; set; }
        int ExpiresIn { get; set; }
        string UserName { get; set; }
        DateTime ExpirationDate { get; set; }
    }
}
```

### 3. **AuthenticationService**
```csharp
// Services/Implementation/AuthenticationService.cs
using [SeuProjeto].Domain.Implementation;
using [SeuProjeto].Domain.Interface;
using [SeuProjeto].Services.Interface;
using [SeuProjeto].UnitOfWork.Interface;
using [SeuProjeto].DTO;
using Foundation.Base.Util;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace [SeuProjeto].Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly I[SeuProjeto]UnitOfWork _unitOfWork;
        private readonly IJwtSettings _jwtSettings;
        
        public AuthenticationService(
            I[SeuProjeto]UnitOfWork unitOfWork,
            IJwtSettings jwtSettings)
        {
            _unitOfWork = unitOfWork;
            _jwtSettings = jwtSettings;
        }
        
        public async Task<TokenResponseDTO> AuthenticateAsync(LoginRequestDTO request)
        {
            // Buscar usuário
            var account = await _unitOfWork.AccountRepository.GetByUserNameAsync(request.UserName);
            
            if (account == null)
                throw new UnauthorizedAccessException("Usuário não encontrado");
            
            // Verificar senha
            if (!account.VerifyPassword(request.Password))
                throw new UnauthorizedAccessException("Senha inválida");
            
            // Gerar token
            var token = GenerateJwtToken(account);
            
            return new TokenResponseDTO
            {
                AccessToken = token.AccessToken,
                TokenType = token.TokenType,
                ExpiresIn = token.ExpiresIn,
                UserName = token.UserName
            };
        }
        
        public async Task<AccountResponseDTO> CreateUserAsync(CreateUserRequestDTO request)
        {
            // Verificar se usuário já existe
            var existingAccount = await _unitOfWork.AccountRepository.GetByUserNameAsync(request.UserName);
            if (existingAccount != null)
                throw new InvalidOperationException("Usuário já existe");
            
            // Criar nova conta
            var account = new Account(request.UserName, request.Password);
            
            await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.CommitAsync();
            
            return new AccountResponseDTO
            {
                Id = account.Id,
                UserName = account.UserName,
                CreatedAt = account.CreatedAt
            };
        }
        
        private Token GenerateJwtToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.UserName),
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, account.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            // Adicionar claims do RBAC se necessário
            // var userClaims = await GetUserClaimsAsync(account.Id);
            // claims.AddRange(userClaims);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            
            return new Token(
                tokenString,
                _jwtSettings.ExpirationMinutes * 60,
                account.UserName);
        }
        
        public async Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
                
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
                
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = false // Não validar expiração para refresh
                };
                
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
```

### 4. **Interface AuthenticationService**
```csharp
// Services/Interface/IAuthenticationService.cs
using [SeuProjeto].DTO;
using System.Security.Claims;

namespace [SeuProjeto].Services.Interface
{
    public interface IAuthenticationService
    {
        Task<TokenResponseDTO> AuthenticateAsync(LoginRequestDTO request);
        Task<AccountResponseDTO> CreateUserAsync(CreateUserRequestDTO request);
        Task<bool> ValidateTokenAsync(string token);
        ClaimsPrincipal? GetPrincipalFromToken(string token);
    }
}
```

### 5. **DTOs de Autenticação**
```csharp
// DTO/LoginRequestDTO.cs
using System.ComponentModel.DataAnnotations;

namespace [SeuProjeto].DTO
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Nome de usuário é obrigatório")]
        public string UserName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Password { get; set; } = string.Empty;
    }
}

// DTO/TokenResponseDTO.cs
namespace [SeuProjeto].DTO
{
    public class TokenResponseDTO
    {
        public string AccessToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresIn { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<string> Claims { get; set; } = new();
    }
}

// DTO/CreateUserRequestDTO.cs
using System.ComponentModel.DataAnnotations;

namespace [SeuProjeto].DTO
{
    public class CreateUserRequestDTO
    {
        [Required(ErrorMessage = "Nome de usuário é obrigatório")]
        [StringLength(50, ErrorMessage = "Nome de usuário deve ter no máximo 50 caracteres")]
        public string UserName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 100 caracteres")]
        public string Password { get; set; } = string.Empty;
        
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }
    }
}

// DTO/AccountResponseDTO.cs
namespace [SeuProjeto].DTO
{
    public class AccountResponseDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
```

### 6. **AuthenticationController**
```csharp
// Controllers/AuthenticationController.cs
using [SeuProjeto].Services.Interface;
using [SeuProjeto].DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Foundation.Base.Util;

namespace [SeuProjeto].API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Autenticar usuário e gerar token JWT")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TokenResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(request, serviceProvider, this);
            if (validationResult != null) return validationResult;
            
            try
            {
                var token = await _authenticationService.AuthenticateAsync(request);
                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ProblemDetails
                {
                    Status = 401,
                    Title = "Falha na autenticação",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Erro interno do servidor",
                    Detail = ex.Message
                });
            }
        }
        
        [HttpPost("register")]
        [SwaggerOperation(Summary = "Registrar novo usuário")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(AccountResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Register([FromBody] CreateUserRequestDTO request, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(request, serviceProvider, this);
            if (validationResult != null) return validationResult;
            
            try
            {
                var account = await _authenticationService.CreateUserAsync(request);
                return CreatedAtAction(nameof(GetUserInfo), new { id = account.Id }, account);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Erro na criação do usuário",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Erro interno do servidor",
                    Detail = ex.Message
                });
            }
        }
        
        [HttpGet("me")]
        [Authorize]
        [SwaggerOperation(Summary = "Obter informações do usuário atual")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AccountResponseDTO))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                var userName = User.Identity?.Name;
                if (string.IsNullOrEmpty(userName))
                    return Unauthorized();
                
                // Implementar busca do usuário atual
                // var user = await _userService.GetByUserNameAsync(userName);
                // return Ok(user);
                
                return Ok(new { UserName = userName, Message = "Usuário autenticado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Erro interno do servidor",
                    Detail = ex.Message
                });
            }
        }
        
        [HttpPost("validate-token")]
        [SwaggerOperation(Summary = "Validar token JWT")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateToken([FromBody] ValidateTokenRequestDTO request)
        {
            try
            {
                var isValid = await _authenticationService.ValidateTokenAsync(request.Token);
                
                if (isValid)
                    return Ok(new { IsValid = true, Message = "Token válido" });
                
                return BadRequest(new { IsValid = false, Message = "Token inválido" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Erro interno do servidor",
                    Detail = ex.Message
                });
            }
        }
    }
}

// DTO adicional para validação de token
public class ValidateTokenRequestDTO
{
    [Required]
    public string Token { get; set; } = string.Empty;
}
```

### 7. **Configuração JWT no Program.cs**
```csharp
// No Program.cs, adicione a configuração JWT:
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// Configurar JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);

var jwtSettingsValues = jwtSettings.Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettingsValues.SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Para desenvolvimento
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettingsValues.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettingsValues.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Registrar JwtSettings como serviço
builder.Services.AddSingleton<IJwtSettings>(jwtSettingsValues);
```

### 8. **Configuração Swagger com JWT**
```csharp
// Adicione ao Program.cs na configuração do Swagger:
builder.Services.AddSwaggerGen(options =>
{
    // Configuração JWT no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
```

### 9. **Middleware de Autorização Customizado (Opcional)**
```csharp
// Middleware/JwtMiddleware.cs
using [SeuProjeto].Services.Interface;

namespace [SeuProjeto].API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task Invoke(HttpContext context, IAuthenticationService authService)
        {
            var token = context.Request.Headers["Authorization"]
                .FirstOrDefault()?.Split(" ").Last();
            
            if (!string.IsNullOrEmpty(token))
            {
                var principal = authService.GetPrincipalFromToken(token);
                if (principal != null)
                {
                    context.User = principal;
                }
            }
            
            await _next(context);
        }
    }
}

// Registre no Program.cs:
app.UseMiddleware<JwtMiddleware>();
```

## 🔐 Exemplo de Uso

### Login e Obtenção de Token:
```bash
curl -X POST "https://localhost:7001/Authentication/login" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "admin",
    "password": "password123"
  }'
```

### Resposta:
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "tokenType": "Bearer",
  "expiresIn": 3600,
  "userName": "admin",
  "claims": []
}
```

### Uso do Token em Requisições:
```bash
curl -X GET "https://localhost:7001/Authentication/me" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

Este padrão garante autenticação JWT segura e escalável!