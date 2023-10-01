using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tasks.Application.Tasks;
using Tasks.Domain;

namespace Tasks.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper; 
    private readonly UserManager<User> _userManager;

    public ProjectsController(
        ISender sender, 
        IMapper mapper,
        UserManager<User> userManager)
    {
        _sender = sender;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<ActionResult<Project>> CreateProject(CreateProjectRequest request)
    {
        var currentUser = await _userManager.GetUserAsync(User);

        if (currentUser is null) return Unauthorized();

        return await _sender.Send(
            new CreateProjectCommand(currentUser.Id, request.Name, request.Color)
        );
    }
}