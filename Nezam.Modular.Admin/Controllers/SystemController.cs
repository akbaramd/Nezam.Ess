using Microsoft.AspNetCore.Mvc;
using Nezam.Admin._keenthemes.libs;

namespace Nezam.Admin.Controllers;

public class SystemController : Controller
{

    private readonly ILogger<DashboardsController> _logger;
    private readonly IKTTheme _theme;

    public SystemController(ILogger<DashboardsController> logger, IKTTheme theme)
    {
        _logger = logger;
        _theme = theme;
    }

    [HttpGet("/not-found")]
    public IActionResult PageNotFound()
    {
        return View(_theme.GetPageView("System", "NotFound.cshtml"));
    }

    [HttpGet("/error")]
    public IActionResult Error()
    {
        return View(_theme.GetPageView("System", "Error.cshtml"));
    }
}
