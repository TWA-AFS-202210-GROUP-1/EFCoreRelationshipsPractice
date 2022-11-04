using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreRelationshipsPracticeTest
{
  public class TestBase : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
  {
    public TestBase(CustomWebApplicationFactory<Program> factory)
    {
      this.Factory = factory;
    }

    protected CustomWebApplicationFactory<Program> Factory { get; }

    public void Dispose()
    {
      var scope = Factory.Services.CreateScope();
      var scopedServices = scope.ServiceProvider;
      var context = scopedServices.GetRequiredService<CompanyDbContext>();

      context.Employees.RemoveRange(context.Employees);
      context.Companies.RemoveRange(context.Companies);
      context.Profiles.RemoveRange(context.Profiles);

      context.SaveChanges();
    }

    protected static List<CompanyDto> PrepareTestCompanyDtos()
    {
      var companyDtos = new List<CompanyDto>
      {
        new CompanyDto
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
        },
        new CompanyDto
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
        },
      };

      return companyDtos;
    }

    protected HttpClient GetClient()
    {
      return Factory.CreateClient();
    }
  }
}