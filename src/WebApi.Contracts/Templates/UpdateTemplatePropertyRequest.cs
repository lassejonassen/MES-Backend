namespace WebApi.Contracts.Templates;

public sealed record UpdateTemplatePropertyRequest
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Type { get; init; }
    public string? Description { get; init; }
    public required bool IsRequired { get; init; }
    public string? DefaultValue { get; init; }
    public bool IsReadOnly { get; init; }
}