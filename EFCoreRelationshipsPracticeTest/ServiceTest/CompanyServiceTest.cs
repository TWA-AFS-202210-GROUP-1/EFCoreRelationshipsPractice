using EFCoreRelationshipsPractice.Repository;
using EFCoreRelationshipsPractice.Services;
using Microsoft.Extensions.DependencyInjection;

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
      var companyDtos = PrepareTestCompanyDtos();
      CompanyService companyService = new CompanyService(context);

      // when
      await companyService.AddCompany(companyDtos[0]);

      // then
      Assert.Equal(1, context.Companies.Count());
    }

    [Fact]
    public async Task Should_get_all_companies_successfully_via_company_service()
    {
      // give
      var context = GetCompanyDbContext();
      var companyDtos = PrepareTestCompanyDtos();
      var companyService = new CompanyService(context);

      foreach (var companyDto in companyDtos)
      {
        await companyService.AddCompany(companyDto);
      }

      // when
      var returnedCompanies = await companyService.GetAll();

      // then
      Assert.Equal(2, returnedCompanies.Count);
      Assert.Equal(companyDtos[1].Name, returnedCompanies[0].Name);
      Assert.Equal(companyDtos[0].Name, returnedCompanies[1].Name);
    }

    [Fact]
    public async Task Should_get_company_by_id_successfully_via_company_service()
    {
      // give
      var context = GetCompanyDbContext();
      var companyDtos = PrepareTestCompanyDtos();
      var companyService = new CompanyService(context);

      var companyId = await companyService.AddCompany(companyDtos[0]);
      await companyService.AddCompany(companyDtos[1]);

      // when
      var returnedCompany = await companyService.GetById(companyId);

      // then
      Assert.Equal(companyDtos[0].Name, returnedCompany.Name);
      Assert.Equal(companyDtos[0].EmployeeDtos?.First().ToString(),
                   returnedCompany.EmployeeDtos?.First().ToString());
      Assert.Equal(companyDtos[0].ProfileDto?.ToString(),
                   returnedCompany.ProfileDto?.ToString());
    }

    [Fact]
    public async Task Should_delete_company_by_id_successfully_via_company_service()
    {
      // give
      var context = GetCompanyDbContext();
      var companyDtos = PrepareTestCompanyDtos();
      var companyService = new CompanyService(context);

      var companyId = await companyService.AddCompany(companyDtos[0]);
      await companyService.AddCompany(companyDtos[1]);

      // when
      await companyService.DeleteCompany(companyId);

      // then
      Assert.Equal(1, context.Companies.Count());
      Assert.Equal(companyDtos[1].Name, context.Companies.First().Name);
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
