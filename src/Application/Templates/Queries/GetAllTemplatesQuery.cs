using Application.Templates.Extensions;
using Domain.Templates.Repositories;
using WebApi.Contracts.Templates;

namespace Application.Templates.Queries;

public sealed record GetAllTemplatesQuery();

public sealed class GetAllTemplatesQueryHandler(
    ITemplatesRepository templatesRepository)
{
    public async Task<IReadOnlyList<TemplateDto>> Handle(GetAllTemplatesQuery query, CancellationToken cancellationToken)
    {
        var templates = await templatesRepository.GetAllAsync(cancellationToken);

        return [.. templates.Select(x => x.ToDto()!)];
    }
}