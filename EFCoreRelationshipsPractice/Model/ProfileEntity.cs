using System.Collections.Generic;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class ProfileEntity
    {
        public ProfileEntity()
        {
        }

        public int Id { get; set; }

        public int RegisteredCapital { get; set; }

        public string CertId { get; set; }
    }
}