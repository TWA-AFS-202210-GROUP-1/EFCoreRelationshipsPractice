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

            return this.companyDbContext.Companies
                .Include( _ => _.Profile)
                .Include(_ => _.Employees)
                .ToList()
                .Select(company => new CompanyDto(company)).ToList();
        }

        public async Task<CompanyDto> GetById(int id)
        {
            var companyEntity = companyDbContext.Companies
                .Include(_ => _.Profile)
                .Include(_ => _.Employees)
                .FirstOrDefault(_ => _.Id == id);
            return new CompanyDto(companyEntity);
        }

        public async Task<int> AddCompany(CompanyDto companyDto)
        {
            var companyEntity = companyDto.ToEntity();
            await companyDbContext.Companies.AddAsync(companyEntity);
            await this.companyDbContext.SaveChangesAsync();

            return companyEntity.Id;

        }

        public async Task DeleteCompany(int id)
        {
            var employeeEntitiesToBeDeleted = companyDbContext.Employees.Where(_ => _.Id == id);
            companyDbContext.Employees.RemoveRange(employeeEntitiesToBeDeleted);
            var companyEntityToBeDeleted = await companyDbContext.Companies.FindAsync(id);
            companyDbContext.Companies.Remove(companyEntityToBeDeleted);

            await companyDbContext.SaveChangesAsync();
        }
    }
}