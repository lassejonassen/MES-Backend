using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    private IMessageBus? _bus;
    protected IMessageBus Bus => _bus ??= HttpContext.RequestServices.GetService<IMessageBus>()!;
}
