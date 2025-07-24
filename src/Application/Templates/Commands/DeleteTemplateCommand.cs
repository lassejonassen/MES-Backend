using Abstractions;
using Domain.Templates.Errors;
using Domain.Templates.Repositories;

namespace Application.Templates.Commands;

public sealed record DeleteTemplateCommand(Guid Id);

public sealed class DeleteTemplateCommandHandler(
        IUnitOfWork unitOfWork,
        ITemplatesRepository templatesRepository)
{

    public async Task<Result> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await templatesRepository.GetAsync(request.Id, cancellationToken);

        if (template.IsFailure)
        {
            return Result.Failure(TemplateErrors.NotFound);
        }

        template.Value.Delete();
        templatesRepository.Delete(template.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}