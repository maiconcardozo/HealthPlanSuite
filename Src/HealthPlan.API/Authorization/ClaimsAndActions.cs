namespace HealthPlan.API.Authorization
{
    /// <summary>
    /// Defines all claims (resources) and actions (operations) for the HealthPlan API.
    /// Claims represent what resource a user can access, and Actions represent what they can do with it.
    /// </summary>
    public static class ClaimsAndActions
    {
        /// <summary>
        /// Claim names representing different resources in the system
        /// </summary>
        public static class Claims
        {
            public const string AcceptanceRule = "AcceptanceRule";
            public const string Accommodation = "Accommodation";
            public const string AdhesionFee = "AdhesionFee";
            public const string AgeRange = "AgeRange";
            public const string Beneficiary = "Beneficiary";
            public const string Company = "Company";
            public const string Coverage = "Coverage";
            public const string HealthPlan = "HealthPlan";
            public const string PlanCoverage = "PlanCoverage";
            public const string PlanPriceRange = "PlanPriceRange";
            public const string ProcedureCoparticipation = "ProcedureCoparticipation";
            public const string PromotionalDiscount = "PromotionalDiscount";
            public const string Quote = "Quote";
            public const string QuoteHistory = "QuoteHistory";
        }

        /// <summary>
        /// Action names representing operations that can be performed on resources
        /// </summary>
        public static class Actions
        {
            public const string Read = "Read";
            public const string Create = "Create";
            public const string Update = "Update";
            public const string Delete = "Delete";
            public const string List = "List";
        }

        /// <summary>
        /// Gets a dictionary mapping controller names to their claim names
        /// </summary>
        public static Dictionary<string, string> ControllerToClaimMapping = new()
        {
            { "AcceptanceRule", Claims.AcceptanceRule },
            { "Accommodation", Claims.Accommodation },
            { "AdhesionFee", Claims.AdhesionFee },
            { "AgeRange", Claims.AgeRange },
            { "Beneficiary", Claims.Beneficiary },
            { "Company", Claims.Company },
            { "Coverage", Claims.Coverage },
            { "HealthPlan", Claims.HealthPlan },
            { "PlanCoverage", Claims.PlanCoverage },
            { "PlanPriceRange", Claims.PlanPriceRange },
            { "ProcedureCoparticipation", Claims.ProcedureCoparticipation },
            { "PromotionalDiscount", Claims.PromotionalDiscount },
            { "Quote", Claims.Quote },
            { "QuoteHistory", Claims.QuoteHistory }
        };

        /// <summary>
        /// Gets a dictionary mapping HTTP methods to action names.
        /// Note: GET is handled specially to distinguish between List and Read operations.
        /// </summary>
        public static Dictionary<string, string> HttpMethodToActionMapping = new()
        {
            { "POST", Actions.Create },
            { "PUT", Actions.Update },
            { "DELETE", Actions.Delete }
        };
    }
}
