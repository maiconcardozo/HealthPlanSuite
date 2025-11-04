# Authentication and Permissions Documentation

## Overview

This document outlines the authentication and authorization implementation for the HealthPlan Suite API. The system uses a claim-action based permission model where:

- **Claims** represent resources (e.g., "Company", "Quote", "HealthPlan")
- **Actions** represent operations (e.g., "Read", "Create", "Update", "Delete", "List")
- **AccountClaimActions** link user accounts to specific claim-action combinations

## Authorization Model

### Claims (Resources)

Each claim represents a distinct resource or entity type in the system:

| Claim Name | Description |
|------------|-------------|
| AcceptanceRule | Acceptance rules for health plans |
| Accommodation | Accommodation types for health plans |
| AdhesionFee | Adhesion fees configuration |
| AgeRange | Age ranges for pricing |
| Beneficiary | Beneficiary information |
| Company | Insurance company data |
| Coverage | Coverage types |
| HealthPlan | Health plan definitions |
| PlanCoverage | Plan-coverage associations |
| PlanPriceRange | Plan price ranges |
| ProcedureCoparticipation | Procedure coparticipation rules |
| PromotionalDiscount | Promotional discount configurations |
| Quote | Quote management |
| QuoteHistory | Quote history tracking |

### Actions (Operations)

Standard operations that can be performed on resources:

| Action Name | Description | HTTP Method |
|-------------|-------------|-------------|
| Read | View a single resource by ID | GET /{id} |
| List | View multiple resources | GET / |
| Create | Create a new resource | POST / |
| Update | Modify an existing resource | PUT / or PUT /{id} |
| Delete | Remove a resource | DELETE /{id} |

## Controller Permissions Matrix

### AcceptanceRuleController

| Endpoint | HTTP Method | Required Claim | Required Action |
|----------|-------------|----------------|-----------------|
| GET / | GET | AcceptanceRule | List |
| GET /{id} | GET | AcceptanceRule | Read |
| POST / | POST | AcceptanceRule | Create |
| PUT / | PUT | AcceptanceRule | Update |
| DELETE /{id} | DELETE | AcceptanceRule | Delete |

### AccommodationController

| Endpoint | HTTP Method | Required Claim | Required Action |
|----------|-------------|----------------|-----------------|
| GET / | GET | Accommodation | List |
| GET /{id} | GET | Accommodation | Read |
| POST / | POST | Accommodation | Create |
| PUT / | PUT | Accommodation | Update |
| DELETE /{id} | DELETE | Accommodation | Delete |

### AdhesionFeeController

| Endpoint | HTTP Method | Required Claim | Required Action |
|----------|-------------|----------------|-----------------|
| GET / | GET | AdhesionFee | List |
| GET /{id} | GET | AdhesionFee | Read |
| POST / | POST | AdhesionFee | Create |
| PUT / | PUT | AdhesionFee | Update |
| DELETE /{id} | DELETE | AdhesionFee | Delete |

### AgeRangeController

| Endpoint | HTTP Method | Required Claim | Required Action |
|----------|-------------|----------------|-----------------|
| GET / | GET | AgeRange | List |
| GET /{id} | GET | AgeRange | Read |
| POST / | POST | AgeRange | Create |
| PUT / | PUT | AgeRange | Update |
| DELETE /{id} | DELETE | AgeRange | Delete |

### BeneficiaryController

| Endpoint | HTTP Method | Required Claim | Required Action |
|----------|-------------|----------------|-----------------|
| GET / | GET | Beneficiary | List |
| GET /{id} | GET | Beneficiary | Read |
| POST / | POST | Beneficiary | Create |
| PUT / | PUT | Beneficiary | Update |
| DELETE /{id} | DELETE | Beneficiary | Delete |

### CompanyController

| Endpoint | HTTP Method | Required Claim | Required Action |
|----------|-------------|----------------|-----------------|
| GET / | GET | Company | List |
| GET /{id} | GET | Company | Read |
| GET /cnpj/{cnpj} | GET | Company | Read |
| POST / | POST | Company | Create |
| PUT / | PUT | Company | Update |
| DELETE /{id} | DELETE | Company | Delete |

### CoverageController

| Endpoint | HTTP Method | Required Claim | Required Action |
|----------|-------------|----------------|-----------------|
| GET / | GET | Coverage | List |
| GET /{id} | GET | Coverage | Read |
| POST / | POST | Coverage | Create |
| PUT / | PUT | Coverage | Update |
| DELETE /{id} | DELETE | Coverage | Delete |

### HealthPlanController

| Endpoint | HTTP Method | Required Claim | Required Action |
|----------|-------------|----------------|-----------------|
| GET / | GET | HealthPlan | List |
| GET /{id} | GET | HealthPlan | Read |
| GET /company/{companyId} | GET | HealthPlan | Read |
| GET /code/{code} | GET | HealthPlan | Read |
| POST / | POST | HealthPlan | Create |
| PUT / | PUT | HealthPlan | Update |
| DELETE /{id} | DELETE | HealthPlan | Delete |

### PlanCoverageController

| Endpoint | HTTP Method | Required Claim | Required Action |
|----------|-------------|----------------|-----------------|
| GET / | GET | PlanCoverage | List |
| GET /{id} | GET | PlanCoverage | Read |
| POST / | POST | PlanCoverage | Create |
| PUT / | PUT | PlanCoverage | Update |
| DELETE /{id} | DELETE | PlanCoverage | Delete |

### PlanPriceRangeController

| Endpoint | HTTP Method | Required Claim | Required Action |
|----------|-------------|----------------|-----------------|
| GET / | GET | PlanPriceRange | List |
| GET /{id} | GET | PlanPriceRange | Read |
| POST / | POST | PlanPriceRange | Create |
| PUT / | PUT | PlanPriceRange | Update |
| DELETE /{id} | DELETE | PlanPriceRange | Delete |

### ProcedureCoparticipationController

| Endpoint | HTTP Method | Required Claim | Required Action |
|----------|-------------|----------------|-----------------|
| GET / | GET | ProcedureCoparticipation | List |
| GET /{id} | GET | ProcedureCoparticipation | Read |
| POST / | POST | ProcedureCoparticipation | Create |
| PUT / | PUT | ProcedureCoparticipation | Update |
| DELETE /{id} | DELETE | ProcedureCoparticipation | Delete |

### PromotionalDiscountController

| Endpoint | HTTP Method | Required Claim | Required Action |
|----------|-------------|----------------|-----------------|
| GET / | GET | PromotionalDiscount | List |
| GET /{id} | GET | PromotionalDiscount | Read |
| POST / | POST | PromotionalDiscount | Create |
| PUT / | PUT | PromotionalDiscount | Update |
| DELETE /{id} | DELETE | PromotionalDiscount | Delete |

### QuoteController

| Endpoint | HTTP Method | Required Claim | Required Action |
|----------|-------------|----------------|-----------------|
| GET / | GET | Quote | List |
| GET /{id} | GET | Quote | Read |
| GET /beneficiary/{beneficiaryId} | GET | Quote | Read |
| GET /{id}/complete | GET | Quote | Read |
| POST / | POST | Quote | Create |
| PUT / | PUT | Quote | Update |
| DELETE /{id} | DELETE | Quote | Delete |

### QuoteHistoryController

| Endpoint | HTTP Method | Required Claim | Required Action |
|----------|-------------|----------------|-----------------|
| GET / | GET | QuoteHistory | List |
| GET /{id} | GET | QuoteHistory | Read |
| POST / | POST | QuoteHistory | Create |
| PUT / | PUT | QuoteHistory | Update |
| DELETE /{id} | DELETE | QuoteHistory | Delete |

## Implementation Details

### Authorization Attribute

The `RequireClaimActionAttribute` is applied to controllers and actions to enforce permissions. It can be used in two ways:

1. **Auto-detection** (recommended for consistency):
   ```csharp
   [RequireClaimAction]
   public class CompanyController : ControllerBase
   {
       // Automatically uses "Company" claim and detects action from HTTP method
   }
   ```

2. **Explicit specification** (for custom scenarios):
   ```csharp
   [RequireClaimAction("Company", "Read")]
   public IActionResult GetCompanyByCNPJ(string cnpj)
   {
       // Explicitly requires Company:Read permission
   }
   ```

### Setup Required

To fully enable authentication and authorization:

1. **Database Setup**: Ensure the Authentication.Login database tables exist:
   - `Account` - User accounts
   - `Claim` - Available claims (resources)
   - `Action` - Available actions (operations)
   - `ClaimAction` - Valid claim-action combinations
   - `AccountClaimAction` - User permissions (which users have which claim-actions)

2. **Seed Data**: Populate the database with:
   - All claims listed in this document
   - All actions listed in this document
   - ClaimAction combinations for each permission needed
   - AccountClaimAction entries to grant permissions to users

3. **Authentication Setup**: Configure JWT or session-based authentication to identify users

4. **Authorization Service**: Implement `IAccountClaimActionService.UserHasPermission()` method to check if a user has a specific claim-action combination

## Testing Permissions

### Required Test Scenarios

1. **Unauthenticated Access**: Verify that unauthenticated requests return 401
2. **Unauthorized Access**: Verify that authenticated users without the required permission return 403
3. **Authorized Access**: Verify that users with the correct permission can access endpoints
4. **Cross-Resource Access**: Verify users with Company:Read cannot access Quote:Read endpoints

### Example Permission Grants

For a typical user role:

**Admin Role** - Full access:
- All Claims × All Actions = Full system access

**Manager Role** - Read/Update access:
- All Claims × {Read, List, Update} = Can view and modify but not create/delete

**Viewer Role** - Read-only access:
- All Claims × {Read, List} = Can only view data

**Quote Specialist Role** - Quote-specific access:
- Quote × {Read, List, Create, Update}
- Beneficiary × {Read, List}
- Company × {Read, List}
- HealthPlan × {Read, List}

## Integration with maiconcardozo/Authentication

This implementation integrates with the Authentication.Login module that provides:

- `IAccountService` - Account management
- `IClaimService` - Claim management
- `IActionService` - Action management
- `IClaimActionService` - ClaimAction management
- `IAccountClaimActionService` - Permission checking

These services are already registered in `Startup.cs` via `AddAuthenticationLoginServices()`.

## Security Considerations

1. **Principle of Least Privilege**: Users should only have the minimum permissions needed
2. **Separation of Concerns**: Claims separate resources, actions separate operations
3. **Audit Trail**: All permission checks should be logged for security auditing
4. **Token Security**: JWT tokens should have appropriate expiration and refresh mechanisms
5. **SQL Injection**: Use parameterized queries in all database operations
6. **XSS Protection**: Validate and sanitize all user inputs

## Future Enhancements

1. **Role-Based Groups**: Implement roles that group multiple claim-actions
2. **Resource-Level Permissions**: Fine-grained permissions (e.g., "can only edit own quotes")
3. **Conditional Permissions**: Time-based or condition-based access
4. **Permission Delegation**: Allow users to delegate permissions to others
5. **API Rate Limiting**: Prevent abuse by limiting requests per user/endpoint
