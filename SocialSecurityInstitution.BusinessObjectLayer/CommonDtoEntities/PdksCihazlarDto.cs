using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class PdksCihazlarDto
    {
        public int Id { get; set; }
        public required Departmanlar Departman { get; set; }
        public required string CihazIP { get; set; }
        public int CihazPort { get; set; }
        public int Durum { get; set; }
        public string? Aciklama { get; set; }
        public int Aktiflik { get; set; }
        public DateTime IslemZamani { get; set; }
        public int IslemSayisi { get; set; }
        public int IslemBasari { get; set; }
        public string? IslemDurum { get; set; }
        public DateTime KontrolZamani { get; set; }
        public int KontrolSayisi { get; set; }
        public int KontrolBasari { get; set; }
        public string? KontrolDurum { get; set; }
    }
}
