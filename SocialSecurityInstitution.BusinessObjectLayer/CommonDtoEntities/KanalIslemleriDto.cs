using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KanalIslemleriDto
    {
        public int Id { get; set; }
        public required string KanalIslemAdi { get; set; }
        public int BaslangicNumara { get; set; }
        public int BitisNumara { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }

        public bool FarkKosulunuKontrolEt()
        {
            return Math.Abs(BitisNumara - BaslangicNumara) == 500;
        }
    }
}
