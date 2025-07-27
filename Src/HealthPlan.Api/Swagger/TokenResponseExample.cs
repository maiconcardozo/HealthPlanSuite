using Authentication.Login.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Authentication.API.Swagger
{
    public class SuccessDetailsExample : IExamplesProvider<SuccessResponseDTO>
    {
        public SuccessResponseDTO GetExamples()
        {
            return new SuccessResponseDTO
            {
                Status = 200,
                Message = "Request was successful.",
                Data = default(object)
            };
        }
    }

    public static class ProblemDetailsExampleFactory
    {
        public static ProblemDetails ForBadRequest(string detail) => new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Invalid request",
            Detail = detail,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Instance = "/authentication/generatetoken"
        };

        public static ProblemDetails ForUnauthorized(string detail) => new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Detail = detail,
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
            Instance = "/authentication/generatetoken"
        };

        public static ProblemDetails ForNotFound(string detail) => new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Not Found",
            Detail = detail,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Instance = "/resource/notfound"
        };

        public static ProblemDetails ForInternalServerError(string detail) => new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Detail = detail,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Instance = "/authentication/generatetoken"
        };
    }

    public class ProblemDetailsBadRequestExample : IExamplesProvider<ProblemDetails>
    {
        private readonly string _detail;

        public ProblemDetailsBadRequestExample(string detail = "One or more validation errors occurred.")
        {
            _detail = detail;
        }

        public ProblemDetails GetExamples() => ProblemDetailsExampleFactory.ForBadRequest(_detail);
    }

    public class ProblemDetailsUnauthorizedExample : IExamplesProvider<ProblemDetails>
    {
        private readonly string _detail;

        public ProblemDetailsUnauthorizedExample(string detail = "Authentication failed. Invalid credentials.")
        {
            _detail = detail;
        }

        public ProblemDetails GetExamples() => ProblemDetailsExampleFactory.ForUnauthorized(_detail);
    }

    public class ProblemDetailsInternalServerErrorExample : IExamplesProvider<ProblemDetails>
    {
        private readonly string _detail;

        public ProblemDetailsInternalServerErrorExample(string detail = "An unexpected error occurred.")
        {
            _detail = detail;
        }

        public ProblemDetails GetExamples() => ProblemDetailsExampleFactory.ForInternalServerError(_detail);
    }

    public class ProblemDetailsNotFoundExample : IExamplesProvider<ProblemDetails>
    {
        private readonly string _detail;

        public ProblemDetailsNotFoundExample(string detail = "The requested resource was not found.")
        {
            _detail = detail;
        }

        public ProblemDetails GetExamples() => ProblemDetailsExampleFactory.ForNotFound(_detail);
    }
}