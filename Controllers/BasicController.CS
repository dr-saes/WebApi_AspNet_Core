
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;

namespace WebApi_AspNet_Core;

[ApiController]
public abstract class BasicController : ControllerBase
{
    protected IActionResult CreateResponse(HttpStatusCode statusCode)
    {
        var contentTypes = new MediaTypeCollection { System.Net.Mime.MediaTypeNames.Application.Json };
        var result = new ObjectResult(new { statusCode, currentDate = DatetimeUtils.getCurrentDateTime() });
        result.StatusCode = (int)statusCode;
        result.Value = null;
        result.ContentTypes = contentTypes;
        return result;
    }

    protected IActionResult CreateResponse<T>(HttpStatusCode statusCode, T content)
    {
        var contentTypes = new MediaTypeCollection { System.Net.Mime.MediaTypeNames.Application.Json };
        var result = new ObjectResult(new { statusCode, currentDate = DatetimeUtils.getCurrentDateTime() });
        result.StatusCode = (int)statusCode;
        result.Value = content;
        result.ContentTypes = contentTypes;
        return result;
    }
}