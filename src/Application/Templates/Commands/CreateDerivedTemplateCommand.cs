using Abstractions;
using Domain.Templates.Entities;
using Domain.Templates.Errors;
using Domain.Templates.Repositories;

namespace Application.Templates.Commands;

public record CreateDerivedTemplateCommand(
    Guid? BaseTemplateId,
    string Name,
    string Description,
    string? BaseTemplateName = null);

public sealed class CreateDerivedTemplateCommandHandler(
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork,
    ITemplatesRepository templatesRepository)
{
    public async Task<Result<Guid>> Handle(CreateDerivedTemplateCommand command, CancellationToken cancellationToken)
    {
        bool isNameUnique = await templatesRepository.IsNameUniqueAsync(command.Name, cancellationToken);

        if (!isNameUnique)
        {
            return Result.Failure<Guid>(TemplateErrors.NameNotUnique);
        }

        var baseTemplate = command.BaseTemplateId.HasValue
            ? await templatesRepository.GetAsync(command.BaseTemplateId.Value, cancellationToken)
            : await templatesRepository.GetByName(command.BaseTemplateName!, cancellationToken);

        if (baseTemplate.IsFailure)
        {
            return Result.Failure<Guid>(TemplateErrors.NotFound);
        }

        var template = Template.CreateDerived(
            baseTemplate.Value.Id,
            command.Name,
            command.Description,
            dateTimeProvider.UtcNow);

        templatesRepository.Add(template.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return template.Value.Id;
    }
}