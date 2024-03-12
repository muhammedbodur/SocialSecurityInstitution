using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class PersonelCocuklariDto
    {
        public required string TcKimlikNo { get; set; }
        public required string CocukAdi { get; set; }
        public DateOnly CocukDogumTarihi { get; set; }
        public OgrenimDurumu OgrenimDurumu { get; set; }
    }
}
