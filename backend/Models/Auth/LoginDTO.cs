using TypeGen.Core.TypeAnnotations;

namespace backend.Models.Auth;

[ExportTsInterface]
public class LoginDTO
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}