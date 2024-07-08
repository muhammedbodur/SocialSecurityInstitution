using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class PersonellerAltKanallarRequestDto
    {
        public string TcKimlikNo { get; set; }
        public string AdSoyad { get; set; }
        public int UzmanSayisi { get; set; }
        public int UzmanYrdSayisi { get; set; }
    }
}
