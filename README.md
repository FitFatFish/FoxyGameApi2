# FoxyGameApi2

## Database Migrations

This project uses Entity Framework Core for database migrations.  
Follow these steps to add a new migration and update the database:

### 1. Add a Migration

Use the following command to add a new migration. Replace `MigrationName` with a descriptive name for your migration.

```bash
dotnet ef migrations add InitialCreate --project Foxy.DataLayer --startup-project Foxy.WebApi --context FoxyDbContext
```

### 2. Update the Database

Apply the latest migrations to the database with:
```bash
dotnet ef database update --project Foxy.DataLayer --startup-project Foxy.WebApi --context FoxyDbContext
```

### Notes
- Ensure you have the `dotnet-ef` tool installed. If not, install it with:
- Run these commands from the solution root directory.
- The `--project` parameter specifies the class library containing your `DbContext`.
- The `--startup-project` parameter specifies the startup project (usually the Web API).
- The `--context` parameter specifies the name of your `DbContext` class.

### How to clear all data an tables in postgresql

To clear all data and tables in a PostgreSQL database, you can use the following SQL commands. Be cautious, as this will delete all data in the specified database.
```sql
DO $$
DECLARE
    r RECORD;
BEGIN
    -- Disable all triggers to avoid constraint violations during deletion
    SET session_replication_role = replica;
    
    -- Drop all tables
    FOR r IN (
        SELECT tablename 
        FROM pg_tables 
        WHERE schemaname = 'public'
    ) LOOP
        EXECUTE 'DROP TABLE IF EXISTS ' || quote_ident(r.tablename) || ' CASCADE';
    END LOOP;
    
    -- Re-enable triggers
    SET session_replication_role = origin;
END $$;
```
