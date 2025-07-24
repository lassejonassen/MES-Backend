using Abstractions;

namespace Persistence.DbContexts;

public class BaseDbContext<TDbContext>(DbContextOptions<TDbContext> options) : DbContext(options), IUnitOfWork where TDbContext : DbContext
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<DateTime>().HaveConversion<DateTimeAsUtcValueConverter>();
        configurationBuilder.Properties<DateTime?>().HaveConversion<NullableDateTimeAsUtcValueConverter>();
    }

    public class NullableDateTimeAsUtcValueConverter() : ValueConverter<DateTime?, DateTime?>(
        v => !v.HasValue ? v : ToUtc(v.Value),
        v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v)
    {
        private static DateTime? ToUtc(DateTime v) => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime();
    }

    public class DateTimeAsUtcValueConverter() : ValueConverter<DateTime, DateTime>(
        v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
        v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

    public async Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken)
    {
        var strategy = Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            var currentTransaction = Database.CurrentTransaction;

            if (currentTransaction is not null)
            {
                await currentTransaction.DisposeAsync();
            }

            await using var transaction = await Database.BeginTransactionAsync(cancellationToken);

            await action();

            await SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public void RevertChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.State = EntityState.Unchanged; // Revert modified entities
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;  // Remove added entities
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged; // Revert deleted entities
                    break;
            }
        }
    }
}