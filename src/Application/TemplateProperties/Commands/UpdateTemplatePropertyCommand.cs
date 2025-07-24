using Abstractions;
using Domain.Templates.Errors;
using Domain.Templates.Repositories;

namespace Application.TemplateProperties.Commands;

public record UpdateTemplatePropertyCommand
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Type { get; init; }
    public string? Description { get; init; }
    public bool IsRequired { get; init; }
    public string? DefaultValue { get; init; }
    public bool IsReadOnly { get; init; }
}

public sealed class UpdateTemplatePropertyCommandHandler(
    IUnitOfWork unitOfWork,
    ITemplatePropertiesRepository propertiesRepository)
{
    public async Task<Result> Handle(UpdateTemplatePropertyCommand command, CancellationToken cancellationToken)
    {
        var property = await propertiesRepository.GetAsync(command.Id, cancellationToken);

        if (property.IsFailure)
        {
            return Result.Failure(TemplatePropertyErrors.NotFound);
        }

        property.Value.Update(
            command.Name,
            command.Type,
            command.Description,
            command.IsRequired,
            command.IsReadOnly,
            command.DefaultValue);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}