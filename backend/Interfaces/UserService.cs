using backend.Models.User;
using ErrorOr;

public interface IUserService
{
    Task<List<UserDTO>> GetAllAsync(List<string>? includes = null);
    Task<ErrorOr<UserDTO>> GetByIdAsync(Guid id, List<string>? includes = null);
    Task<ErrorOr<UserDTO>> CreateAsync(UserDTO userDTO);
    // Task<ErrorOr<UserDTO>> UpdateAsync(UserModel user);
    // Task<UserDTO?> DeleteAsync(Guid id);
}