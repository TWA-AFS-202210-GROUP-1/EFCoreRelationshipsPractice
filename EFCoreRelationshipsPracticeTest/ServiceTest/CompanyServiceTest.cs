using EFCoreRelationshipsPractice.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreRelationshipsPracticeTest.ControllerTest
{
    using System.Net.Mime;
    using System.Text;
    using EFCoreRelationshipsPractice.Dtos;
    using EFCoreRelationshipsPractice.Services;
    using Newtonsoft.Json;

    [Collection("sameCollection")]
    public class CompanyServiceTest : TestBase
    {
        public CompanyServiceTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        private CompanyDbContext GetCompanyDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            CompanyDbContext context = scopedService.GetRequiredService<CompanyDbContext>();
            return context;
        }

        [Fact]
        public async Task Should_create_company_success_via_company_service()
        {
            // given
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.EmployeeDtos = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 9,
                },
            };

            companyDto.ProfileDto = new ProfileDto()
            {
                RegisteredCapital = 100010,
                CertId = "100",
            };

            CompanyService companyService = new CompanyService(context);

            // when
            await companyService.AddCompany(companyDto);

            // then
            Assert.Equal(1, context.Companies.Count());
        }

        [Fact]
        public async Task Should_get_all_companies_surcess_via_company_service()
        {
            // given
            var context = GetCompanyDbContext();
            CompanyDto companyDto1 = new CompanyDto();
            companyDto1.Name = "IBM";

            CompanyDto companyDto2 = new CompanyDto();
            companyDto2.Name = "SLB";

            CompanyService companyService = new CompanyService(context);

            await companyService.AddCompany(companyDto1);
            await companyService.AddCompany(companyDto2);

            // when
            var allCompanies = await companyService.GetAll();

            // then
            Assert.Equal(2, allCompanies.Count());
        }
    }
}