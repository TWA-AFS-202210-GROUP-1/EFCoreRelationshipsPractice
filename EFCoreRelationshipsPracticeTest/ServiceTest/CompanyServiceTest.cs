using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Repository;
using EFCoreRelationshipsPractice.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreRelationshipsPracticeTest.ServiceTest
{
  [Collection("Sequential")]
  public class CompanyServiceTest : TestBase
  {
    public CompanyServiceTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_create_company_successfully_via_company_service()
    {
      // give
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
          CertId = "100",
        },
      };
      CompanyService companyService = new CompanyService(context);

      // when
      await companyService.AddCompany(companyDto);

      // then
      Assert.Equal(1, context.Companies.Count());
    }

    [Fact]
    public async Task Should_get_all_companies_successfully_via_company_service()
    {
      // give
      var context = GetCompanyDbContext();
      CompanyDto companyDto1 = new CompanyDto
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

      CompanyDto companyDto2 = new CompanyDto
      {
        Name = "MS",
        EmployeeDtos = new List<EmployeeDto>()
                {
                    new EmployeeDto()
                    {
                        Name = "Jerry",
                        Age = 18,
                    },
                },
        ProfileDto = new ProfileDto()
        {
          RegisteredCapital = 100020,
          CertId = "101",
        },
      };
      CompanyService companyService = new CompanyService(context);
      await companyService.AddCompany(companyDto1);
      await companyService.AddCompany(companyDto2);

      // when
      var returnedCompanies = await companyService.GetAll();

      // then
      Assert.Equal(2, returnedCompanies.Count());
      Assert.Equal(companyDto2.Name, returnedCompanies[0].Name);
      Assert.Equal(companyDto1.Name, returnedCompanies[1].Name);
    }

    [Fact]
    public async Task Should_get_company_by_id_successfully_via_company_service()
    {
      // give
      var context = GetCompanyDbContext();
      CompanyDto companyDto1 = new CompanyDto
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

      CompanyDto companyDto2 = new CompanyDto
      {
        Name = "MS",
        EmployeeDtos = new List<EmployeeDto>()
                {
                    new EmployeeDto()
                    {
                        Name = "Jerry",
                        Age = 18,
                    },
                },
        ProfileDto = new ProfileDto()
        {
          RegisteredCapital = 100020,
          CertId = "101",
        },
      };
      CompanyService companyService = new CompanyService(context);
      var companyId = await companyService.AddCompany(companyDto1);
      await companyService.AddCompany(companyDto2);

      // when
      var returnedCompany = await companyService.GetById(companyId);

      // then
      Assert.Equal(companyDto1.Name, returnedCompany.Name);
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
