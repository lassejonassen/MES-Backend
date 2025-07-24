using Abstractions;
using Domain.Templates.Entities;
using Domain.Templates.Errors;
using Domain.Templates.Repositories;
using Persistence.DbContexts;

namespace Persistence.Repositories;

internal class TemplatePropertiesRepository(ApplicationDbContext context)
    : BaseRepository<TemplateProperty>(context), ITemplatePropertiesRepository
{
    public async Task<IEnumerable<TemplateProperty>> GetAllAsync(Guid templateId, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TemplateProperty>()
            .Where(x => x.TemplateId == templateId)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TemplateProperty>> GetAllAsync(IEnumerable<Guid> templateIds, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TemplateProperty>()
            .Where(x => templateIds.Contains(x.TemplateId))
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Result<TemplateProperty>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var property = await DbContext.Set<TemplateProperty>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return property is not null
            ? Result.Success(property)
            : Result.Failure<TemplateProperty>(TemplatePropertyErrors.NotFound);
    }
}