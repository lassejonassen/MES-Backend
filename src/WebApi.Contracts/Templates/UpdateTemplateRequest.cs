namespace WebApi.Contracts.Templates;

public sealed record UpdateTemplateRequest(
    Guid Id,
    string Name,
    string? Description);
