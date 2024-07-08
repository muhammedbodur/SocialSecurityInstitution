using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class DepartmanPersonelleriDto
    {
        public string TcKimlikNo { get; set; } = string.Empty;
        public PersonellerDto personellerDto { get; set; }
        public int SicilNo { get; set; }
        public string PersonelAdSoyad { get; set; }
        public int DepartmanId { get; set; }
        public string DepartmanAdi { get; set; }
        public DepartmanlarDto departmanlarDto { get; set; }
    }
}
