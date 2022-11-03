using System.Collections.Generic;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class CompanyEntity
    {
        public CompanyEntity()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ProfileEntity? Profile { get; set; }

    }
}