using EFCoreRelationshipsPractice.Model;
using System.Collections.Generic;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class CompanyDto
    {
        public CompanyDto(CompanyEntity companyEntity)
        {
            Name = companyEntity.Name;
        }

        public CompanyDto()
        {
        }

        public string Name { get; set; }

        public ProfileDto? Profile { get; set; }

        public List<EmployeeDto>? Employees { get; set; }

        public CompanyEntity ToCompanyEntity()
        {
            return new CompanyEntity()
            {
                Name = Name,
            };
            //throw new NotImplementedException();
        }
    }
}