using Domain.Templates.Entities;
using Domain.Templates.Enums;

namespace Persistence.Configurations;

internal class TemplatePropertyConfiguration : IEntityTypeConfiguration<TemplateProperty>
{
    public void Configure(EntityTypeBuilder<TemplateProperty> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasMaxLength(50)
            .HasConversion<EnumToStringConverter<TemplatePropertyType>>()
            .IsRequired();
    }
}
