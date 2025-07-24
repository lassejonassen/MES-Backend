using Domain.Templates.Entities;
using WebApi.Contracts.Templates;

namespace Application.TemplateProperties.Extensions;

public static class TemplatePropertyExtensions
{
    public static TemplatePropertyDto ToDto(this TemplateProperty templateProperty)
    {
        return new TemplatePropertyDto
        {
            Id = templateProperty.Id,
            TemplateId = templateProperty.TemplateId,
            Description = templateProperty.Description,
            DefaultValue = templateProperty.DefaultValue,
            Name = templateProperty.Name,
            Type = templateProperty.Type.ToString(),
            IsRequired = templateProperty.IsRequired,
            CreatedAtUtc = templateProperty.CreatedAtUtc,
            UpdatedAtUtc = templateProperty.UpdatedAtUtc,
            IsReadOnly = templateProperty.IsReadOnly
        };
    }
}