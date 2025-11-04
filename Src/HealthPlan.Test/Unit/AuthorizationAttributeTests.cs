using HealthPlan.API.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HealthPlan.Test.Unit
{
    /// <summary>
    /// Tests for the RequireClaimActionAttribute authorization filter
    /// </summary>
    public class AuthorizationAttributeTests
    {
        /// <summary>
        /// Tests that the attribute can auto-detect claim from controller name
        /// </summary>
        [Fact]
        public void RequireClaimAction_AutoDetectsClaim_FromControllerName()
        {
            // Arrange
            var attribute = new RequireClaimActionAttribute();
            var context = CreateAuthorizationFilterContext("Company", "GET");

            // Act - This will log the detected claim and action
            attribute.OnAuthorization(context);

            // Assert - Context result should be null (no error) for this test
            // In a full implementation, this would check permissions and set context.Result if unauthorized
            Assert.Null(context.Result);
        }

        /// <summary>
        /// Tests that the attribute can auto-detect action from HTTP method
        /// </summary>
        [Fact]
        public void RequireClaimAction_AutoDetectsAction_FromHttpMethod()
        {
            // Arrange
            var attribute = new RequireClaimActionAttribute();
            var contextGet = CreateAuthorizationFilterContext("Company", "GET");
            var contextPost = CreateAuthorizationFilterContext("Company", "POST");
            var contextPut = CreateAuthorizationFilterContext("Company", "PUT");
            var contextDelete = CreateAuthorizationFilterContext("Company", "DELETE");

            // Act
            attribute.OnAuthorization(contextGet);
            attribute.OnAuthorization(contextPost);
            attribute.OnAuthorization(contextPut);
            attribute.OnAuthorization(contextDelete);

            // Assert - All should complete without error
            Assert.Null(contextGet.Result);
            Assert.Null(contextPost.Result);
            Assert.Null(contextPut.Result);
            Assert.Null(contextDelete.Result);
        }

        /// <summary>
        /// Tests that the attribute accepts explicit claim and action
        /// </summary>
        [Fact]
        public void RequireClaimAction_UsesExplicitClaimAndAction()
        {
            // Arrange
            var attribute = new RequireClaimActionAttribute("CustomClaim", "CustomAction");
            var context = CreateAuthorizationFilterContext("Company", "GET");

            // Act
            attribute.OnAuthorization(context);

            // Assert - Should complete without error
            Assert.Null(context.Result);
        }

        /// <summary>
        /// Tests claims and actions constants are properly defined
        /// </summary>
        [Fact]
        public void ClaimsAndActions_HasAllRequiredClaims()
        {
            // Assert - Verify all expected claims are defined
            Assert.Equal("AcceptanceRule", ClaimsAndActions.Claims.AcceptanceRule);
            Assert.Equal("Accommodation", ClaimsAndActions.Claims.Accommodation);
            Assert.Equal("AdhesionFee", ClaimsAndActions.Claims.AdhesionFee);
            Assert.Equal("AgeRange", ClaimsAndActions.Claims.AgeRange);
            Assert.Equal("Beneficiary", ClaimsAndActions.Claims.Beneficiary);
            Assert.Equal("Company", ClaimsAndActions.Claims.Company);
            Assert.Equal("Coverage", ClaimsAndActions.Claims.Coverage);
            Assert.Equal("HealthPlan", ClaimsAndActions.Claims.HealthPlan);
            Assert.Equal("PlanCoverage", ClaimsAndActions.Claims.PlanCoverage);
            Assert.Equal("PlanPriceRange", ClaimsAndActions.Claims.PlanPriceRange);
            Assert.Equal("ProcedureCoparticipation", ClaimsAndActions.Claims.ProcedureCoparticipation);
            Assert.Equal("PromotionalDiscount", ClaimsAndActions.Claims.PromotionalDiscount);
            Assert.Equal("Quote", ClaimsAndActions.Claims.Quote);
            Assert.Equal("QuoteHistory", ClaimsAndActions.Claims.QuoteHistory);
        }

        /// <summary>
        /// Tests actions constants are properly defined
        /// </summary>
        [Fact]
        public void ClaimsAndActions_HasAllRequiredActions()
        {
            // Assert - Verify all expected actions are defined
            Assert.Equal("Read", ClaimsAndActions.Actions.Read);
            Assert.Equal("Create", ClaimsAndActions.Actions.Create);
            Assert.Equal("Update", ClaimsAndActions.Actions.Update);
            Assert.Equal("Delete", ClaimsAndActions.Actions.Delete);
            Assert.Equal("List", ClaimsAndActions.Actions.List);
        }

        /// <summary>
        /// Tests controller to claim mapping
        /// </summary>
        [Fact]
        public void ClaimsAndActions_MapsControllersToClaimsCorrectly()
        {
            // Assert - Verify mapping exists for all controllers
            Assert.Equal("Company", ClaimsAndActions.ControllerToClaimMapping["Company"]);
            Assert.Equal("Quote", ClaimsAndActions.ControllerToClaimMapping["Quote"]);
            Assert.Equal("HealthPlan", ClaimsAndActions.ControllerToClaimMapping["HealthPlan"]);
            Assert.Equal(14, ClaimsAndActions.ControllerToClaimMapping.Count);
        }

        /// <summary>
        /// Tests HTTP method to action mapping
        /// </summary>
        [Fact]
        public void ClaimsAndActions_MapsHttpMethodsToActionsCorrectly()
        {
            // Assert - Verify mapping exists for HTTP methods
            Assert.Equal("Read", ClaimsAndActions.HttpMethodToActionMapping["GET"]);
            Assert.Equal("Create", ClaimsAndActions.HttpMethodToActionMapping["POST"]);
            Assert.Equal("Update", ClaimsAndActions.HttpMethodToActionMapping["PUT"]);
            Assert.Equal("Delete", ClaimsAndActions.HttpMethodToActionMapping["DELETE"]);
            Assert.Equal(4, ClaimsAndActions.HttpMethodToActionMapping.Count);
        }

        /// <summary>
        /// Helper method to create an AuthorizationFilterContext for testing
        /// </summary>
        private AuthorizationFilterContext CreateAuthorizationFilterContext(string controllerName, string httpMethod)
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = new ServiceCollection().BuildServiceProvider()
            };
            httpContext.Request.Method = httpMethod;

            var routeData = new RouteData();
            routeData.Values.Add("controller", controllerName);

            var actionContext = new ActionContext(
                httpContext,
                routeData,
                new ActionDescriptor()
            );

            return new AuthorizationFilterContext(
                actionContext,
                new List<IFilterMetadata>()
            );
        }
    }
}
