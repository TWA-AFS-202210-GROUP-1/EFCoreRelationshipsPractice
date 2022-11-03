using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreRelationshipsPractice.Model
{
    public class ProfileEntity
    {
        public ProfileEntity() { }

        public int Id { get; set; }

        public int RegisterCapital { get; set; }

        public string CertId { get; set; }

    }
}
