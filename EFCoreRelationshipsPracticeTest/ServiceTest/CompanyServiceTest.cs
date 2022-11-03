using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Repository;
using EFCoreRelationshipsPractice.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EFCoreRelationshipsPracticeTest.ServiceTest
{
    [Collection("test")]
    public class CompanyServiceTest : TestBase
    {
        private Task<CompanyService> companyService;

        public CompanyServiceTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_company_successfully_via_company_service()
        {
            //given
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto
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
                    CertId = "100"
                },
            };
            CompanyService companyservice = new CompanyService(context);

            //when
            await companyservice.AddCompany(companyDto);

            //then
            Assert.Equal(1, context.Companies.Count());
        }

        [Fact]
        public async Task Should_get_all_companies()
        {
            //given
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto
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
                    CertId = "100"
                },
            };

            companyDto = new CompanyDto
            {
                Name = "CBA",
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
                    CertId = "102"
                },
            };

            CompanyService companyservice = new CompanyService(context);

            //when
            await companyservice.AddCompany(companyDto);
            await companyservice.AddCompany(companyDto);
            var result = await companyservice.GetAll();

            //then
            Assert.Equal(2, result.Count());
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
