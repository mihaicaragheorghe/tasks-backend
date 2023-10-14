using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tasks.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{

}