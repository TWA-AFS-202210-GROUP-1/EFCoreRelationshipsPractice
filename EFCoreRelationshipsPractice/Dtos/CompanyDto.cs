using EFCoreRelationshipsPractice.Models;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class CompanyDto
    {
        public CompanyDto()
        {
        }

        public CompanyDto(CompanyEntity companyEntity)
        {
            this.Name = companyEntity.Name;
        }

        public string Name { get; set; }

        public ProfileDto? Profile { get; set; }

        public List<EmployeeDto>? Employees { get; set; }

    }
}