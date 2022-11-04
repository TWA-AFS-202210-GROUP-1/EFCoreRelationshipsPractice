using EFCoreRelationshipsPractice.Model;
using System.Collections.Generic;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class CompanyDto
    {
        public CompanyDto()
        {
        }

        public CompanyDto(CompanyEntity companyEntity)
        {
            Name = companyEntity.Name;
            ProfileDto = companyEntity.Profile != null ? new ProfileDto(companyEntity.Profile) : null;
            EmployeesDto = companyEntity.Employees?.Select(employeeEntity => new EmployeeDto(employeeEntity)).ToList();
        }

        public string Name { get; set; }

        public ProfileDto? ProfileDto { get; set; }

        public List<EmployeeDto>? EmployeesDto { get; set; }

        public CompanyEntity ToEntity()
        {
            return new CompanyEntity()
            {
                Name = this.Name,
                Profile = this.ProfileDto?.ToEntity(),
                Employees = this.EmployeesDto?.Select(employeeDto => employeeDto.ToEntity()).ToList(),
            };
        }
    }
}