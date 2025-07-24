namespace WebApi.Contracts.Templates;

public record TemplatePropertyDto
{
    public required Guid Id { get; init; }
    public required Guid TemplateId { get; init; }
    public required string Name { get; init; }
    public required string Type { get; init; }
    public string? Description { get; init; }
    public string? DefaultValue { get; init; }
    public required bool IsRequired { get; init; }
    public DateTime CreatedAtUtc { get; init; }
    public DateTime UpdatedAtUtc { get; init; }
    public bool IsReadOnly { get; init; }
}
