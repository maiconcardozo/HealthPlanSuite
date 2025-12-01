using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using HealthPlan.API.Resource;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace HealthPlan.API.Swagger
{
    /// <summary>
    /// Swagger operation filter for localizing API operation documentation
    /// </summary>
    public class LocalizedSwaggerOperationFilter : IOperationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the LocalizedSwaggerOperationFilter
        /// </summary>
        /// <param name="httpContextAccessor">HTTP context accessor for accessing culture information</param>
        public LocalizedSwaggerOperationFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Applies localization to Swagger operation documentation
        /// </summary>
        /// <param name="operation">The Swagger operation</param>
        /// <param name="context">The operation filter context</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var culture = GetCurrentCulture();

            // Localize operation summary
            if (!string.IsNullOrEmpty(operation.Summary) && operation.Summary.StartsWith("ResourceAPI."))
            {
                var key = operation.Summary.Substring("ResourceAPI.".Length);
                var text = ResourceAPI.ResourceManager.GetString(key, culture);
                if (!string.IsNullOrEmpty(text))
                {
                    operation.Summary = text;
                }
            }

            // Localize operation description
            if (!string.IsNullOrEmpty(operation.Description) && operation.Description.StartsWith("ResourceAPI."))
            {
                var key = operation.Description.Substring("ResourceAPI.".Length);
                var text = ResourceAPI.ResourceManager.GetString(key, culture);
                if (!string.IsNullOrEmpty(text))
                {
                    operation.Description = text;
                }
            }

            // Localize response descriptions
            foreach (var response in operation.Responses)
            {
                if (!string.IsNullOrEmpty(response.Value.Description) && response.Value.Description.StartsWith("ResourceAPI."))
                {
                    var key = response.Value.Description.Substring("ResourceAPI.".Length);
                    var text = ResourceAPI.ResourceManager.GetString(key, culture);
                    if (!string.IsNullOrEmpty(text))
                    {
                        response.Value.Description = text;
                    }
                }
            }
        }

        private CultureInfo GetCurrentCulture()
        {
            // Try to get culture from current HTTP request context first
            if (_httpContextAccessor.HttpContext != null)
            {
                var requestCultureFeature = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
                if (requestCultureFeature?.RequestCulture?.Culture != null)
                {
                    return requestCultureFeature.RequestCulture.Culture;
                }
            }

            // Fall back to CurrentUICulture if no HTTP context or request culture available
            return CultureInfo.CurrentUICulture;
        }
    }
}