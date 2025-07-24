using Abstractions;

namespace Domain.Templates.Entities;

public class TemplatePropertyOption : BaseEntity
{
    private TemplatePropertyOption()
    {
    }

    private TemplatePropertyOption(DateTime utcNow) : base(utcNow)
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; init; }
    public Guid TemplatePropertyId { get; set; }
    public string Name { get; set; } = null!;
    public string? Value { get; set; }
    public int DisplayOrder { get; set; } = 0;

    public virtual TemplateProperty TemplateProperty { get; set; } = null!;

    public static TemplatePropertyOption Create(Guid templatePropertyId, string name, string? value, int displayOrder, DateTime utcNow)
    {
        // TODO: Add Validation.

        return new TemplatePropertyOption(utcNow)
        {
            TemplatePropertyId = templatePropertyId,
            Name = name,
            Value = value,
            DisplayOrder = displayOrder
        };
    }
}