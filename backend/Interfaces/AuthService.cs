using backend.Models.Auth;
using ErrorOr;

public interface IAuthService
{
    Task<ErrorOr<string>> LoginAsync(LoginDTO loginDTO);
}