using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models.Company;
using backend.Models.User;

namespace backend.Services;

public class CompanyService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    /// <summary>
    /// Authoritative ownership check — queries the DB directly.
    /// Never rely on a loaded navigation property for security decisions.
    /// </summary>
    public async Task<bool> IsUserOwnerOfCompany(Guid userId, Guid companyId)
    {
        return await _context.Companies
            .AnyAsync(c => c.Id == companyId && c.OwnerId == userId);
    }

    // TODO: Add mapping methods here when ready, e.g.:
    // public CompanyDTO ToDTO(CompanyModel company, bool includeUsers = false) { ... }
    // public CompanyDTO ToDTOWithOwner(CompanyModel company) { ... }
}
