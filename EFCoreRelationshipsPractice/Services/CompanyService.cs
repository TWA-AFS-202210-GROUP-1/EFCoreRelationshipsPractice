using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // 1. get  companies from Db
            var companies = companyDbContext.Companies
                .Include(company => company.Profile)
                .Include(company => company.EmployeeList)
                .ToList();

            // 2. convert entity to DTO
            return companies.Select(companyEntity => new CompanyDto(companyEntity)).ToList();

        }

        public async Task<CompanyDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddCompany(CompanyDto companyDto)
        {
            CompanyEntity companyEntity = companyDto.ToCompanyEntity();

            await companyDbContext.Companies.AddAsync(companyEntity);
            await companyDbContext.SaveChangesAsync();

            return companyEntity.Id;
            //throw new NotImplementedException();
        }

        public async Task DeleteCompany(int id)
        {
            var companyEntity = await companyDbContext.Companies
                .Include(company => company.EmployeeList)
                .Include(company => company.Profile)
                .FirstOrDefaultAsync(company => company.Id.Equals(id));
            companyDbContext.Companies.Remove(companyEntity);
            await companyDbContext.SaveChangesAsync();
        }
    }
}