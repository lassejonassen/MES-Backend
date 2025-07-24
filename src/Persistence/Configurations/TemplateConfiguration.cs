using Domain.Templates.Entities;

namespace Persistence.Configurations;

internal class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasOne(x => x.BaseTemplate)
            .WithMany(x => x.DerivedTemplates)
            .HasForeignKey(x => x.BaseTemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.TemplateProperties)
            .WithOne(x => x.Template)
            .HasForeignKey(x => x.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}