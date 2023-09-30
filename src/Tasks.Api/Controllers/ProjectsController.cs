using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasks.Application.Tasks;

namespace Tasks.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper; 

    public ProjectsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> CreateProject(CreateProjectRequest command)
    {
        return Ok(await _sender.Send(_mapper.Map<CreateProjectCommand>(command)));
    }
}