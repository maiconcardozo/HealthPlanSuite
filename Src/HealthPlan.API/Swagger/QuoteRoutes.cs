using HealthPlan.API.Resource;

namespace HealthPlan.API.Swagger
{
    /// <summary>
    /// Route constants for Quote API endpoints.
    /// Uses resource files for localization and consistency.
    /// </summary>
    public static class QuoteRoutes
    {
        /// <summary>
        /// Route for getting all quotes.
        /// </summary>
        public const string GetQuotes = "quotes";
        
        /// <summary>
        /// Route for getting a quote by ID.
        /// </summary>
        public const string GetQuoteById = "{id}";
        
        /// <summary>
        /// Route for getting quotes by beneficiary.
        /// </summary>
        public const string GetQuotesByBeneficiary = "beneficiary/{beneficiaryId}";
        
        /// <summary>
        /// Route for adding a new quote.
        /// </summary>
        public const string AddQuote = "";
        
        /// <summary>
        /// Route for updating an existing quote.
        /// </summary>
        public const string UpdateQuote = "";
        
        /// <summary>
        /// Route for deleting a quote.
        /// </summary>
        public const string DeleteQuote = "{id}";
    }
}