using Domain.Templates.Entities;
using WebApi.Contracts.Templates;

namespace Application.Templates.Extensions;

public static class TemplateExtensions
{
    public static TemplateDto? ToDto(this Template template)
    {
        if (template is null)
        {
            return null;
        }

        return new TemplateDto
        {
            Id = template.Id,
            BaseTemplateId = template.BaseTemplateId,
            Name = template.Name,
            BaseTemplateName = template.BaseTemplate?.Name,
            Description = template.Description,
            CreatedAtUtc = template.CreatedAtUtc,
            UpdatedAtUtc = template.UpdatedAtUtc
        };
    }
}
