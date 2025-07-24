using Abstractions;
using Domain.Templates.Entities;
using Domain.Templates.Errors;
using Domain.Templates.Repositories;
using Persistence.DbContexts;

namespace Persistence.Repositories;

internal class TemplatesRepository(ApplicationDbContext context) : BaseRepository<Template>(context), ITemplatesRepository
{
    public async Task<IEnumerable<Template>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Template>()
            .Include(t => t.BaseTemplate)
            .ToListAsync(cancellationToken);
    }

    public async Task<Result<Template>> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var template = await DbContext.Set<Template>()
            .Include(x => x.BaseTemplate)
            .Include(x => x.TemplateProperties)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return template is not null
            ? Result.Success(template)
            : Result.Failure<Template>(TemplateErrors.NotFound);
    }

    public async Task<Result<Template>> GetByName(string name, CancellationToken cancellationToken = default)
    {
        var template = await DbContext.Set<Template>()
            .Include(x => x.BaseTemplate)
            .Include(x => x.TemplateProperties)
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return template is not null
            ? Result.Success(template)
            : Result.Failure<Template>(TemplateErrors.NotFound);
    }

    public Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<Template>()
            .AllAsync(x => x.Name != name, cancellationToken);
    }
}
