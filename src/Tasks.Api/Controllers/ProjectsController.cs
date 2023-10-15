using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Contracts;
using Tasks.Application.Tasks.Commands;
using Tasks.Application.Tasks.Queries;
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

        return currentUser is null
            ? Unauthorized()
            : new ProjectDto(await _sender.Send(new CreateProjectCommand(   
                currentUser.Id, request.Name, request.Color)));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
        var currentUser = await _userManager.GetUserAsync(User);

        return currentUser is null
            ? Unauthorized()
            : (await _sender.Send(new GetProjectsQuery(currentUser.Id)))
                .Select(project => new ProjectDto(project))
                .ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid id)
    {
        var project = await _sender.Send(new GetProjectByIdQuery(id));

        return project is null ? NotFound() : new ProjectDto(project);
    }
}