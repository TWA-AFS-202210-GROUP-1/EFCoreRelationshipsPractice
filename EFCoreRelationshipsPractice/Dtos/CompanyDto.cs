using EFCoreRelationshipsPractice.Model;

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
      EmployeeDtos = companyEntity.Employees?
        .Select(employeeEntity => new EmployeeDto(employeeEntity))
        .ToList();
    }

    public string? Name { get; set; }

    public ProfileDto? ProfileDto { get; set; }

    public List<EmployeeDto>? EmployeeDtos { get; set; }

    public CompanyEntity ToEntity()
    {
      return new CompanyEntity()
      {
        Name = Name,
        Profile = ProfileDto?.ToEntity(),
        Employees = EmployeeDtos?.Select(employeeDto => employeeDto.ToEntity()).ToList(),
      };
    }
  }
}