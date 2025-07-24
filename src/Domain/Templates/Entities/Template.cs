using Abstractions;

namespace Domain.Templates.Entities;

public class Template : BaseEntity
{
    private Template()
    {
    }

    private Template(DateTime utcNow) : base(utcNow)
    {
        Id = Guid.NewGuid();
    }

    private Template(Guid id, DateTime utcNow) : base(utcNow)
    {
        Id = id;
    }

    public Guid Id { get; init; }
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public Guid? BaseTemplateId { get; init; }

    public virtual Template? BaseTemplate { get; init; }
    public virtual List<Template> DerivedTemplates { get; init; } = [];
    public virtual List<TemplateProperty> TemplateProperties { get; init; } = [];

    public static Result<Template> Create(Guid id, string name, string description, DateTime utcNow)
    {
        // TOOD: Add Validation

        var template = new Template(id, utcNow)
        {
            Name = name,
            Description = description
        };

        // Raise domain event.

        return template;
    }

    public static Result<Template> Create(string name, string description, DateTime utcNow)
    {
        // TOOD: Add Validation

        var template = new Template(utcNow)
        {
            Name = name,
            Description = description
        };

        // Raise domain event.

        return template;
    }

    public static Result<Template> CreateDerived(Guid baseTemplateId, string name, string description, DateTime utcNow)
    {
        // TOOD: Add Validation

        var template = new Template(utcNow)
        {
            BaseTemplateId = baseTemplateId,
            Name = name,
            Description = description
        };

        // Raise domain event.

        return template;
    }

    public Result Update(string name, string description)
    {
        // TOOD: Add Validation

        Name = name;
        Description = description;

        // Raise domain event.
        return Result.Success();
    }

    public void Delete()
    {
        // Raise domain event.
    }
}
