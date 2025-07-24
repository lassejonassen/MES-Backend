using Microsoft.Extensions.Logging;

namespace Persistence;

public static class DatabaseMigrator
{
    public static void ApplyMigrations(DbContext context, ILogger logger)
    {
        logger.LogInformation("Checking if there are new migrations");

        if (context.Database.GetPendingMigrations().Any())
        {
            try
            {
                logger.LogInformation("Starting pending migrations");

                context.Database.Migrate();

                logger.LogInformation("Migrations done");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error while applying migrations");
                throw;
            }
        }
        else
        {
            logger.LogInformation("No pending migrations");
        }
    }
}
