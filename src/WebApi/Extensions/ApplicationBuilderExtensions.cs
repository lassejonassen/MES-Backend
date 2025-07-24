using Microsoft.EntityFrameworkCore;
using Persistence;

namespace WebApi.Extensions;

public static partial class ApplicationBuilderExtensions
{
    public static IApplicationBuilder ApplyMigrations<TDbContext>(this IApplicationBuilder app) where TDbContext : DbContext
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<TDbContext>>();

        DatabaseMigrator.ApplyMigrations(context, logger);

        return app;
    }
}