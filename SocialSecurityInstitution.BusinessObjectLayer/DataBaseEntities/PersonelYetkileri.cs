using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities
{
    public class PersonelYetkileri
    {
        [Key]
        public int PersonelYetkiId { get; set; }
        public string TcKimlikNo { get; set; }
        [ForeignKey("TcKimlikNo")]
        public required Personeller Personeller { get; set; }
        public required ModulControllerIslemler Yetki { get; set; }
        public YetkiTipleri YetkiTipleri { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
    }
}
