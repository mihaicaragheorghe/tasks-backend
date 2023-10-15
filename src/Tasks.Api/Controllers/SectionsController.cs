using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Contracts;
using Tasks.Application.Tasks.Commands;
using Tasks.Application.Tasks.Queries;
using Tasks.Domain;

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
        return Ok(new SectionDto(
            await _sender.Send(new CreateSectionCommand(request.ProjectId, request.Name))));
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