namespace EFCoreRelationshipsPracticeTest.ControllerTest
{
    using System.Net.Mime;
    using System.Text;
    using EFCoreRelationshipsPractice.Dtos;
    using Newtonsoft.Json;

    [Collection("CompanyCollection")]
    public class CompanyControllerTest : TestBase
    {
        public CompanyControllerTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_company_success()
        {
            // given
            var client = GetClient();
            CompanyDto companyDto = new CompanyDto
            {
                Name = "IBM",
                //ProfileDTO = new ProfileDto()
                //{
                //    RegisteredCapital = 100010,
                //    CertId = "100",
                //},
            };
            //CompanyDto companyDto = new CompanyDto(name);
            //{
            //    Name = "IBM",
            //};

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
        public async Task Should_create_company_with_profile_success()
        {
            // given
            var client = GetClient();
            CompanyDto companyDto = new CompanyDto
            {
                Name = "IBM",
                ProfileDTO = new ProfileDto()
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
            Assert.Equal(companyDto.ProfileDTO.CertId, returnCompanies[0].ProfileDTO.CertId);
            Assert.Equal(companyDto.ProfileDTO.RegisteredCapital, returnCompanies[0].ProfileDTO.RegisteredCapital);
        }

        [Fact]
        public async Task Should_create_company_with_profile_and_employees_success()
        {
            // given
            var client = GetClient();
            CompanyDto companyDto = new CompanyDto
            {
                Name = "IBM",
                EmployeesDTO = new List<EmployeeDto>()
                {
                    new EmployeeDto()
                    {
                        Name = "Tom",
                        Age = 19,
                    },
                },
                ProfileDTO = new ProfileDto()
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
            Assert.Equal(companyDto.ProfileDTO.CertId, returnCompanies[0].ProfileDTO.CertId);
            Assert.Equal(companyDto.ProfileDTO.RegisteredCapital, returnCompanies[0].ProfileDTO.RegisteredCapital);
            Assert.Equal(companyDto.EmployeesDTO.Count, returnCompanies[0].EmployeesDTO.Count);
            Assert.Equal(companyDto.EmployeesDTO[0].Age, returnCompanies[0].EmployeesDTO[0].Age);
            Assert.Equal(companyDto.EmployeesDTO[0].Name, returnCompanies[0].EmployeesDTO[0].Name);
        }

        [Fact]
        public async Task Should_delete_company_and_related_employee_and_profile_success()
        {
            var client = GetClient();
            CompanyDto companyDto = new CompanyDto
            {
                Name = "IBM",
                EmployeesDTO = new List<EmployeeDto>()
                {
                    new EmployeeDto()
                    {
                        Name = "Tom",
                        Age = 19,
                    },
                },
                ProfileDTO = new ProfileDto()
                {
                    RegisteredCapital = 100010,
                    CertId = "100"
                },
            };

            var httpContent = JsonConvert.SerializeObject(companyDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await client.PostAsync("/companies", content);
            await client.DeleteAsync(response.Headers.Location);
            var allCompaniesResponse = await client.GetAsync("/companies");
            var body = await allCompaniesResponse.Content.ReadAsStringAsync();

            var returnCompanies = JsonConvert.DeserializeObject<List<CompanyDto>>(body);

            Assert.Empty(returnCompanies);
        }

        [Fact]
        public async Task Should_create_many_companies_success()
        {
            var client = GetClient();
            CompanyDto companyDto = new CompanyDto
            {
                Name = "IBM",
                EmployeesDTO = new List<EmployeeDto>()
                {
                    new EmployeeDto()
                    {
                        Name = "Tom",
                        Age = 19,
                    },
                },
                ProfileDTO = new ProfileDto()
                {
                    RegisteredCapital = 100010,
                    CertId = "100",
                },
            };

            CompanyDto companyDto2 = new CompanyDto
            {
                Name = "MS",
                EmployeesDTO = new List<EmployeeDto>()
                {
                    new EmployeeDto()
                    {
                        Name = "Jerry",
                        Age = 18,
                    },
                },
                ProfileDTO = new ProfileDto()
                {
                    RegisteredCapital = 100020,
                    CertId = "101",
                },
            };

            var httpContent = JsonConvert.SerializeObject(companyDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/companies", content);
            var httpContent2 = JsonConvert.SerializeObject(companyDto2);
            StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/companies", content2);

            var allCompaniesResponse = await client.GetAsync("/companies");
            var body = await allCompaniesResponse.Content.ReadAsStringAsync();

            var returnCompanies = JsonConvert.DeserializeObject<List<CompanyDto>>(body);

            Assert.Equal(2, returnCompanies.Count);
        }

        [Fact]
        public async Task Should_get_company_by_id_success()
        {
            var client = GetClient();
            CompanyDto companyDto = new CompanyDto
            {
                Name = "IBM",
                EmployeesDTO = new List<EmployeeDto>()
                {
                    new EmployeeDto()
                    {
                        Name = "Tom",
                        Age = 19,
                    },
                },
                ProfileDTO = new ProfileDto()
                {
                    RegisteredCapital = 100010,
                    CertId = "100",
                },
            };

            CompanyDto companyDto2 = new CompanyDto
            {
                Name = "MS",
                EmployeesDTO = new List<EmployeeDto>()
                {
                    new EmployeeDto()
                    {
                        Name = "Jerry",
                        Age = 18,
                    },
                },
                ProfileDTO = new ProfileDto()
                {
                    RegisteredCapital = 100020,
                    CertId = "101",
                },
            };

            var httpContent = JsonConvert.SerializeObject(companyDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var companyResponse =await client.PostAsync("/companies", content);

            var httpContent2 = JsonConvert.SerializeObject(companyDto2);
            StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/companies", content2);

            var allCompaniesResponse = await client.GetAsync(companyResponse.Headers.Location);
            var body = await allCompaniesResponse.Content.ReadAsStringAsync();

            var returnCompany = JsonConvert.DeserializeObject<CompanyDto>(body);

            Assert.Equal("IBM", returnCompany.Name);
        }
    }
}