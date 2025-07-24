using Abstractions;
using Domain.Templates.Repositories;
using WebApi.Contracts.Templates;
using Application.TemplateProperties.Extensions;

namespace Application.TemplateProperties.Queries;

public record GetTemplatePropertyQuery(Guid Id);

public sealed class GetTemplatePropertyQueryHandler(ITemplatePropertiesRepository repository)
{
    public async Task<Result<TemplatePropertyDto>> Handle(GetTemplatePropertyQuery request, CancellationToken cancellationToken)
    {
        var templateProperty = await repository.GetAsync(request.Id, cancellationToken);

        if (templateProperty.IsFailure)
        {
            return Result.Failure<TemplatePropertyDto>(templateProperty.Error);
        }

        return templateProperty.Value.ToDto();
    }
}