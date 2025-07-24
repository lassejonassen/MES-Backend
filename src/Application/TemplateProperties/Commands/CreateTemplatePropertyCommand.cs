using Abstractions;
using Domain.Templates.Entities;
using Domain.Templates.Errors;
using Domain.Templates.Repositories;

namespace Application.TemplateProperties.Commands;

public record CreateTemplatePropertyCommand
{
    public Guid TemplateId { get; init; }
    public required string Name { get; init; }
    public required string Type { get; init; }
    public string? Description { get; init; }
    public bool IsRequired { get; init; }
    public bool IsReadOnly { get; init; } = false;
    public string? DefaultValue { get; init; }
}

public sealed class CreateTemplatePropertyCommandHandler(
    IUnitOfWork unitOfWork,
    ITemplatePropertiesRepository propertiesRepository,
    ITemplatesRepository templatesRepository)
{
    public async Task<Result<Guid>> Handle(CreateTemplatePropertyCommand request, CancellationToken cancellationToken)
    {
        var template = await templatesRepository.GetAsync(request.TemplateId, cancellationToken);

        if (template.IsFailure)
        {
            return Result.Failure<Guid>(TemplateErrors.NotFound);
        }

        var property = TemplateProperty.Create(
                        request.TemplateId,
                        request.Name,
                        request.Type,
                        request.Description,
                        request.IsRequired,
                        request.IsReadOnly,
                        DateTime.UtcNow,
                        request.DefaultValue);

        propertiesRepository.Add(property);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return property.Id;
    }
}
