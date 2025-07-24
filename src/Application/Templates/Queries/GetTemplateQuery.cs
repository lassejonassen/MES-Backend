using Abstractions;
using Application.Templates.Extensions;
using Domain.Templates.Repositories;
using WebApi.Contracts.Templates;

namespace Application.Templates.Queries;

public sealed record GetTemplateQuery(Guid Id);

public sealed class GetTemplateQueryHandler(ITemplatesRepository templatesRepository)
{
    public async Task<Result<TemplateDto>> Handle(GetTemplateQuery request, CancellationToken cancellationToken)
    {
        var template = await templatesRepository.GetAsync(request.Id, cancellationToken);

        if (template.IsFailure)
        {
            return Result.Failure<TemplateDto>(template.Error);
        }

        return Result.Success(template.Value.ToDto()!);
    }
}
