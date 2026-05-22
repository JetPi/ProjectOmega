using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models.Company;

namespace backend.Models.User;

public enum UserRole
{
    User,
    CompanyOwner,
    Admin
}

public class UserModel
{
    [Key]
    public Guid Id { get; set; }

    // Scalar FK — always present in the DB row
    public Guid CompanyId { get; set; }

    // Navigation property — loaded via EF Include() when needed
    [ForeignKey(nameof(CompanyId))]
    [InverseProperty(nameof(CompanyModel.Users))]
    public CompanyModel? Company { get; set; }

    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
