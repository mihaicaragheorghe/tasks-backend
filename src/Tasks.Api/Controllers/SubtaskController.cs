using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Contracts;
using Tasks.Application.Tasks.Commands;
using Tasks.Application.Tasks.Queries;
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
    public async Task<ActionResult<Subtask>> CreateSubtask(CreateSubtaskRequest request)
    {
        return Ok(await _sender.Send(request.ToCommand()));
    }

    [HttpPut]
    public async Task<ActionResult<Subtask>> UpdateSubtask(Guid id, UpdateSubtaskRequest request)
    {
        return Ok(await _sender.Send(request.ToCommand(id)));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSubtask(Guid id)
    {
        await _sender.Send(new DeleteSubtaskCommand(id));

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subtask>>> GetSubtasks(Guid parentId)
    {
        return Ok(await _sender.Send(new GetSubtasksQuery(parentId)));
    }
}