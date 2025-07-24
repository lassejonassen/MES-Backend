using Abstractions;
using Domain.Templates.Entities;

namespace Domain.Templates.Errors;

public static class TemplatePropertyErrors
{
    private const string EntityType = nameof(TemplateProperty);

    public static readonly Error NotFound = DomainErrorFactory.NotFound(EntityType);
}
