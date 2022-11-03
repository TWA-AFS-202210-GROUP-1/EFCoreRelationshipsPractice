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
            var companies = companyDbContext.Companies.Include(company => company.Profile).Include(_ => _.Employees).ToList();
            return companies.Select(companyEntity => new CompanyDto(companyEntity)).ToList();
        }

        public async Task<CompanyDto> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddCompany(CompanyDto companyDto)
        {
            //convert dto to entity
            CompanyEntity companyEntity = companyDto.ToEntity();
            //save entity to db
            await companyDbContext.Companies.AddAsync(companyEntity);
            await companyDbContext.SaveChangesAsync();

            return companyEntity.Id;
        }

        public async Task DeleteCompany(int id)
        {
            var foundCompany = await companyDbContext.Companies.Include(company => company.Profile).Include(_ => _.Employees).FirstOrDefaultAsync(company => company.Id == id);
            companyDbContext.Companies.Remove(foundCompany);
            await companyDbContext.SaveChangesAsync();
        }
    }
}