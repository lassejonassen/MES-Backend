using Abstractions;
using Domain.Templates.Errors;
using Domain.Templates.Repositories;

namespace Application.TemplateProperties.Commands;

public sealed record DeleteTemplatePropertyCommand(Guid Id);

public sealed class DeleteTemplatePropertyCommandHandler(
    IUnitOfWork unitOfWork,
    ITemplatePropertiesRepository propertiesRepository)
{
    public async Task<Result> Handle(DeleteTemplatePropertyCommand command, CancellationToken cancellationToken)
    {
        var property = await propertiesRepository.GetAsync(command.Id, cancellationToken);

        if (property.IsFailure)
        {
            return Result.Failure(TemplatePropertyErrors.NotFound);
        }

        propertiesRepository.Delete(property.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}