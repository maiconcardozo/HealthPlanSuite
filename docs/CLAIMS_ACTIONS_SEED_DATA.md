# Claims and Actions Seed Data

This document provides the seed data required for the Authentication system. These claims and actions must be populated in the database before the authorization system can function.

## Database Tables Required

The Authentication.Login system requires the following tables:

1. **Claim** - Defines available claims (resources)
2. **Action** - Defines available actions (operations)
3. **ClaimAction** - Defines valid claim-action combinations
4. **Account** - User accounts
5. **AccountClaimAction** - Links accounts to their permissions

## Claims to Seed

Insert the following claims into the `Claim` table:

```sql
-- MySQL/MariaDB: Use CURRENT_TIMESTAMP or CURRENT_TIMESTAMP
-- SQL Server: Use GETDATE() or CURRENT_TIMESTAMP
-- PostgreSQL: Use CURRENT_TIMESTAMP or CURRENT_TIMESTAMP
-- SQLite: Use datetime('now')

-- For MySQL/MariaDB:
INSERT INTO Claim (Name, Description, CreatedAt, UpdatedAt, Active) VALUES
('AcceptanceRule', 'Access to acceptance rules for health plans', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('Accommodation', 'Access to accommodation types configuration', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('AdhesionFee', 'Access to adhesion fee management', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('AgeRange', 'Access to age range pricing configuration', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('Beneficiary', 'Access to beneficiary information management', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('Company', 'Access to insurance company data management', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('Coverage', 'Access to coverage types management', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('HealthPlan', 'Access to health plan definitions', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('PlanCoverage', 'Access to plan-coverage associations', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('PlanPriceRange', 'Access to plan price range configuration', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('ProcedureCoparticipation', 'Access to procedure coparticipation rules', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('PromotionalDiscount', 'Access to promotional discount configuration', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('Quote', 'Access to quote management', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('QuoteHistory', 'Access to quote history tracking', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1);
```

## Actions to Seed

Insert the following actions into the `Action` table:

```sql
INSERT INTO Action (Name, Description, CreatedAt, UpdatedAt, Active) VALUES
('Read', 'Read/view a single resource by ID', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('List', 'List/view multiple resources', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('Create', 'Create a new resource', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('Update', 'Update an existing resource', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1),
('Delete', 'Delete/remove a resource', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1);
```

## ClaimAction Combinations to Seed

Create all valid claim-action combinations. Assuming all claims support all actions:

```sql
-- This creates 70 combinations (14 claims × 5 actions)
INSERT INTO ClaimAction (ClaimId, ActionId, CreatedAt, UpdatedAt, Active)
SELECT c.Id, a.Id, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1
FROM Claim c
CROSS JOIN Action a
WHERE c.Active = 1 AND a.Active = 1;
```

If you want to be more selective about which combinations are valid:

```sql
-- Example: Create specific combinations
INSERT INTO ClaimAction (ClaimId, ActionId, CreatedAt, UpdatedAt, Active)
SELECT c.Id, a.Id, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1
FROM Claim c
JOIN Action a ON 1=1
WHERE c.Name = 'AcceptanceRule' 
  AND a.Name IN ('Read', 'List', 'Create', 'Update', 'Delete')
  AND c.Active = 1 AND a.Active = 1;

-- Repeat for each claim...
```

## Example Admin User Setup

Create an admin user with all permissions:

```sql
-- First, create an admin account (password should be hashed using Argon2)
INSERT INTO Account (UserName, Password, Email, CreatedAt, UpdatedAt, Active) VALUES
('admin', '$argon2id$v=19$m=65536,t=3,p=1$SALT$HASH', 'admin@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1);

-- Get the account ID
SET @adminAccountId = LAST_INSERT_ID();

-- Grant all permissions to admin
INSERT INTO AccountClaimAction (AccountId, ClaimActionId, CreatedAt, UpdatedAt, Active)
SELECT @adminAccountId, ca.Id, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1
FROM ClaimAction ca
WHERE ca.Active = 1;
```

## Example Role-Based Setup

### Manager Role (Read/Update Access)

```sql
-- Create manager account
INSERT INTO Account (UserName, Password, Email, CreatedAt, UpdatedAt, Active) VALUES
('manager', '$argon2id$v=19$m=65536,t=3,p=1$SALT$HASH', 'manager@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1);

SET @managerAccountId = LAST_INSERT_ID();

-- Grant Read and Update permissions to manager
INSERT INTO AccountClaimAction (AccountId, ClaimActionId, CreatedAt, UpdatedAt, Active)
SELECT @managerAccountId, ca.Id, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1
FROM ClaimAction ca
JOIN Action a ON ca.ActionId = a.Id
WHERE a.Name IN ('Read', 'List', 'Update')
  AND ca.Active = 1;
```

### Viewer Role (Read-Only Access)

```sql
-- Create viewer account
INSERT INTO Account (UserName, Password, Email, CreatedAt, UpdatedAt, Active) VALUES
('viewer', '$argon2id$v=19$m=65536,t=3,p=1$SALT$HASH', 'viewer@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1);

SET @viewerAccountId = LAST_INSERT_ID();

-- Grant Read-only permissions to viewer
INSERT INTO AccountClaimAction (AccountId, ClaimActionId, CreatedAt, UpdatedAt, Active)
SELECT @viewerAccountId, ca.Id, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1
FROM ClaimAction ca
JOIN Action a ON ca.ActionId = a.Id
WHERE a.Name IN ('Read', 'List')
  AND ca.Active = 1;
```

### Quote Specialist Role (Quote-Specific Access)

```sql
-- Create quote specialist account
INSERT INTO Account (UserName, Password, Email, CreatedAt, UpdatedAt, Active) VALUES
('quote_specialist', '$argon2id$v=19$m=65536,t=3,p=1$SALT$HASH', 'specialist@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1);

SET @specialistAccountId = LAST_INSERT_ID();

-- Grant Quote-related permissions
INSERT INTO AccountClaimAction (AccountId, ClaimActionId, CreatedAt, UpdatedAt, Active)
SELECT @specialistAccountId, ca.Id, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1
FROM ClaimAction ca
JOIN Claim c ON ca.ClaimId = c.Id
JOIN Action a ON ca.ActionId = a.Id
WHERE (
    (c.Name = 'Quote' AND a.Name IN ('Read', 'List', 'Create', 'Update'))
    OR (c.Name = 'Beneficiary' AND a.Name IN ('Read', 'List'))
    OR (c.Name = 'Company' AND a.Name IN ('Read', 'List'))
    OR (c.Name = 'HealthPlan' AND a.Name IN ('Read', 'List'))
    OR (c.Name = 'QuoteHistory' AND a.Name IN ('Read', 'List'))
)
AND ca.Active = 1;
```

## C# Seed Data (for EF Core Migrations)

If using Entity Framework Core migrations, you can create a seed data class:

```csharp
public static class AuthenticationSeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Seed Claims
        var claims = new[]
        {
            new Claim { Id = 1, Name = "AcceptanceRule", Description = "Access to acceptance rules for health plans", Active = true },
            new Claim { Id = 2, Name = "Accommodation", Description = "Access to accommodation types configuration", Active = true },
            new Claim { Id = 3, Name = "AdhesionFee", Description = "Access to adhesion fee management", Active = true },
            new Claim { Id = 4, Name = "AgeRange", Description = "Access to age range pricing configuration", Active = true },
            new Claim { Id = 5, Name = "Beneficiary", Description = "Access to beneficiary information management", Active = true },
            new Claim { Id = 6, Name = "Company", Description = "Access to insurance company data management", Active = true },
            new Claim { Id = 7, Name = "Coverage", Description = "Access to coverage types management", Active = true },
            new Claim { Id = 8, Name = "HealthPlan", Description = "Access to health plan definitions", Active = true },
            new Claim { Id = 9, Name = "PlanCoverage", Description = "Access to plan-coverage associations", Active = true },
            new Claim { Id = 10, Name = "PlanPriceRange", Description = "Access to plan price range configuration", Active = true },
            new Claim { Id = 11, Name = "ProcedureCoparticipation", Description = "Access to procedure coparticipation rules", Active = true },
            new Claim { Id = 12, Name = "PromotionalDiscount", Description = "Access to promotional discount configuration", Active = true },
            new Claim { Id = 13, Name = "Quote", Description = "Access to quote management", Active = true },
            new Claim { Id = 14, Name = "QuoteHistory", Description = "Access to quote history tracking", Active = true }
        };
        modelBuilder.Entity<Claim>().HasData(claims);

        // Seed Actions
        var actions = new[]
        {
            new Action { Id = 1, Name = "Read", Description = "Read/view a single resource by ID", Active = true },
            new Action { Id = 2, Name = "List", Description = "List/view multiple resources", Active = true },
            new Action { Id = 3, Name = "Create", Description = "Create a new resource", Active = true },
            new Action { Id = 4, Name = "Update", Description = "Update an existing resource", Active = true },
            new Action { Id = 5, Name = "Delete", Description = "Delete/remove a resource", Active = true }
        };
        modelBuilder.Entity<Action>().HasData(actions);

        // Seed ClaimActions (all combinations)
        var claimActions = new List<ClaimAction>();
        int claimActionId = 1;
        for (int claimId = 1; claimId <= 14; claimId++)
        {
            for (int actionId = 1; actionId <= 5; actionId++)
            {
                claimActions.Add(new ClaimAction
                {
                    Id = claimActionId++,
                    ClaimId = claimId,
                    ActionId = actionId,
                    Active = true
                });
            }
        }
        modelBuilder.Entity<ClaimAction>().HasData(claimActions);
    }
}
```

## Verification Queries

After seeding, verify the data:

```sql
-- Count claims
SELECT COUNT(*) as ClaimCount FROM Claim WHERE Active = 1;
-- Expected: 14

-- Count actions
SELECT COUNT(*) as ActionCount FROM Action WHERE Active = 1;
-- Expected: 5

-- Count claim-action combinations
SELECT COUNT(*) as ClaimActionCount FROM ClaimAction WHERE Active = 1;
-- Expected: 70 (14 × 5)

-- View all combinations
SELECT 
    c.Name as Claim,
    a.Name as Action,
    ca.Id as ClaimActionId
FROM ClaimAction ca
JOIN Claim c ON ca.ClaimId = c.Id
JOIN Action a ON ca.ActionId = a.Id
WHERE ca.Active = 1
ORDER BY c.Name, a.Name;

-- Count user permissions
SELECT 
    acc.UserName,
    COUNT(aca.Id) as PermissionCount
FROM Account acc
LEFT JOIN AccountClaimAction aca ON acc.Id = aca.AccountId AND aca.Active = 1
GROUP BY acc.Id, acc.UserName;
```

## Testing Permissions

Test that permissions work correctly:

```sql
-- Check if user has specific permission
SELECT COUNT(*) > 0 as HasPermission
FROM AccountClaimAction aca
JOIN ClaimAction ca ON aca.ClaimActionId = ca.Id
JOIN Claim c ON ca.ClaimId = c.Id
JOIN Action a ON ca.ActionId = a.Id
JOIN Account acc ON aca.AccountId = acc.Id
WHERE acc.UserName = 'admin'
  AND c.Name = 'Company'
  AND a.Name = 'Create'
  AND aca.Active = 1
  AND ca.Active = 1
  AND c.Active = 1
  AND a.Active = 1;
-- Expected: 1 (true) for admin user
```

## Migration Steps

1. Create the database tables (Claim, Action, ClaimAction, Account, AccountClaimAction)
2. Run the SQL seed scripts above to populate Claims and Actions
3. Create ClaimAction combinations
4. Create initial admin user with all permissions
5. Create additional users with role-based permissions as needed
6. Test the authorization system with different users
7. Monitor authorization logs to ensure permissions are being checked

## Notes

- All dates should be set appropriately (CreatedAt, UpdatedAt)
- Passwords must be hashed using Argon2 before inserting
- The `Active` flag allows soft-deletion of permissions
- ClaimAction combinations can be more restrictive if needed
- Consider creating a stored procedure or service for checking permissions
- Consider adding an audit table to log permission checks
