namespace EFCoreRelationshipsPracticeTest.ControllerTest
{
    using System.Net.Mime;
    using System.Text;
    using EFCoreRelationshipsPractice.Dtos;
    using EFCoreRelationshipsPractice.Repository;
    using EFCoreRelationshipsPractice.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    public class CompanyServiceTest : TestBase
    {
        public CompanyServiceTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_company_success_via_company_service()
        {
            // given
            var context = GetCompanyDbContext();
            CompanyService companyService = new CompanyService(context);
            CompanyDto companyDto = PrepareAddCompanyDto();
            // when
            await companyService.AddCompany(companyDto);

            // then
            Assert.Equal(1, context.Companies.Count());
        }

        [Fact]
        public async Task Should_get_company_byId_success_via_company_service()
        {
            // given
            var context = GetCompanyDbContext();
            CompanyService companyService = new CompanyService(context);
            CompanyDto companyDto = PrepareAddCompanyDto();
            var targetId = await companyService.AddCompany(companyDto);
            // when
            CompanyDto targetCompany = await companyService.GetById(targetId);

            // then
            Assert.Equal("IBM", targetCompany.Name);
        }

        [Fact]
        public async Task Should_get_all_companies_success_via_company_service()
        {
            // given
            var context = GetCompanyDbContext();
            CompanyService companyService = new CompanyService(context);
            PrepareCompaniesDto(companyService);
            //when
            List<CompanyDto> targetCompanies = await companyService.GetAll();
            //then
            Assert.Equal("slb", targetCompanies[1].Name);
            Assert.Equal(1, targetCompanies[1].EmployeesDto.Count);
        }

        [Fact]
        public async Task Should_delete_a_company_by_id_success_via_company_service()
        {
            // given
            var context = GetCompanyDbContext();
            CompanyService companyService = new CompanyService(context);
            var companiesIds = PrepareCompaniesDto(companyService);
            //when
            await companyService.DeleteCompany(await companiesIds[0]);
            //then
            Assert.Equal(1, context.Companies.Count());
        }

        private List<Task<int>> PrepareCompaniesDto(CompanyService companyService)
        {
            List<CompanyDto> companyDtos = new List<CompanyDto>()
            {
                new CompanyDto() { Name = "IBM",
                    EmployeesDto = new List<EmployeeDto>(){new EmployeeDto() { Name = "Tom", Age = 19, } },
                    ProfileDto = new ProfileDto(){RegisteredCapital = 100010, CertId = "100",}},
                new CompanyDto() { Name = "slb",
                    EmployeesDto = new List<EmployeeDto>(){new EmployeeDto() { Name = "Andy", Age = 18, } },
                    ProfileDto = new ProfileDto(){RegisteredCapital = 100010, CertId = "1000",}}
            };
            var companiesIds = companyDtos.Select(async companyDto => await companyService.AddCompany(companyDto)).ToList();
            return companiesIds;
        }

        private CompanyDto PrepareAddCompanyDto()
        {
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.EmployeesDto = new List<EmployeeDto>()
            {
                new EmployeeDto() { Name = "Tom", Age = 19, }
            };
            companyDto.ProfileDto = new ProfileDto()
            {
                RegisteredCapital = 100010,
                CertId = "100",
            };
            return companyDto;
        }

        private CompanyDbContext GetCompanyDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            CompanyDbContext context = scopedService.GetRequiredService<CompanyDbContext>();
            return context;

        }
    }
}