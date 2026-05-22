using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models.Us

namespace backend.Services;

public class UserService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public static UserDTO MapToDTO(User user)
    {
        return new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Status = user.Status,
            CreatedAt = user.CreatedAt,
            IsOwner = user.Company != null && user.Company.OwnerId == user.Id
        };
    }

    // TODO: GetAllAsync
    // public async Task<List<UserDTO>> GetAllAsync() { ... }

    // TODO: GetByIdAsync
    // public async Task<UserDTO?> GetByIdAsync(Guid id) { ... }

    // TODO: CreateAsync
    // public async Task<UserDTO> CreateAsync(CreateUserDTO dto) { ... }

    // TODO: UpdateAsync
    // public async Task<UserDTO?> UpdateAsync(Guid id, UpdateUserDTO dto) { ... }

    // TODO: DeleteAsync
    // public async Task<bool> DeleteAsync(Guid id) { ... }
}
