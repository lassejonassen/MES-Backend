using Abstractions;
using Domain.Templates.Enums;

namespace Domain.Templates.Entities;

public class TemplateProperty : BaseEntity
{
    private TemplateProperty()
    {
    }

    private TemplateProperty(DateTime utcNow) : base(utcNow)
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; init; }

    public Guid TemplateId { get; init; }

    public string Name { get; private set; } = null!;
    public TemplatePropertyType Type { get; private set; }
    public string? Description { get; private set; }
    public bool IsRequired { get; private set; }
    public string? DefaultValue { get; private set; }
    public bool IsReadOnly { get; private set; } = false;

    public virtual Template Template { get; init; } = null!;
    public virtual List<TemplatePropertyOption> Options { get; set; } = [];

    public static TemplateProperty Create(
        Guid templateId,
        string name,
        string type,
        string? description,
        bool isRequired,
        bool isReadOnly,
        DateTime utcNow,
        string? defaultValue = null)
    {
        // TOOD: Add Validation

        return new TemplateProperty(utcNow)
        {
            TemplateId = templateId,
            Name = name,
            Type = Enum.Parse<TemplatePropertyType>(type, ignoreCase: true),
            Description = description,
            DefaultValue = defaultValue,
            IsRequired = isRequired,
            IsReadOnly = isReadOnly
        };
    }

    public void Update(
        string name,
        string type,
        string? description,
        bool isRequired,
        bool isReadOnly,
        string? defaultValue)
    {
        // TOOD: Add Validation

        Name = name;
        Type = Enum.Parse<TemplatePropertyType>(type, ignoreCase: true);
        Description = description;
        IsRequired = isRequired;
        IsReadOnly = isReadOnly;
        DefaultValue = defaultValue;
    }

    public bool IsTableType()
    {
        return Type == TemplatePropertyType.Table;
    }
}
