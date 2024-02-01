using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Contracts;
using Application.Tasks.Queries;
using Domain;
using Application.Subtasks.Commands;

namespace Tasks.Api.Controllers;

public class SubtasksController(ISender sender) : BaseController
{
    [HttpPost]
    public async Task<ActionResult<Subtask>> CreateSubtask(CreateSubtaskRequest request)
    {
        return Ok(await sender.Send(request.ToCommand()));
    }

    [HttpPut]
    public async Task<ActionResult<Subtask>> UpdateSubtask(Guid id, UpdateSubtaskRequest request)
    {
        return Ok(await sender.Send(request.ToCommand(id)));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSubtask(Guid id)
    {
        await sender.Send(new DeleteSubtaskCommand(id));

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subtask>>> GetSubtasks(Guid parentId)
    {
        return Ok(await sender.Send(new GetSubtasksQuery(parentId)));
    }
}