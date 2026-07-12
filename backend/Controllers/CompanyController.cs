using backend.Models.Auth;
using backend.Models.Company;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CompanyController : BaseController
{
    private readonly CompanyService _companyService;

    public CompanyController(CompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CompanyDTO>>> GetCompanies()
    {
        var companies = await _companyService.GetAllAsync();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDTO>> GetCompany(Guid id)
    {
        var result = await _companyService.GetByIdAsync(id);

        if (result.IsError) return HandleError(result.Errors);
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<CompanyDTO>> CreateCompany([FromBody] CompanyDTO companyDTO)
    {
        var result = await _companyService.CreateAsync(companyDTO);

        if (result.IsError) return HandleError(result.Errors);
        return Ok(result.Value);
    }
}