using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class PersonelYetkileriDto
    {
        public int Id { get; set; }
        public required string TcKimlikNo { get; set; }
        public required Yetkiler Yetki { get; set; }
        public required YetkiTipleri YetkiTipi { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
