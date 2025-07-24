using Abstractions;
using Domain.Templates.Entities;

namespace Domain.Templates.Repositories;

public interface ITemplatePropertiesRepository : IBaseRepository<TemplateProperty>
{
    Task<IEnumerable<TemplateProperty>> GetAllAsync(Guid templateId, CancellationToken cancellationToken);
    Task<IEnumerable<TemplateProperty>> GetAllAsync(IEnumerable<Guid> templateIds, CancellationToken cancellationToken);
    Task<Result<TemplateProperty>> GetAsync(Guid id, CancellationToken cancellationToken);
}
