using Domain.Templates.Entities;

namespace Persistence.DbContexts;

public  class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : BaseDbContext<ApplicationDbContext>(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Template> Templates { get; set; }
    public DbSet<TemplateProperty> TemplateProperties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
