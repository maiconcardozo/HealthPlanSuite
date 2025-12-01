# Controller Migration Steps

## Overview

This document provides guidance for migrating controllers to follow best practices and patterns established in the HealthPlanSuite project.

## Migration Pattern

When migrating or creating new controllers, follow these steps:

### Step 1: Define the Controller Structure

Controllers should follow the standard ASP.NET Core patterns with proper dependency injection:

```csharp
using Microsoft.AspNetCore.Mvc;

namespace HealthPlanSuite.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleController : ControllerBase
    {
        private readonly IService _service;

        public ExampleController(IService service)
        {
            _service = service;
        }
    }
}
```

### Step 2: Implement CRUD Operations

Each controller should implement standard CRUD operations following RESTful conventions:

- `GET` - Retrieve resources
- `POST` - Create new resources
- `PUT` - Update existing resources
- `DELETE` - Remove resources

### Step 3: Add Proper Error Handling

Ensure all controller methods have proper error handling and return appropriate HTTP status codes.

### Step 4: Add Swagger Documentation

Document all endpoints with Swagger annotations for API documentation.

## Verification Checklist

- [ ] Controller follows RESTful conventions
- [ ] Dependency injection is properly configured
- [ ] Error handling is implemented
- [ ] Swagger documentation is complete
- [ ] Unit tests are created for all endpoints

## Reference

Use existing controllers in the `src/HealthPlanSuite.API/Controllers/` directory as reference implementations.
