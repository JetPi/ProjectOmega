using backend.Data;
using backend.Models.User;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace backend.Services;

public class UserService(AppDbContext context, IPasswordHasher<UserModel> passwordHasher) : IUserService
{
    private readonly AppDbContext _context = context;
    private readonly IPasswordHasher<UserModel> _passwordHasher = passwordHasher;

    public async Task<List<UserDTO>> GetAllAsync(List<string>? includes = null)
    {
        return await _context.Users.Include(u => u.Company).Select(u => MapToDTO(u, includes)).ToListAsync();
    }

    public async Task<ErrorOr<UserDTO>> GetByIdAsync(Guid id, List<string>? includes = null)
    {
        var user = await _context.Users.Include(u => u.Company).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return Error.NotFound(description: "User not found");
        
        return MapToDTO(user, includes);
    }

    public async Task<ErrorOr<UserDTO>> CreateAsync(UserDTO userDTO)
    {
        if (string.IsNullOrEmpty(userDTO.Password)) return Error.Validation(description: "Password is required");

        var user = MapToModel(userDTO);
        user.PasswordHash = _passwordHasher.HashPassword(user, userDTO.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return MapToDTO(user);
    }

    private static UserModel MapToModel(UserDTO dto) => new()
    {
        Username = dto.Username,
        Email = dto.Email,
        CompanyId = dto.CompanyId,
        Role = dto.Role,
    };

    public static UserDTO MapToDTO(UserModel user, List<string>? includes = null)
    {
        List<string> safeIncludes = includes ?? new List<string>();

        return new UserDTO
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt,
            CompanyId = user.CompanyId,
            Company = safeIncludes.Select(i => i.ToLower()).Contains("company") && user.Company != null ? CompanyService.MapToDTO(user.Company) : null,
            IsOwner = user.Company != null && user.Company.OwnerId == user.Id
        };
    }

    // TODO: CreateAsync
    // public async Task<UserDTO> CreateAsync(CreateUserDTO dto) { ... }

    // TODO: UpdateAsync
    // public async Task<UserDTO?> UpdateAsync(Guid id, UpdateUserDTO dto) { ... }

    // TODO: DeleteAsync
    // public async Task<bool> DeleteAsync(Guid id) { ... }
}
