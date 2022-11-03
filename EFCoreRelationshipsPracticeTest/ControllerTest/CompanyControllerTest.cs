namespace EFCoreRelationshipsPracticeTest.ControllerTest
{
  using System.Net.Mime;
  using System.Text;
  using EFCoreRelationshipsPractice.Dtos;
  using Newtonsoft.Json;

  [Collection("Sequential")]
  public class CompanyControllerTest : TestBase
  {
    public CompanyControllerTest(CustomWebApplicationFactory<Program> factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_create_company_successfully()
    {
      // given
      var client = GetClient();
      CompanyDto companyDto = new CompanyDto
      {
        Name = "IBM",
      };

      // when
      var httpContent = JsonConvert.SerializeObject(companyDto);
      StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
      await client.PostAsync("/companies", content);

      // then
      var allCompaniesResponse = await client.GetAsync("/companies");
      var body = await allCompaniesResponse.Content.ReadAsStringAsync();

      var returnCompanies = JsonConvert.DeserializeObject<List<CompanyDto>>(body);

      Assert.Single(returnCompanies);
    }

    [Fact]
    public async Task Should_create_company_with_profile_successfully()
    {
      // given
      var client = GetClient();
      CompanyDto companyDto = new CompanyDto
      {
        Name = "IBM",
        ProfileDto = new ProfileDto()
        {
          RegisteredCapital = 100010,
          CertId = "100",
        },
      };

      // when
      var httpContent = JsonConvert.SerializeObject(companyDto);
      StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
      await client.PostAsync("/companies", content);

      // then
      var allCompaniesResponse = await client.GetAsync("/companies");
      var body = await allCompaniesResponse.Content.ReadAsStringAsync();

      var returnCompanies = JsonConvert.DeserializeObject<List<CompanyDto>>(body);

      Assert.Single(returnCompanies);
      Assert.Equal(companyDto.ProfileDto.CertId, returnCompanies[0].ProfileDto?.CertId);
      Assert.Equal(companyDto.ProfileDto.RegisteredCapital,
                   returnCompanies[0].ProfileDto?.RegisteredCapital);
    }

    [Fact]
    public async Task Should_create_company_with_profile_and_employees_successfully()
    {
      // given
      var client = GetClient();
      var companyDtos = PrepareTestCompanyDtos();

      // when
      var httpContent = JsonConvert.SerializeObject(companyDtos[0]);
      StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
      await client.PostAsync("/companies", content);

      // then
      var allCompaniesResponse = await client.GetAsync("/companies");
      var body = await allCompaniesResponse.Content.ReadAsStringAsync();

      var returnCompanies = JsonConvert.DeserializeObject<List<CompanyDto>>(body);

      Assert.Single(returnCompanies);
      Assert.Equal(companyDtos[0].ProfileDto?.CertId, returnCompanies[0].ProfileDto?.CertId);
      Assert.Equal(companyDtos[0].ProfileDto?.RegisteredCapital,
                   returnCompanies[0].ProfileDto?.RegisteredCapital);
      Assert.Equal(companyDtos[0].EmployeeDtos?.Count, returnCompanies[0].EmployeeDtos?.Count);
      Assert.Equal(companyDtos[0].EmployeeDtos?[0].Age, returnCompanies[0].EmployeeDtos?[0].Age);
      Assert.Equal(companyDtos[0].EmployeeDtos?[0].Name,
                   returnCompanies[0].EmployeeDtos?[0].Name);
    }

    [Fact]
    public async Task Should_delete_company_and_related_employee_and_profile_successfully()
    {
      var client = GetClient();
      var companyDtos = PrepareTestCompanyDtos();

      var httpContent = JsonConvert.SerializeObject(companyDtos[0]);
      StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

      var response = await client.PostAsync("/companies", content);
      await client.DeleteAsync(response.Headers.Location);
      var allCompaniesResponse = await client.GetAsync("/companies");
      var body = await allCompaniesResponse.Content.ReadAsStringAsync();

      var returnCompanies = JsonConvert.DeserializeObject<List<CompanyDto>>(body);

      Assert.Empty(returnCompanies);
    }

    [Fact]
    public async Task Should_create_many_companies_successfully()
    {
      var client = GetClient();
      var companyDtos = PrepareTestCompanyDtos();

      var httpContent = JsonConvert.SerializeObject(companyDtos[0]);
      StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
      await client.PostAsync("/companies", content);
      var httpContent2 = JsonConvert.SerializeObject(companyDtos[1]);
      StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);
      await client.PostAsync("/companies", content2);

      var allCompaniesResponse = await client.GetAsync("/companies");
      var body = await allCompaniesResponse.Content.ReadAsStringAsync();

      var returnCompanies = JsonConvert.DeserializeObject<List<CompanyDto>>(body);

      Assert.Equal(2, returnCompanies.Count);
    }

    [Fact]
    public async Task Should_get_company_by_id_successfully()
    {
      var client = GetClient();
      var companyDtos = PrepareTestCompanyDtos();

      var httpContent = JsonConvert.SerializeObject(companyDtos[0]);
      StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
      var companyResponse = await client.PostAsync("/companies", content);

      var httpContent2 = JsonConvert.SerializeObject(companyDtos[1]);
      StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);
      await client.PostAsync("/companies", content2);

      var allCompaniesResponse = await client.GetAsync(companyResponse.Headers.Location);
      var body = await allCompaniesResponse.Content.ReadAsStringAsync();

      var returnCompany = JsonConvert.DeserializeObject<CompanyDto>(body);

      Assert.Equal("IBM", returnCompany.Name);
    }
  }
}