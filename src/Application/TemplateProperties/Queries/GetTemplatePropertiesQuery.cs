using Application.TemplateProperties.Extensions;
using Domain.Templates.Repositories;
using WebApi.Contracts.Templates;

namespace Application.TemplateProperties.Queries;

public record GetTemplatePropertiesQuery(Guid TemplateId);

public sealed class GetTemplatePropertiesQueryHandler(
    ITemplatePropertiesRepository propertiesRepository)
{
    public async Task<IReadOnlyList<TemplatePropertyDto>> Handle(GetTemplatePropertiesQuery query, CancellationToken cancellationToken)
    {
        var properties = await propertiesRepository.GetAllAsync(query.TemplateId, cancellationToken);

        var dtos = properties.Select(x => x.ToDto()).ToList();

        return dtos;
    }
}
