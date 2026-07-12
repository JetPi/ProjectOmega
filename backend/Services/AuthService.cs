using backend.Data;
using backend.Models.Auth;
using backend.Models.User;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace backend.Services;

public class AuthService(AppDbContext context, IPasswordHasher<UserModel> passwordHasher, IJwtService jwtService) : IAuthService
{
    private readonly AppDbContext _context = context;
    private readonly IPasswordHasher<UserModel> _passwordHasher = passwordHasher;
    private readonly IJwtService _jwtService = jwtService;
    
    public async Task<ErrorOr<string>> LoginAsync(LoginDTO loginDTO)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);
        if (user == null) return Error.NotFound(description: "User not found");

        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDTO.Password);
        if (verificationResult != PasswordVerificationResult.Success) return Error.Validation(description: "Invalid password");

        return _jwtService.GenerateToken(user);
    }
}
