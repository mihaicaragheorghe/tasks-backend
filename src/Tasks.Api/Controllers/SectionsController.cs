using MediatR;


using Microsoft.AspNetCore.Mvc;


using Tasks.Api.Contracts;
using Application.Tasks.Queries;
using Application.Sections.Commands;

namespace Tasks.Api.Controllers;

public class SectionsController : BaseController
{
    private readonly ISender _sender;

    public SectionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<SectionDto>> CreateSection(CreateSectionRequest request)
    {
        return Ok(new SectionDto(await _sender.Send(request.ToCommand())));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SectionDto>> UpdateSection(Guid id, UpdateSectionRequest request)
    {
        return Ok(new SectionDto(await _sender.Send(request.ToCommand(id))));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSection(Guid id)
    {
        await _sender.Send(new DeleteSectionCommand(id));

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SectionDto>>> GetSections(Guid projectId)
    {
        return Ok((await _sender.Send(new GetSectionsQuery(projectId)))
            .Select(section => new SectionDto(section))
            .ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SectionDto>> GetSection(Guid id)
    {
        var section = await _sender.Send(new GetSectionByIdQuery(id));

        return section is null ? Unauthorized() : new SectionDto(section);
    }
}