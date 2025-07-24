namespace WebApi.Contracts.Templates;

public sealed record CreateTemplateRequest(
    string Name,
    string Description);
