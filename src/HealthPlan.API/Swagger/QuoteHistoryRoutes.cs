using HealthPlan.API.Resource;

namespace HealthPlan.API.Swagger
{
    /// <summary>
    /// Route constants for QuoteHistory API endpoints.
    /// Uses resource files for localization and consistency.
    /// </summary>
    public static class QuoteHistoryRoutes
    {
        /// <summary>
        /// Route for getting all quote histories.
        /// </summary>
        public const string GetQuoteHistories = "quote-histories";
        
        /// <summary>
        /// Route for getting a quote history by ID.
        /// </summary>
        public const string GetQuoteHistoryById = "{id}";
        
        /// <summary>
        /// Route for getting quote histories by quote ID.
        /// </summary>
        public const string GetQuoteHistoriesByQuoteId = "quote/{quoteId}";
        
        /// <summary>
        /// Route for adding a new quote history.
        /// </summary>
        public const string AddQuoteHistory = "";
        
        /// <summary>
        /// Route for updating an existing quote history.
        /// </summary>
        public const string UpdateQuoteHistory = "";
        
        /// <summary>
        /// Route for deleting a quote history.
        /// </summary>
        public const string DeleteQuoteHistory = "{id}";
    }
}