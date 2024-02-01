using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Tasks.Api.Contracts;
using Application.Comments.Queries;
using Domain;

namespace Tasks.Api.Controllers;

public class CommentsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;

    public CommentsController(IMediator mediator, UserManager<User> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    [HttpGet("{taskId}")]
    public async Task<ActionResult<IList<CommentDto>>> GetCommentsByTaskId(Guid taskId)
    {
        return Ok((await _mediator.Send(new GetCommentsByTaskId(taskId)))
            .Select(comment => new CommentDto(comment)));
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> CreateComment(CreateCommentRequest request)
    {
        var user = await _userManager.GetUserAsync(User);

        return user is null 
            ? Unauthorized()
            : Ok(new CommentDto(await _mediator.Send(request.ToCommand(user.Id))));
    }
}