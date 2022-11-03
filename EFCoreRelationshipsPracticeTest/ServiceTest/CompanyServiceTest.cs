using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationshipsPracticeTest.ServiceTest
{
    public class CompanyServiceTest: CompanyServiceTestBase
    {
        [Fact]
        public void Should_return_2_company_with_2_employee_when_get_all()
        {
            //given
            InitDataBase();

            //when
            var companies = CompanyService.GetAll().Result;
            
            //then
            Assert.Equal(2, companies.Count);
            Assert.Equal("SLB", companies[0].Name);
            Assert.Equal("SLBCert", companies[0].ProfileDto?.CertId);
            Assert.Equal(2, companies[0].EmployeeDtos?.Count);
        }

        [Fact]
        public void Should_create_company_successfully_when_give_a_company()
        {
            //given
            var companyDto = GetACompanyDto();

            //when
            var companyId = CompanyService.AddCompany(companyDto).Result;

            //then
            var companyEntity = CompanyDbContext.Companies
                .Include(_ => _.Employees)
                .Include(_ => _.Profile)
                .FirstOrDefault(_ => _.Id == companyId);

            Assert.Equal("SLB", companyEntity?.Name);
            Assert.Equal("SLBCert", companyEntity?.Profile?.CertId);
            Assert.Equal("Xu", companyEntity?.Employees?[0].Name);
        }

    }
}
