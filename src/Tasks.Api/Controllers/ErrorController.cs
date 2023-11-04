using System.Net;

using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tasks.Application.Core;

namespace Tasks.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public ActionResult<ErrorResponse> Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error;

        Response.StatusCode = exception switch
        {
            ServiceException appException => (int)appException.Error.HttpStatusCode,
            ValidationException => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };

        return exception switch
        {
            ServiceException appException => new ErrorResponse(appException),
            ValidationException validationException => new ErrorResponse(validationException),
            _ => ErrorResponse.InternalServerError()
        };
    }
}
