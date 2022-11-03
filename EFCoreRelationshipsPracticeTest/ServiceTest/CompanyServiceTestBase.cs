using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Models;
using EFCoreRelationshipsPractice.Repository;
using EFCoreRelationshipsPractice.Services;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationshipsPracticeTest.ServiceTest
{
    public class CompanyServiceTestBase : IDisposable
    {
        internal CompanyService CompanyService;
        internal CompanyDbContext CompanyDbContext;
        public CompanyServiceTestBase()
        {
            var options = new DbContextOptionsBuilder<CompanyDbContext>()
                .UseInMemoryDatabase(databaseName: "DB")
                .Options;

            CompanyDbContext = new CompanyDbContext(options);
            CompanyService = new CompanyService(CompanyDbContext);
        }

        internal void InitDataBase()
        {
            CompanyDbContext.Companies.AddRange(new List<CompanyEntity>()
            {
                new CompanyEntity(){Name = "SLB",Employees = GetInitEmployees(), Profile = new ProfileEntity(){CertId = "SLBCert", RegisteredCapital = 1000}},
                new CompanyEntity(){Name = "Google",Employees = GetInitEmployees(), Profile = new ProfileEntity(){CertId = "GoogleCert", RegisteredCapital = 2000}}
            });
            CompanyDbContext.SaveChanges();
        }

        internal CompanyDto GetACompanyDto()
        {
            return new CompanyDto()
            {
                Name = "SLB", 
                EmployeeDtos = new List<EmployeeDto>() { new EmployeeDto(){Age = 18, Name = "Xu"}},
                ProfileDto = new ProfileDto() { CertId = "SLBCert", RegisteredCapital = 1000 }
            };
        }

        internal List<EmployeeEntity> GetInitEmployees()
        {
            return new List<EmployeeEntity>()
            {
                new EmployeeEntity() { Age = 20, Name = "Xu" },
                new EmployeeEntity() { Age = 30, Name = "Du" }
            };
        }

        public void Dispose()
        {
            CompanyDbContext.RemoveRange(CompanyDbContext.Employees);
            CompanyDbContext.RemoveRange(CompanyDbContext.Companies);
            CompanyDbContext.RemoveRange(CompanyDbContext.Profiles);
            CompanyDbContext.SaveChanges();
        }
    }
}
