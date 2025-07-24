using Abstractions;
using Domain.Templates.Entities;
using Domain.Templates.Errors;
using Domain.Templates.Repositories;

namespace Application.Templates.Commands;

public sealed record CreateTemplateCommand(string Name, string Description);

public sealed class CreateTemplateCommandHandler(
    IUnitOfWork unitOfWork,
    ITemplatesRepository templatesRepository)
{
    public async Task<Result<Guid>> Handle(CreateTemplateCommand command, CancellationToken cancellationToken)
    {
        bool isNameUnique = await templatesRepository.IsNameUniqueAsync(command.Name, cancellationToken);

        if (!isNameUnique)
        {
            return Result.Failure<Guid>(TemplateErrors.NameNotUnique);
        }

        var template = Template.Create(command.Name, command.Description, DateTime.UtcNow);

        templatesRepository.Add(template.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return template.Value.Id;
    }
}