using HealthPlan.API.Resource;

namespace HealthPlan.API.Swagger
{
    /// <summary>
    /// Route constants for Accommodation API endpoints.
    /// Uses resource files for localization and consistency.
    /// </summary>
    public static class AccommodationRoutes
    {
        /// <summary>
        /// Route for getting all accommodations.
        /// </summary>
        public const string GetAccommodations = "accommodations";
        
        /// <summary>
        /// Route for getting an accommodation by ID.
        /// </summary>
        public const string GetAccommodationById = "{id}";
        
        /// <summary>
        /// Route for getting accommodations by type.
        /// </summary>
        public const string GetAccommodationsByType = "type/{type}";
        
        /// <summary>
        /// Route for adding a new accommodation.
        /// </summary>
        public const string AddAccommodation = "";
        
        /// <summary>
        /// Route for updating an existing accommodation.
        /// </summary>
        public const string UpdateAccommodation = "";
        
        /// <summary>
        /// Route for deleting an accommodation.
        /// </summary>
        public const string DeleteAccommodation = "{id}";
    }
}