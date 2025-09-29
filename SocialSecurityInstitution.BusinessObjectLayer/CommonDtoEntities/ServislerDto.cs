using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class ServislerDto
    {
        public int ServisId { get; set; }
        public required string ServisAdi { get; set; }
        public Aktiflik ServisAktiflik { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
