using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class DepartmanlarDto
    {
        public int Id { get; set; }
        public required string DepartmanAdi { get; set; }
        public Aktiflik DepartmanAktiflik { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
