using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Services;
using backend.Models.Auth;
using System.Security.Claims;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginDTO loginDTO)
    {
        var result = await _authService.LoginAsync(loginDTO);

        if (result.IsError) return HandleError(result.Errors);
        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet("me")]
    public ActionResult<object> Me()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var role = User.FindFirstValue(ClaimTypes.Role);
        var companyId = User.FindFirstValue("CompanyId");

        return Ok(new
        {
            UserId = userId,
            Role = role,
            CompanyId = companyId,
            IsAuthenticated = User.Identity?.IsAuthenticated ?? false
        });
    }
}