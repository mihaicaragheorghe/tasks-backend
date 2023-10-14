using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Contracts;
using Tasks.Application.Tasks.Commands;
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
}