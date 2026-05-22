using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.Models.User;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    // Inject the service
    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUser(Guid id)
    {
        var userDto = await _userService.GetUserByIdAsync(id);

        if (userDto == null) return NotFound();

        return Ok(userDto);
    }
}