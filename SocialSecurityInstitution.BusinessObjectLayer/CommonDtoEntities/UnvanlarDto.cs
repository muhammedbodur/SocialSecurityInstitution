using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class UnvanlarDto
    {
        public int Id { get; set; }
        public required string UnvanAdi { get; set; }
        public Aktiflik UnvanAktiflik { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
    }
}
