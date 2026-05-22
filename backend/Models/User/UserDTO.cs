using TypeGen.Core.TypeAnnotations;
using backend.Models.Company;

namespace backend.Models.User;

[ExportTsInterface]
public class UserDTO
{
    public Guid Id { get; set; }

    // Scalar FK — always populated so the frontend always knows which company this user belongs to
    public Guid CompanyId { get; set; }

    // Full company object — null unless the controller explicitly loads it (e.g. via EF Include)
    // Frontend can check: if (user.company) { ... } else { fetch by user.companyId }
    public CompanyDTO? Company { get; set; }

    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public bool IsOwner { get; set; }
    public DateTime CreatedAt { get; set; }
}
