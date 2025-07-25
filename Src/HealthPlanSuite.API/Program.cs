using HealthPlanSuite.API.Resource;
using HealthPlanSuite.API.Swagger;
using HealthPlanSuite.Core.Extensions;
using HealthPlanSuite.Core.DTO;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using HealthPlanSuite.API.Data;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

var appsettings = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

// Add HealthPlanSuite services
builder.Services.AddHealthPlanSuiteCore(HealthPlanSuite.API.Helper.Utils.GetConnectionString(appsettings));

builder.Services.AddControllers();

// Add FluentValidation
builder.Services.AddTransient<FluentValidation.IValidator<OperadoraPayLoadDTO>, HealthPlanSuite.API.Validators.OperadoraPayloadValidator>();

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
        Title = "HealthPlanSuite API",
        Version = "v1",
        Description = ResourceAPI.APIManagement
    });
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<SuccessDetailsExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<ProblemDetailsBadRequestExample>();

var app = builder.Build();

app.UseMiddleware<HealthPlanSuite.API.Middleware.SwaggerAuthMiddleware>();

app.UseStaticFiles();
app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthPlanSuite API V1");
    options.RoutePrefix = string.Empty;
    options.InjectStylesheet("/Style/custom-swagger.css");
});

app.UseHttpsRedirection();

app.UseMiddleware<HealthPlanSuite.API.Middleware.ExceptionHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();