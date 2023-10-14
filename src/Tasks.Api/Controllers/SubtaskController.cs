using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Contracts;
using Tasks.Application.Tasks.Commands;
using Tasks.Domain;

namespace Tasks.Api.Controllers;

public class SubtasksController : BaseController
{
    private readonly ISender _sender;

    public SubtasksController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<Subtask>> CreateSubtask(CreateSubtaskRequest command)
    {
        return Ok(await _sender.Send(new CreateSubtaskCommand(command.ParentId, command.Title)));
    }
}