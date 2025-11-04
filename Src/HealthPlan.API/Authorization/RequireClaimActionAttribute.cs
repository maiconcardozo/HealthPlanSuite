using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HealthPlan.API.Authorization
{
    /// <summary>
    /// Authorization attribute that requires a specific claim and action combination.
    /// This attribute checks if the authenticated user has the required claim-action permission.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class RequireClaimActionAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string? _claim;
        private readonly string? _action;

        /// <summary>
        /// Initializes a new instance of RequireClaimActionAttribute with specific claim and action.
        /// </summary>
        /// <param name="claim">The claim (resource) name required</param>
        /// <param name="action">The action (operation) name required</param>
        public RequireClaimActionAttribute(string claim, string action)
        {
            _claim = claim;
            _action = action;
        }

        /// <summary>
        /// Initializes a new instance of RequireClaimActionAttribute that will auto-detect claim and action
        /// based on controller name and HTTP method.
        /// </summary>
        public RequireClaimActionAttribute()
        {
            _claim = null;
            _action = null;
        }

        /// <summary>
        /// Called when authorization is required. Checks if user has the required claim-action permission.
        /// </summary>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // For now, this is a placeholder implementation that documents the authorization requirement
            // In a full implementation, this would:
            // 1. Get the authenticated user's ID from the JWT token or session
            // 2. Determine the required claim and action (from parameters or auto-detect from controller/method)
            // 3. Call IAccountClaimActionService to verify the user has the required permission
            // 4. Return 401 Unauthorized if not authenticated or 403 Forbidden if lacking permission

            var claim = _claim;
            var action = _action;

            // Auto-detect claim and action if not specified
            if (claim == null || action == null)
            {
                var controllerName = context.RouteData.Values["controller"]?.ToString();
                var httpMethod = context.HttpContext.Request.Method;

                if (controllerName != null && ClaimsAndActions.ControllerToClaimMapping.TryGetValue(controllerName, out var detectedClaim))
                {
                    claim = detectedClaim;
                }

                if (httpMethod != null)
                {
                    // Special handling for GET requests
                    if (httpMethod == "GET")
                    {
                        // Check if this is a list operation (no ID in route) or single item read (has ID)
                        // Primary check: look for 'id' parameter in route values
                        // Secondary check: look for any route parameter that might be an ID (common patterns)
                        var hasId = context.RouteData.Values.ContainsKey("id") ||
                                    context.RouteData.Values.ContainsKey("beneficiaryId") ||
                                    context.RouteData.Values.ContainsKey("companyId") ||
                                    context.RouteData.Values.ContainsKey("cnpj") ||
                                    context.RouteData.Values.ContainsKey("code") ||
                                    context.RouteData.Values.Any(kvp => kvp.Key.EndsWith("Id", StringComparison.OrdinalIgnoreCase));
                        
                        action = hasId ? ClaimsAndActions.Actions.Read : ClaimsAndActions.Actions.List;
                    }
                    else if (ClaimsAndActions.HttpMethodToActionMapping.TryGetValue(httpMethod, out var detectedAction))
                    {
                        action = detectedAction;
                    }
                }
            }

            // TODO: Implement actual authorization check
            // Example implementation (commented out as services need to be available):
            /*
            var accountClaimActionService = context.HttpContext.RequestServices
                .GetRequiredService<IAccountClaimActionService>();
            
            var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userId = int.Parse(userIdClaim.Value);
            var hasPermission = accountClaimActionService.UserHasPermission(userId, claim, action);
            
            if (!hasPermission)
            {
                context.Result = new ForbidResult();
                return;
            }
            */

            // For now, log the required permission for documentation purposes
            // In production, this would enforce the permission check
            // TODO: Replace with ILogger when implementing full enforcement
            // Example: _logger.LogInformation("Authorization required: Claim={Claim}, Action={Action}", claim, action);
        }
    }
}
