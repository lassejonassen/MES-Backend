using Abstractions;
using Domain.Templates.Errors;
using Domain.Templates.Repositories;

namespace Application.Templates.Commands;

public record UpdateTemplateCommand(Guid Id, string Name, string Description);

public sealed class UpdateTemplateCommandHandler(
    IUnitOfWork unitOfWork,
    ITemplatesRepository templatesRepository)
{
    public async Task<Result> Handle(UpdateTemplateCommand command, CancellationToken cancellationToken)
    {
        var template = await templatesRepository.GetAsync(command.Id, cancellationToken);

        if (template.IsFailure)
        {
            return Result.Failure(TemplateErrors.NotFound);
        }

        template.Value.Update(command.Name, command.Description);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}