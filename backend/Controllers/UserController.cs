using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.Models.User;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseController
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetUsers()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUser(Guid id)
    {
        var result = await _userService.GetByIdAsync(id);

        if (result.IsError) return HandleError(result.Errors);
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> CreateUser([FromBody] UserDTO userDTO)
    {
        var result = await _userService.CreateAsync(userDTO);

        if (result.IsError) return HandleError(result.Errors);
        return Ok(result.Value);
    }
}