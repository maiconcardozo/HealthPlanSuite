using Authentication.API.Resource;
using Authentication.API.Swagger;
using Authentication.Login.Domain.Implementation;
using Authentication.Login.Domain.Interface;
using Authentication.Login.DTO;
using Authentication.Login.Extensions;
using Authentication.Login.Util;
using HealthPlan.Quote.Extensions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using Authentication.API.Data;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

var appsettings = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

builder.Services.AddAuthenticationLoginServices(Authentication.API.Helper.Utils.GetConnectionString(appsettings));
builder.Services.AddHealthPlanServices(); // Add health plan services
builder.Services.AddControllers();
builder.Services.AddTransient<FluentValidation.IValidator<AccountPayLoadDTO>, AccountPayloadValidator>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    options.EnableAnnotations();
    options.ExampleFilters();
    options.OperationFilter<LocalizedSwaggerOperationFilter>();

    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Authentication API",
        Version = "v1",
        Description = ResourceAPI.APIManagement
    });
});

builder.Services.Configure<JwtSettings>(Authentication.API.Helper.Utils.GetJwtSettings(appsettings));
builder.Services.AddSingleton<IJwtSettings>(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);

builder.Services.AddSwaggerExamplesFromAssemblyOf<SuccessDetailsExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<ProblemDetailsBadRequestExample>();

var app = builder.Build();

app.UseMiddleware<Authentication.API.Middleware.SwaggerAuthMiddleware>();

app.UseStaticFiles();
app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication API V1");
    options.RoutePrefix = string.Empty;
    options.InjectStylesheet("/Style/custom-swagger.css");
});

app.UseHttpsRedirection();

app.UseMiddleware<Authentication.API.Middleware.ExceptionHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();