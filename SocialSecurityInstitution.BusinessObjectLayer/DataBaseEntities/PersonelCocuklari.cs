using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class PersonelCocuklari
    {
        [Key]
        public int Id { get; set; }
        public required string TcKimlikNo { get; set; }
        public required string CocukAdi { get; set; }
        public DateOnly CocukDogumTarihi { get; set; }
        public OgrenimDurumu OgrenimDurumu { get; set; }

        [ForeignKey("TcKimlikNo")]
        public required Personeller Personel { get; set; }
    }

}
