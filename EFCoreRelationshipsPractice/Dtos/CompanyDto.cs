using EFCoreRelationshipsPractice.Model;
using System.Collections.Generic;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class CompanyDto
    {
        public CompanyDto(CompanyEntity companyEntity)
        {
            Name = companyEntity.Name;
            ProfileDTO = companyEntity.Profile != null ? new ProfileDto(companyEntity.Profile) : null;
            EmployeesDTO = companyEntity.EmployeeList.Select(employeeEntity => new EmployeeDto(employeeEntity)).ToList();

        }

        public CompanyDto()
        {
        }

        public string Name { get; set; }

        public ProfileDto? ProfileDTO { get; set; }

        public List<EmployeeDto>? EmployeesDTO { get; set; }

        public CompanyEntity ToCompanyEntity()
        {
            return new CompanyEntity()
            {
                Name = Name,
                Profile = ProfileDTO?.ToEntity(),
                EmployeeList = EmployeesDTO?.Select(employeeDto => employeeDto.ToEntity()).ToList(),
            };
        }
    }
}