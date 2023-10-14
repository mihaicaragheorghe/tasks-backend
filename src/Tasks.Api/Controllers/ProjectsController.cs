using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Contracts;
using Tasks.Application.Tasks.Commands;
using Tasks.Domain;

namespace Tasks.Api.Controllers;

public class ProjectsController : BaseController
{
    private readonly ISender _sender;
    private readonly UserManager<User> _userManager;

    public ProjectsController(ISender sender, UserManager<User> userManager)
    {
        _sender = sender;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject(CreateProjectRequest request)
    {
        var currentUser = await _userManager.GetUserAsync(User);

        if (currentUser is null) return Unauthorized();

        return new ProjectDto(
            await _sender.Send(new CreateProjectCommand(currentUser.Id, request.Name, request.Color)));
    }
}