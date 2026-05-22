using TypeGen.Core.TypeAnnotations;
using backend.Models.User;

namespace backend.Models.Company;

[ExportTsInterface]
public class CompanyDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid? OwnerId { get; set; }
    public UserDTO? Owner { get; set; }
    public List<Guid> UserIds { get; set; } = [];
    public List<UserDTO>? Users { get; set; }
}
