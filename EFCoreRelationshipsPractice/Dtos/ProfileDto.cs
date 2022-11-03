using EFCoreRelationshipsPractice.Model;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class ProfileDto
    {
        public ProfileDto()
        {
        }

        public ProfileDto(ProfileEntity profileEntity)
        {
            RegisteredCapital = profileEntity.RegisterCapital;
            CertId = profileEntity.CertId;
        }

        public int RegisteredCapital { get; set; }

        public string CertId { get; set; }

        public ProfileEntity? ToEntity()
        {
            return new ProfileEntity()
            {
                RegisterCapital = RegisteredCapital,
                CertId = CertId,
            };
        }
    }
}