using backend.Models.Company;
using ErrorOr;

public interface ICompanyService
{
    Task<List<CompanyDTO>> GetAllAsync(List<string>? includes = null);
    Task<ErrorOr<CompanyDTO>> GetByIdAsync(Guid id, List<string>? includes = null);
    Task<ErrorOr<CompanyDTO>> CreateAsync(CompanyDTO companyDTO);
    // Task<ErrorOr<CompanyDTO>> UpdateAsync(CompanyDTO companyDTO);
    // Task<ErrorOr<CompanyDTO>> DeleteAsync(Guid id);
}