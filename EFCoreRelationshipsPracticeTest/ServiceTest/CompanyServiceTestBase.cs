using EFCoreRelationshipsPractice.Repository;
using EFCoreRelationshipsPractice.Services;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationshipsPracticeTest.ServiceTest
{
    public class CompanyServiceTestBase : IDisposable
    {
        private readonly CompanyService _companyService;
        private readonly CompanyDbContext _companyDbContext;
        public CompanyServiceTestBase()
        {
            var options = new DbContextOptionsBuilder<CompanyDbContext>()
                .UseInMemoryDatabase(databaseName: "DB")
                .Options;

            _companyDbContext = new CompanyDbContext(options);
            _companyService = new CompanyService(_companyDbContext);
        }

        public void Dispose()
        {
            _companyDbContext.Dispose();
        }
    }
}
