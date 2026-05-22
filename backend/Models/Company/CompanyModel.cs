using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models.User;

namespace backend.Models.Company;

public class CompanyModel
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public Guid? OwnerId { get; set; }
    [ForeignKey(nameof(OwnerId))]
    public UserModel? Owner { get; set; }
    [InverseProperty(nameof(UserModel.Company))]
    public ICollection<UserModel> Users { get; set; } = new List<UserModel>();
}
