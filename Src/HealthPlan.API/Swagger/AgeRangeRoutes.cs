using HealthPlan.API.Resource;

namespace HealthPlan.API.Swagger
{
    /// <summary>
    /// Route constants for AgeRange API endpoints.
    /// Uses resource files for localization and consistency.
    /// </summary>
    public static class AgeRangeRoutes
    {
        /// <summary>
        /// Route for getting all age ranges.
        /// </summary>
        public const string GetAgeRanges = "ageranges";
        
        /// <summary>
        /// Route for getting an age range by ID.
        /// </summary>
        public const string GetAgeRangeById = "{id}";
        
        /// <summary>
        /// Route for getting age range by age.
        /// </summary>
        public const string GetAgeRangeByAge = "age/{age}";
        
        /// <summary>
        /// Route for adding a new age range.
        /// </summary>
        public const string AddAgeRange = "";
        
        /// <summary>
        /// Route for updating an existing age range.
        /// </summary>
        public const string UpdateAgeRange = "{id}";
        
        /// <summary>
        /// Route for deleting an age range.
        /// </summary>
        public const string DeleteAgeRange = "{id}";
    }
}