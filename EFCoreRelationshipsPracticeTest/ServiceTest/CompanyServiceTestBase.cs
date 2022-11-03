using EFCoreRelationshipsPractice.Models;
using EFCoreRelationshipsPractice.Repository;
using EFCoreRelationshipsPractice.Services;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationshipsPracticeTest.ServiceTest
{
    public class CompanyServiceTestBase : IDisposable
    {
        internal readonly CompanyService CompanyService;
        internal readonly CompanyDbContext CompanyDbContext;
        public CompanyServiceTestBase()
        {
            var options = new DbContextOptionsBuilder<CompanyDbContext>()
                .UseInMemoryDatabase(databaseName: "DB")
                .Options;

            CompanyDbContext = new CompanyDbContext(options);
            CompanyService = new CompanyService(CompanyDbContext);

            InitDataBase();
        }

        private void InitDataBase()
        {
            CompanyDbContext.Companies.AddRange(new List<CompanyEntity>()
            {
                new CompanyEntity(){Name = "SLB",Employees = GetInitEmployees(), Profile = new ProfileEntity(){CertId = "SLBCert", RegisteredCapital = 1000}},
                new CompanyEntity(){Name = "Google",Employees = GetInitEmployees(), Profile = new ProfileEntity(){CertId = "GoogleCert", RegisteredCapital = 2000}}
            });
            CompanyDbContext.SaveChanges();
        }

        private List<EmployeeEntity> GetInitEmployees()
        {
            return new List<EmployeeEntity>()
            {
                new EmployeeEntity() { Age = 20, Name = "Xu" },
                new EmployeeEntity() { Age = 30, Name = "Du" }
            };
        }

        public void Dispose()
        {
            CompanyDbContext.Dispose();
        }
    }
}
