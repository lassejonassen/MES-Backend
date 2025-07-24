using Abstractions;
using Application.Templates.Commands;
using Application.Templates.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Templates;

namespace WebApi.Controllers;

public class TemplatesController : BaseController
{
    [ProducesResponseType(typeof(TemplateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{id}")]
    //[Authorize(Policy = Permissions.CanReadTemplates)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await Bus.InvokeAsync<Result<TemplateDto>>(new GetTemplateQuery(id), cancellationToken);

        return Ok(result.Value);
    }

    [ProducesResponseType(typeof(IReadOnlyList<TemplateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    //[Authorize(Policy = Permissions.CanReadTemplates)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await Bus.InvokeAsync<IReadOnlyList<TemplateDto>>(new GetAllTemplatesQuery(), cancellationToken);

        return Ok(result);
    }

    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpPost]
    //[Authorize(Policy = Permissions.CanCreateTemplate)]
    public async Task<IActionResult> Create([FromBody] CreateTemplateRequest request, CancellationToken cancellationToken)
    {
        CreateTemplateCommand command = new(request.Name, request.Description);

        var result = await Bus.InvokeAsync<Result<Guid>>(command, cancellationToken);

        if (result.IsFailure)
        {
            return Problem(
                title: result.Error.Code,
                detail: result.Error.Name,
                statusCode: StatusCodes.Status400BadRequest);
        }

        return CreatedAtAction(nameof(Get), new { id = result.Value }, result.Value);
    }

    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpPost("{id}/derived")]
    //[Authorize(Policy = Permissions.CanCreateTemplate)]
    public async Task<IActionResult> CreateDerived(Guid id, [FromBody] CreateDerivedTemplateRequest request, CancellationToken cancellationToken)
    {
        if (id != request.BaseTemplateId)
        {
            return BadRequest();
        }

        CreateDerivedTemplateCommand command = new(request.BaseTemplateId, request.Name, request.Description);

        var result = await Bus.InvokeAsync<Result<Guid>>(command, cancellationToken);

        if (result.IsFailure)
        {
            return Problem(
                title: result.Error.Code,
                detail: result.Error.Name,
                statusCode: StatusCodes.Status400BadRequest);
        }

        return CreatedAtAction(nameof(Get), new { id = result.Value }, result.Value);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpPut("{id}")]
    //[Authorize(Policy = Permissions.CanUpdateTemplate)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTemplateRequest request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }

        UpdateTemplateCommand command = new(request.Id, request.Name, request.Description);

        var result = await Bus.InvokeAsync<Result>(command, cancellationToken);

        if (result.IsFailure)
        {
            return Problem(
                title: result.Error.Code,
                detail: result.Error.Name,
                statusCode: StatusCodes.Status400BadRequest);
        }

        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    //[Authorize(Policy = Permissions.CanDeleteTemplate)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await Bus.InvokeAsync<Result>(new DeleteTemplateCommand(id), cancellationToken);

        if (result.IsFailure)
        {
            return Problem(
                title: result.Error.Code,
                detail: result.Error.Name,
                statusCode: StatusCodes.Status400BadRequest);
        }

        return NoContent();
    }
}
