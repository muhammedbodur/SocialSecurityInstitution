using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class BankolarDto
    {
        public int Id { get; set; }
        public int DepartmanId { get; set; }
        public required Departmanlar Departman { get; set; }
        public int BankoNo { get; set; }
        public Aktiflik BankoAktiflik { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
