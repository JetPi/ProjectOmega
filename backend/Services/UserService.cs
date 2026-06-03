using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models.User;


namespace backend.Services;

public class UserService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public static UserDTO MapToDTO(UserModel user, List<string>? includes = null)
    {
        List<string> safeIncludes = includes ?? new List<string>();

        return new UserDTO
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role.ToString(),
            CreatedAt = user.CreatedAt,
            CompanyId = user.CompanyId,
            Company = safeIncludes.Select(i => i.ToLower()).Contains("company") && user.Company != null ? CompanyService.MapToDTO(user.Company) : null,
            IsOwner = user.Company != null && user.Company.OwnerId == user.Id
        };
    }

    public async Task<List<UserDTO>> GetAllAsync(List<string>? includes = null)
    {
        return await _context.Users.Include(u => u.Company).Select(u => MapToDTO(u, includes)).ToListAsync();
    }

    public async Task<UserDTO?> GetByIdAsync(Guid id, List<string>? includes = null)
    {
        var user = await _context.Users.Include(u => u.Company).FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return null;
        return MapToDTO(user, includes);
    }

    // TODO: CreateAsync
    // public async Task<UserDTO> CreateAsync(CreateUserDTO dto) { ... }

    // TODO: UpdateAsync
    // public async Task<UserDTO?> UpdateAsync(Guid id, UpdateUserDTO dto) { ... }

    // TODO: DeleteAsync
    // public async Task<bool> DeleteAsync(Guid id) { ... }
}
