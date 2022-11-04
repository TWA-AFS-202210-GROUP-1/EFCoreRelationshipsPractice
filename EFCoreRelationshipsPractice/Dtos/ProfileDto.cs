using EFCoreRelationshipsPractice.Model;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class ProfileDto
    {

        public ProfileDto()
        {
        }

        public ProfileDto(ProfileEntity? profile)
        {
            this.RegisteredCapital = profile.RegisteredCapital;
            this.CertId = profile.CertId;
        }

        public int RegisteredCapital { get; set; }
        public string CertId { get; set; }

        public ProfileEntity ToEntity()
        {
            return new ProfileEntity()
            {
                RegisteredCapital = this.RegisteredCapital,
                CertId = this.CertId,
            };
        }
    }
}