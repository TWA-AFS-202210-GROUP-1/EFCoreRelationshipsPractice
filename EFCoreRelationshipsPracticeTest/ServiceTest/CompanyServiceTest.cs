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
            CompanyService companyService = new CompanyService(context);

            // when
            await companyService.AddCompany(CompanyDto1());

            // then
            Assert.Equal(1, context.Companies.Count());
        }

        [Fact]
        public async Task Should_get_all_companies_surcess_via_company_service()
        {
            // given
            var context = GetCompanyDbContext();
            CompanyService companyService = new CompanyService(context);

            await companyService.AddCompany(CompanyDto1());
            await companyService.AddCompany(CompanyDto2());

            // when
            var allCompanies = await companyService.GetAll();

            // then
            Assert.Equal(2, allCompanies.Count());
        }

        [Fact]
        public async Task Should_get_company_by_id_via_company_service()
        {
            // given
            var context = GetCompanyDbContext();

            CompanyService companyService = new CompanyService(context);

            var id = await companyService.AddCompany(CompanyDto1());
            await companyService.AddCompany(CompanyDto2());

            // when
            var companyDtoGet = await companyService.GetById(id);

            // then
            Assert.Equal(CompanyDto1().Name, companyDtoGet.Name);
        }

        [Fact]
        public async Task Should_delete_company_by_id_via_company_service()
        {
            // given
            var context = GetCompanyDbContext();

            CompanyService companyService = new CompanyService(context);

            var id = await companyService.AddCompany(CompanyDto1());
            await companyService.AddCompany(CompanyDto2());

            // when
            await companyService.DeleteCompany(id);

            // then
            Assert.Equal(1, context.Companies.Count());
            Assert.Equal(CompanyDto2().Name, context.Companies.ToList()[0].Name);
        }

        private CompanyDto CompanyDto1()
        {
            CompanyDto companyDto = new CompanyDto()
            {
                Name = "IBM",
                EmployeeDtos = new List<EmployeeDto>()
                {
                    new EmployeeDto()
                    {
                        Name = "Tom",
                        Age = 19,
                    },
                },
                ProfileDto = new ProfileDto()
                {
                    RegisteredCapital = 100010,
                    CertId = "100",
                },
            };
            return companyDto;
        }

        private CompanyDto CompanyDto2()
        {
            CompanyDto companyDto = new CompanyDto()
            {
                Name = "SLB",
                EmployeeDtos = new List<EmployeeDto>()
                {
                    new EmployeeDto()
                    {
                        Name = "Emilie",
                        Age = 20,
                    },
                },
                ProfileDto = new ProfileDto()
                {
                    RegisteredCapital = 100020,
                    CertId = "150",
                },
            };
            return companyDto;
        }
    }
}