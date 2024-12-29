using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Nezam.Admin._keenthemes.libs;
using System.Security.Claims;
using Nezam.EES.Service.Identity.Domains.Users.DomainServices;
using Nezam.EEs.Shared.Domain.Identity.User;

namespace Nezam.Admin.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly IKTTheme _theme;
    private readonly IUserDomainService _userDomainService;

    public AuthController(ILogger<AuthController> logger, IKTTheme theme, IUserDomainService userDomainService)
    {
        _logger = logger;
        _theme = theme;
        _userDomainService = userDomainService;
    }

    [HttpGet("/signin")]
    public IActionResult SignIn()
    {
        return View(_theme.GetPageView("Auth", "SignIn.cshtml"));
    }

    [HttpPost("/signin")]
    public async Task<IActionResult> SignIn(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            ModelState.AddModelError(string.Empty, "Email and password are required.");
            return View(_theme.GetPageView("Auth", "SignIn.cshtml"));
        }

        var userResult = await _userDomainService.GetUserByUsernameAsync(UserNameId.NewId(email));

        if (!userResult.IsSuccess || userResult.Data == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(_theme.GetPageView("Auth", "SignIn.cshtml"));
        }

        var user = userResult.Data;

        if (!user.ValidatePassword(password))
        {
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(_theme.GetPageView("Auth", "SignIn.cshtml"));
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.Value.ToString()),
            new Claim(ClaimTypes.Name, user.UserName.Value),
            // Add additional claims as needed
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("/signup")]
    public IActionResult SignUp()
    {
        return View(_theme.GetPageView("Auth", "SignUp.cshtml"));
    }

    [HttpGet("/reset-password")]
    public IActionResult ResetPassword()
    {
        return View(_theme.GetPageView("Auth", "ResetPassword.cshtml"));
    }

    [HttpGet("/new-password")]
    public IActionResult NewPassword()
    {
        return View(_theme.GetPageView("Auth", "NewPassword.cshtml"));
    }

    [HttpPost("/signout")]
    public async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("SignIn", "Auth");
    }

    private bool ValidatePassword(string password, string hashedPassword)
    {
        // Implement your password validation logic here
        return password == hashedPassword; // Replace with a secure hashing check
    }
}
