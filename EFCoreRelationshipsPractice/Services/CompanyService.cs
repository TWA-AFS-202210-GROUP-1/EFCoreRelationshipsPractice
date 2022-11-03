using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Model;
using EFCoreRelationshipsPractice.Repository;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationshipsPractice.Services
{
  public class CompanyService
  {
    private readonly CompanyDbContext companyDbContext;

    public CompanyService(CompanyDbContext companyDbContext)
    {
      this.companyDbContext = companyDbContext;
    }

    public async Task<List<CompanyDto>> GetAll()
    {
      var companies = companyDbContext.Companies
        .Include(companyEntity => companyEntity.Profile)
        .Include(companyEntity => companyEntity.Employees)
        .ToList();

      return companies.Select(companyEntity => new CompanyDto(companyEntity)).ToList();
    }

    public async Task<CompanyDto> GetById(long id)
    {
      var foundCompany = await companyDbContext.Companies
        .Include(companyEntity => companyEntity.Profile)
        .Include(companyEntity => companyEntity.Employees)
        .FirstOrDefaultAsync(company => company.Id == id);

      return new CompanyDto(foundCompany);
    }

    public async Task<int> AddCompany(CompanyDto companyDto)
    {
      CompanyEntity companyEntity = companyDto.ToEntity();
      await companyDbContext.Companies.AddAsync(companyEntity);
      await companyDbContext.SaveChangesAsync();

      return companyEntity.Id;
    }

    public async Task DeleteCompany(int id)
    {
      var foundCompany = await companyDbContext.Companies
        .Include(companyEntity => companyEntity.Profile)
        .Include(companyEntity => companyEntity.Employees)
        .FirstOrDefaultAsync(company => company.Id == id);

      companyDbContext.Companies.Remove(foundCompany);
      companyDbContext.Profiles.Remove(foundCompany.Profile);
      await companyDbContext.SaveChangesAsync();
    }
  }
}