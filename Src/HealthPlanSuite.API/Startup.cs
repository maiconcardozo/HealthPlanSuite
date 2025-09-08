using FluentValidation;
using HealthPlanSuite.Quote.Mapping;
using HealthPlanSuite.Quote.Repository.Interface;
using HealthPlanSuite.Quote.Services.Implementation;
using HealthPlanSuite.Quote.Services.Interface;

namespace HealthPlanSuite.API
{
    /// <summary>
    /// Classe de configuração da aplicação HealthPlanSuite API
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        /// <summary>
        /// Configura os serviços da aplicação
        /// </summary>
        /// <param name="services">Coleção de serviços</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add API controllers
            services.AddControllers();
            
            // Add API documentation
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "HealthPlanSuite API",
                    Version = "v1",
                    Description = "API para gestão de planos de saúde, beneficiários e cotações"
                });
                
                // Include XML comments
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });

            // Add AutoMapper
            services.AddAutoMapper(typeof(OperadoraProfile));

            // Add FluentValidation
            services.AddValidatorsFromAssemblyContaining<Program>();

            // Register application services
            services.AddScoped<IOperadoraService, OperadoraService>();
            // services.AddScoped<ICotacaoService, CotacaoService>(); // TODO: Implement

            // Register repositories  
            // TODO: Add Entity Framework and repository implementations
            // services.AddScoped<IOperadoraRepository, OperadoraRepository>();
            // services.AddScoped<ICotacaoRepository, CotacaoRepository>();

            // Add CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            // Add health checks
            services.AddHealthChecks();
        }

        /// <summary>
        /// Configura o pipeline de middlewares
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="env">Web host environment</param>
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthPlanSuite API v1");
                    c.RoutePrefix = string.Empty; // Set Swagger UI at root
                });
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.MapControllers();
            app.MapHealthChecks("/health");

            // Redirect root to Swagger UI
            app.MapGet("/", () => Results.Redirect("/swagger"));
        }
    }
}