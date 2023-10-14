using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Contracts;
using Tasks.Application.Tasks.Commands;
using Tasks.Domain;

namespace Tasks.Api.Controllers;

public class TasksController : BaseController
{
    private readonly ISender _sender;
    private readonly UserManager<User> _userManager;

    public TasksController(ISender sender, UserManager<User> userManager)
    {
        _sender = sender;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> CreateTask(CreateTaskRequest request)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null) return Unauthorized();

        return Ok(new TaskDto(
            await _sender.Send(new CreateTaskCommand(
                request.ProjectId,
                request.SectionId,
                request.AssignedToUserId,
                user.Id,
                request.Title,
                request.Description,
                request.Priority,
                request.DueAtUtc,
                request.TagsIds ?? new List<Guid>(),
                request.SubtasksTitles ?? new List<string>()))));
    }
}