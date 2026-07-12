using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models.Company;
using backend.Models.User;
using ErrorOr;

namespace backend.Services;

public class CompanyService(AppDbContext context) : ICompanyService
{
    private readonly AppDbContext _context = context;

    public async Task<List<CompanyDTO>> GetAllAsync(List<string>? includes = null)
    {
        return await _context.Companies.Select(c => MapToDTO(c, includes)).ToListAsync();
    }

    public async Task<ErrorOr<CompanyDTO>> GetByIdAsync(Guid id, List<string>? includes = null)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);

        if (company == null) return Error.NotFound(description: "Company not found");

        return MapToDTO(company, includes);
    }

    public async Task<ErrorOr<CompanyDTO>> CreateAsync(CompanyDTO companyDTO)
    {
        var company = await MapToModel(companyDTO);

        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return MapToDTO(company);
    }

    private async Task<CompanyModel> MapToModel(CompanyDTO dto)
    {
        var userIds = dto.UserIds ?? [];
        var users = await _context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();

        return new CompanyModel()
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address,
            OwnerId = dto.OwnerId,
            CreatedAt = DateTime.UtcNow,
            Users = users,
        };
    }


    public static CompanyDTO MapToDTO(CompanyModel company, List<string>? includes = null)
    {
        List<string> safeIncludes = includes ?? new List<string>();

        return new CompanyDTO
        {
            Id = company.Id,
            Name = company.Name,
            OwnerId = company.OwnerId,
            CreatedAt = company.CreatedAt,
            Users = safeIncludes.Select(i => i.ToLower()).Contains("users") && company.Users != null ? company.Users.Select(u => UserService.MapToDTO(u)).ToList() : []
        };
    }

    public async Task<bool> IsUserOwnerOfCompany(Guid userId, Guid companyId)
    {
        return await _context.Companies
            .AnyAsync(c => c.Id == companyId && c.OwnerId == userId);
    }

}
