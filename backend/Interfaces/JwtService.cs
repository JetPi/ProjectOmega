using backend.Models.User;

public interface IJwtService
{
    string GenerateToken(UserModel user);
}