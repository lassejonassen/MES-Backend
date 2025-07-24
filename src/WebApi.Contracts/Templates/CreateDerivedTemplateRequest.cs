namespace WebApi.Contracts.Templates;

public sealed record CreateDerivedTemplateRequest(
    Guid BaseTemplateId,
    string Name,
    string? Description);