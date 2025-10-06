using System.Reflection;
using HealthPlan.API.Data;
using HealthPlan.API.Resource;
using HealthPlan.API.Services;
using HealthPlan.API.Swagger;
using HealthPlan.Quote.Constants;
using HealthPlan.Quote.Extensions;
using Microsoft.AspNetCore.Localization;
using Swashbuckle.AspNetCore.Filters;

namespace HealthPlan.API
{
    /// <summary>
    /// Class responsible for ASP.NET Core application initial configuration.
    /// Organizes service configuration and middleware pipeline in a structured way.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // ==============================
            // CONFIGURAÇÃO DO AMBIENTE
            // ==============================
            // Detect if running under test (xUnit, NUnit, MSTest, etc.)
            var isTest = AppDomain.CurrentDomain.GetAssemblies()
                .Any(a => a.FullName.StartsWith("xunit", StringComparison.OrdinalIgnoreCase) ||
                          a.FullName.StartsWith("nunit", StringComparison.OrdinalIgnoreCase) ||
                          a.FullName.StartsWith("Microsoft.VisualStudio.TestPlatform", StringComparison.OrdinalIgnoreCase));

            var environment = isTest
                ? ApplicationConstants.Environment.Development
                : Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? ApplicationConstants.Environment.Production;

            var appsettings = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);


            // ==============================
            // CACHE
            // ==============================
            services.AddMemoryCache();
            services.AddSingleton<IConfigurationCache, ConfigurationCache>();

            // ==============================
            // HTTP CONTEXT ACCESSOR (for Swagger localization)
            // ==============================
            services.AddHttpContextAccessor();

            // ==============================
            // LOCALIZAÇÃO
            // ==============================
            services.AddLocalization(options => options.ResourcesPath = "Resource");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en", "pt-BR" };
                options.SetDefaultCulture(supportedCultures[0])
                       .AddSupportedCultures(supportedCultures)
                       .AddSupportedUICultures(supportedCultures);

                // Adicione os providers manualmente!
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider(),
                    new AcceptLanguageHeaderRequestCultureProvider()
                };
            });

            // ==============================
            // AUTENTICAÇÃO & DOMÍNIO
            // ==============================
            services.AddAuthenticationLoginServices(HealthPlan.API.Helper.Utils.GetConnectionString(appsettings));

            // ==============================
            // CONTROLLERS & VALIDAÇÃO
            // ==============================
            services.AddControllers();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Quote.DTO.AcceptanceRulePayLoadDTO>, HealthPlan.Quote.Validators.AcceptanceRulePayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Quote.DTO.AccommodationPayLoadDTO>, HealthPlan.Quote.Validators.AccommodationPayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Quote.DTO.AgeRangePayLoadDTO>, HealthPlan.Quote.Validators.AgeRangePayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Quote.DTO.BeneficiaryPayLoadDTO>, HealthPlan.Quote.Validators.BeneficiaryPayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Quote.DTO.CompanyPayLoadDTO>, HealthPlan.Quote.Validators.CompanyPayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Quote.DTO.ProcedureCoparticipationPayLoadDTO>, HealthPlan.Quote.Validators.ProcedureCoparticipationPayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Quote.DTO.CoveragePayLoadDTO>, HealthPlan.Quote.Validators.CoveragePayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Quote.DTO.PromotionalDiscountPayLoadDTO>, HealthPlan.Quote.Validators.PromotionalDiscountPayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Quote.DTO.HealthPlanPayLoadDTO>, HealthPlan.Quote.Validators.HealthPlanPayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Quote.DTO.PlanCoveragePayLoadDTO>, HealthPlan.Quote.Validators.PlanCoveragePayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Quote.DTO.PlanPriceRangePayLoadDTO>, HealthPlan.Quote.Validators.PlanPriceRangePayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Quote.DTO.QuoteHistoryPayLoadDTO>, HealthPlan.Quote.Validators.QuoteHistoryPayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Quote.DTO.QuotePayLoadDTO>, HealthPlan.Quote.Validators.QuotePayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Quote.DTO.AdhesionFeePayLoadDTO>, HealthPlan.Quote.Validators.AdhesionFeePayloadValidator>();

            // ==============================
            // SWAGGER
            // ==============================
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                options.EnableAnnotations();
                options.ExampleFilters();

                // Filtros para internacionalização
                options.OperationFilter<LocalizedSwaggerOperationFilter>();
                options.DocumentFilter<LocalizedSwaggerDocumentFilter>();

                // Configure Swagger for Health Plan API controllers
                options.SwaggerDoc(ApplicationConstants.Api.SwaggerDefinitions.Authentication, new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Health Plan API",
                    Version = ApplicationConstants.Api.Version,
                    Description = "API for managing health plan quotes, companies, coverages, and related operations"
                });

                options.DocInclusionPredicate((docName, apiDescription) =>
                {
                    var controllerName = apiDescription.ActionDescriptor.RouteValues["controller"];
                    return docName switch
                    {
                        ApplicationConstants.Api.SwaggerDefinitions.Authentication =>
                            controllerName?.Equals("AcceptanceRule", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("Accommodation", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("AdhesionFee", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("AgeRange", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("Beneficiary", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("Company", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("CoparticipacaoProcedimento", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("ProcedureCoparticipation", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("Coverage", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("DescontoPromocional", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("PromotionalDiscount", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("HealthPlan", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("PlanCoverage", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("PrecoPlanoFaixa", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("PlanPriceRange", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("Quote", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("QuoteHistory", StringComparison.OrdinalIgnoreCase) == true ||
                            controllerName?.Equals("TaxaAdesao", StringComparison.OrdinalIgnoreCase) == true,
                        _ => false
                    };
                });
            });

            services.AddSwaggerExamplesFromAssemblyOf<SucessDetailsExample>();
            services.AddSwaggerExamplesFromAssemblyOf<ProblemDetailsBadRequestExample>();


            // ==============================
            // CORS
            // ==============================
            services.AddCors(options =>
            {
                options.AddPolicy(ApplicationConstants.Cors.AllowAllPolicy, policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ==============================
            // DEV/DEBUG
            // ==============================
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // ==============================
            // SEGURANÇA & STATIC FILES
            // ==============================
            app.UseMiddleware<HealthPlan.API.Middleware.SwaggerAuthMiddleware>();
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            // ==============================
            // CORS
            // ==============================
            app.UseCors(ApplicationConstants.Cors.AllowAllPolicy);

            // ==============================
            // CULTURE COOKIE FROM QUERY (deve vir ANTES da localização!)
            // ==============================
            app.UseMiddleware<HealthPlan.API.Middleware.CultureCookieFromQueryMiddleware>();

            // ==============================
            // LOCALIZAÇÃO (deve vir ANTES do Swagger!)
            // ==============================
            app.UseRequestLocalization();

            // ==============================
            // SWAGGER
            // ==============================
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    ApplicationConstants.Api.SwaggerDefinitions.AuthenticationEndpoint,
                    "Health Plan API"
                );
                options.RoutePrefix = ApplicationConstants.Api.EmptyRoutePrefix;
                options.InjectStylesheet(ApplicationConstants.Api.CustomStylePath);
            });

            // ==============================
            // EXCEÇÕES
            // ==============================
            app.UseMiddleware<HealthPlan.API.Middleware.ExceptionHandlingMiddleware>();

            // ==============================
            // ROTEAMENTO & AUTORIZAÇÃO
            // ==============================
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}