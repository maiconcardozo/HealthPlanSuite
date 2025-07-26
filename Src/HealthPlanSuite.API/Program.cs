using Microsoft.EntityFrameworkCore;
using HealthPlanSuite.Extensions;
using HealthPlanSuite.Infrastructure.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Entity Framework
builder.Services.AddDbContext<HealthPlanSuiteContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                          "Server=localhost;Database=HealthPlanSuite;Uid=root;Pwd=password;";
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Add HealthPlanSuite services
builder.Services.AddHealthPlanSuiteServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "HealthPlan Suite API", 
        Version = "v1",
        Description = "API para gerenciamento de planos de saúde, operadoras e rede credenciada"
    });
    
    // Include XML comments for better API documentation
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthPlan Suite API V1");
        c.RoutePrefix = string.Empty; // Make Swagger UI the root page
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
