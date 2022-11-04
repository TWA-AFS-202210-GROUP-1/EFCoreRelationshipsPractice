﻿namespace EFCoreRelationshipsPractice.Dtos
{
    public class ProfileDto
    {
        public ProfileDto()
        {
        }

        public ProfileDto(ProfileEntity? profileEntity)
        {
            RegisteredCapital = profileEntity.RegisteredCapital;
            CertId = profileEntity.CertId;
        }

        public int RegisteredCapital { get; set; }

        public string CertId { get; set; }

        public ProfileEntity ToEntity()
        {
            return new ProfileEntity()
            {
                RegisteredCapital = RegisteredCapital,
                CertId = CertId,
            };
        }
    }
}