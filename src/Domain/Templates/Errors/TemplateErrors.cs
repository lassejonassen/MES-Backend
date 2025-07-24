using Abstractions;
using Domain.Templates.Entities;

namespace Domain.Templates.Errors;

public static class TemplateErrors
{
    private const string EntityType = nameof(Template);

    public static readonly Error NotFound = DomainErrorFactory.NotFound(EntityType);

    public static readonly Error NameNotUnique = DomainErrorFactory.PropertyNotUnique(EntityType, nameof(Template.Name));
}
