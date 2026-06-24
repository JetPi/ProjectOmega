using backend.Models.User;

public interface IUserService
{
    Task<UserDTO> LoginAsync(string email, string password);
    Task<List<UserDTO>> GetAllAsync(List<string>? includes = null);
    Task<UserDTO?> GetByIdAsync(Guid id, List<string>? includes = null);
    Task<UserDTO?> CreateAsync(UserDTO userDTO);
    // Task<UserDTO?> UpdateAsync(UserModel user);
    // Task<UserDTO?> DeleteAsync(Guid id);
}