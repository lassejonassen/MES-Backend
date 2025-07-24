using Abstractions;
using Application.TemplateProperties.Commands;
using Application.TemplateProperties.Queries;
using Domain.Templates.Enums;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Templates;

namespace WebApi.Controllers;

public class TemplatePropertiesController : BaseController
{
    [ProducesResponseType(typeof(TemplatePropertyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{id}")]
    //[Authorize(Policy = Permissions.CanReadTemplateProperties)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await Bus.InvokeAsync<Result<TemplatePropertyDto>>(new GetTemplatePropertyQuery(id), cancellationToken);

        return Ok(result.Value);
    }

    [ProducesResponseType(typeof(IReadOnlyList<TemplatePropertyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    //[Authorize(Policy = Permissions.CanReadTemplateProperties)]
    public async Task<IActionResult> GetAll([FromQuery] Guid templateId, CancellationToken cancellationToken)
    {
        var result = await Bus.InvokeAsync<IReadOnlyList<TemplatePropertyDto>>(new GetTemplatePropertiesQuery(templateId), cancellationToken);

        return Ok(result);
    }

    [ProducesResponseType(typeof(IReadOnlyList<string>), StatusCodes.Status200OK)]
    [HttpGet("Types")]
    //[Authorize(Policy = Permissions.CanReadTemplateProperties)]
    public IActionResult GetPropertyTypes()
    {
        var result = Enum.GetNames<TemplatePropertyType>();
        return Ok(result);
    }

    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpPost]
    //[Authorize(Policy = Permissions.CanCreateTemplateProperty)]
    public async Task<IActionResult> Create([FromBody] CreateTemplatePropertyRequest request, CancellationToken cancellationToken)
    {
        CreateTemplatePropertyCommand command = new()
        {
            TemplateId = request.TemplateId,
            Name = request.Name,
            Type = request.Type,
            Description = request.Description,
            IsRequired = request.IsRequired,
            DefaultValue = request.DefaultValue,
            IsReadOnly = request.IsReadOnly
        };

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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpPut("{id}")]
    //[Authorize(Policy = Permissions.CanUpdateTemplateProperty)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTemplatePropertyRequest request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }

        UpdateTemplatePropertyCommand command = new()
        {
            Id = request.Id,
            Name = request.Name,
            Type = request.Type,
            Description = request.Description,
            IsRequired = request.IsRequired,
            DefaultValue = request.DefaultValue,
            IsReadOnly = request.IsReadOnly
        };

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
    //[Authorize(Policy = Permissions.CanDeleteTemplateProperty)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await Bus.InvokeAsync<Result>(new DeleteTemplatePropertyCommand(id), cancellationToken);

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