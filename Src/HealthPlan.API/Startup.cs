using System.Reflection;
using System.Text;
using Authentication.API.Services;
using HealthPlan.API.Swagger;
using HealthPlan.Application.Behaviors;
using HealthPlan.Application.Commands;
using HealthPlan.Application.Constants;
using HealthPlan.Shared.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.Filters;

namespace HealthPlan.API
{
    /// <summary>
    /// Classe responsável pela configuração inicial da aplicação ASP.NET Core.
    /// Organiza a configuração de serviços e do pipeline de middlewares de forma estruturada.
    /// </summary>
    public class Startup
    {
        private static readonly string[] configureOptions = new[] { "en", "pt-BR" };

        public void ConfigureServices(IServiceCollection services)
        {
            var appsettings = DataConfigurationHelper.BuildConfiguration();

            // ==============================
            // CACHE
            // ==============================
            services.AddMemoryCache();
            services.AddSingleton<IConfigurationCache, ConfigurationCache>();

            // ==============================
            // HTTP CONTEXT ACCESSOR (para localização do Swagger)
            // ==============================
            services.AddHttpContextAccessor();

            // ==============================
            // LOCALIZAÇÃO
            // ==============================
            services.AddLocalization(options => options.ResourcesPath = "Resource");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var culturasSuportadas = configureOptions;
                options.SetDefaultCulture(culturasSuportadas[0])
                       .AddSupportedCultures(culturasSuportadas)
                       .AddSupportedUICultures(culturasSuportadas);
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider(),
                    new AcceptLanguageHeaderRequestCultureProvider(),
                };
            });

            // ==============================
            // AUTENTICAÇÃO JWT
            // ==============================
            var jwtSettings = appsettings.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey não configurada");

            if ((secretKey.Contains("REPLACE-WITH") || secretKey.Length < 32))
            {
                throw new InvalidOperationException(
                    "A chave secreta do JWT deve ser substituída por um valor seguro. " +
                    "Use variáveis de ambiente (JwtSettings__SecretKey) ou Azure Key Vault. " +
                    "A chave deve ter pelo menos 32 caracteres.");
            }

            var key = Encoding.UTF8.GetBytes(secretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = !ambiente.IsDevelopment();
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
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
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Application.DTOs.AcceptanceRulePayLoadDTO>, HealthPlan.Application.Validators.AcceptanceRulePayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Application.DTOs.AccommodationPayLoadDTO>, HealthPlan.Application.Validators.AccommodationPayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Application.DTOs.AgeRangePayLoadDTO>, HealthPlan.Application.Validators.AgeRangePayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Application.DTOs.BeneficiaryPayLoadDTO>, HealthPlan.Application.Validators.BeneficiaryPayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Application.DTOs.CompanyPayLoadDTO>, HealthPlan.Application.Validators.CompanyPayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Application.DTOs.ProcedureCoparticipationPayLoadDTO>, HealthPlan.Application.Validators.ProcedureCoparticipationPayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Application.DTOs.CoveragePayLoadDTO>, HealthPlan.Application.Validators.CoveragePayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Application.DTOs.PromotionalDiscountPayLoadDTO>, HealthPlan.Application.Validators.PromotionalDiscountPayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Application.DTOs.HealthPlanPayLoadDTO>, HealthPlan.Application.Validators.HealthPlanPayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Application.DTOs.PlanCoveragePayLoadDTO>, HealthPlan.Application.Validators.PlanCoveragePayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Application.DTOs.PlanPriceRangePayLoadDTO>, HealthPlan.Application.Validators.PlanPriceRangePayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Application.DTOs.QuoteHistoryPayLoadDTO>, HealthPlan.Application.Validators.QuoteHistoryPayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Application.DTOs.QuotePayLoadDTO>, HealthPlan.Application.Validators.QuotePayloadValidator>();
            services.AddTransient<FluentValidation.IValidator<HealthPlan.Application.DTOs.AdhesionFeePayLoadDTO>, HealthPlan.Application.Validators.AdhesionFeePayloadValidator>();

            // ==============================
            // MEDIATR & BEHAVIORS
            // ==============================
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateQuoteCommand).Assembly);
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

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

                // Configura o Swagger para os controllers da API Health Plan
                options.SwaggerDoc(ApplicationConstants.Api.SwaggerDefinitions.Authentication, new OpenApiInfo
                {
                    Title = "Health Plan API",
                    Version = ApplicationConstants.Api.Version,
                    Description = "API para gestão de cotações, empresas, coberturas e operações relacionadas ao plano de saúde"
                });

                // Adiciona autenticação JWT ao Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header usando o esquema Bearer. Digite 'Bearer' [espaço] e então seu token.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
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
            // SEGURANÇA & ARQUIVOS ESTÁTICOS
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
