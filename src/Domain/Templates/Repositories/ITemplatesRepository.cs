using Abstractions;
using Domain.Templates.Entities;

namespace Domain.Templates.Repositories;

public interface ITemplatesRepository : IBaseRepository<Template>
{
    Task<IEnumerable<Template>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<Template>> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<Template>> GetByName(string name, CancellationToken cancellationToken = default);
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default);
}
