using TypeGen.Core.TypeAnnotations;
using backend.Models.Company;

namespace backend.Models.User;

[ExportTsInterface]
public class UserDTO
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public CompanyDTO? Company { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public bool IsOwner { get; set; }
    public DateTime CreatedAt { get; set; }
}
