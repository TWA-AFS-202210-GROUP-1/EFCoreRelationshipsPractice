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
        }

        public CompanyDto()
        {
        }

        public string Name { get; set; }

        public ProfileDto? ProfileDTO { get; set; }

        public List<EmployeeDto>? Employees { get; set; }

        public CompanyEntity ToCompanyEntity()
        {
            return new CompanyEntity()
            {
                Name = Name,
                Profile = ProfileDTO?.ToEntity(),
                //Profile = this.ProfileDTO != null ? ProfileDTO.ToEntity(), null

                //Profile = new ProfileEntity()
                //{
                //    RegisteredCapital = Profile.RegisteredCapital,
                //    CertId = Profile.CertId,
                //},
            };
            //throw new NotImplementedException();
        }
    }
}