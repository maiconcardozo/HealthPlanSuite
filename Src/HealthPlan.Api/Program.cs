using HealthPlan.API.Resource;
using HealthPlan.API.Swagger;
using HealthPlan.Quote.Extensions;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using HealthPlan.API.Data;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

var appsettings = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

builder.Services.AddHealthPlanServices(); // Add health plan services
builder.Services.AddControllers();
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
        Title = "HealthPlan API",
        Version = "v1",
        Description = ResourceAPI.APIManagement
    });
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<SuccessDetailsExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<ProblemDetailsBadRequestExample>();

var app = builder.Build();

app.UseMiddleware<HealthPlan.API.Middleware.SwaggerAuthMiddleware>();

app.UseStaticFiles();
app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthPlan API V1");
    options.RoutePrefix = string.Empty;
    options.InjectStylesheet("/Style/custom-swagger.css");
});

app.UseHttpsRedirection();

app.UseMiddleware<HealthPlan.API.Middleware.ExceptionHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();