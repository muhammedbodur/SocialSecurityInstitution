using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class PersonelYetkileriDto
    {
        public int PersonelYetkiId { get; set; }

        public string TcKimlikNo { get; set; }

        public int YetkiId { get; set; }
        public string YetkiAdi { get; set; }

        public YetkiTipleri YetkiTipleri { get; set; }

        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

    }
}

