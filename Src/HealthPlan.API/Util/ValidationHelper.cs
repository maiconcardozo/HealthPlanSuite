using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace HealthPlan.API.Util
{
    /// <summary>
    /// Helper class for standardized entity validation across controllers.
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Validates an entity using FluentValidation and returns appropriate error response if validation fails.
        /// </summary>
        /// <typeparam name="T">The type of entity to validate</typeparam>
        /// <param name="entity">The entity to validate</param>
        /// <param name="serviceProvider">Service provider to resolve validators</param>
        /// <param name="controller">Controller instance for error response</param>
        /// <returns>BadRequest result if validation fails, null if validation passes</returns>
        public static async Task<IActionResult?> ValidateEntityAsync<T>(T entity, IServiceProvider serviceProvider, ControllerBase controller)
        {
            var validator = serviceProvider.GetService<IValidator<T>>();
            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(entity, CancellationToken.None);
                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        controller.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return controller.BadRequest(controller.ModelState);
                }
            }
            return null;
        }
    }
}