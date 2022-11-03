using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;
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
            // 1. get company from DB
            var companies = companyDbContext.Companies
                .Include(company => company.Profile)
                .Include(company => company.Employees)
                .ToList();

            // 2. convert entity to Dto
            return companies.Select(companyEntity => new CompanyDto(companyEntity)).ToList();
        }

        public async Task<CompanyDto> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddCompany(CompanyDto companyDto)
        {
            // 1. convert Dto to entity
            CompanyEntity companyEntity = companyDto.ToEntity();

            // 2. save entity to db

            // addAsync
            // saveChangesAsync
            await companyDbContext.Companies.AddAsync(companyEntity); // companyEntity 同步数据库数据
            await companyDbContext.SaveChangesAsync();

            // 3. return company id
            return companyEntity.Id;
        }

        public async Task DeleteCompany(int id)
        {
            throw new NotImplementedException();
        }
    }
}