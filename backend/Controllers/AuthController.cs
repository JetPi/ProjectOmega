using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.Models.User;
using backend.Models.Auth;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly AuthService _authService;

    // Inject the service
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginDTO loginDTO)
    {
        var result = await _authService.LoginAsync(loginDTO);

        if (result.IsError) return HandleError(result.Errors);
        return Ok(result.Value);
    }
}