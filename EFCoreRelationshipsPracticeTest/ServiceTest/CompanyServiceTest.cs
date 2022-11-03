namespace EFCoreRelationshipsPracticeTest.ServiceTest
{
    public class CompanyServiceTest: CompanyServiceTestBase
    {
        [Fact]
        public async void Should_return_2_company_with_2_employee_when_get_all()
        {
            var companies = await CompanyService.GetAll();
            Assert.Equal(2, companies.Count);
            Assert.Equal("SLB", companies[0].Name);
            Assert.Equal("SLBCert", companies[0].ProfileDto?.CertId);
            Assert.Equal(2, companies[0].EmployeeDtos?.Count);
        }
        
    }
}
