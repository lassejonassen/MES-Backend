using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Contracts.Templates;

public sealed record TemplateDto
{
    public required Guid Id { get; init; }
    public Guid? BaseTemplateId { get; init; }
    public required string Name { get; init; }
    public string? BaseTemplateName { get; init; }
    public string? Description { get; init; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}
