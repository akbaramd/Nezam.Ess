using Microsoft.AspNetCore.Mvc;
using Nezam.Admin._keenthemes.libs;

namespace Nezam.Admin.Controllers;

public class DashboardsController : Controller
{
    private readonly ILogger<DashboardsController> _logger;
    private readonly IKTTheme _theme;

    public DashboardsController(ILogger<DashboardsController> logger, IKTTheme theme)
    {
        _logger = logger;
        _theme = theme;
    }

    [HttpGet("/")]
    [HttpGet("/dashboards")]
    public IActionResult Index()
    {
        return View(_theme.GetPageView("Dashboards", "Index.cshtml"));
    }
}
