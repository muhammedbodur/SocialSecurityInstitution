using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class PdksCihazlar
    {
        [Key]
        public int Id { get; set; }
        public required int DepartmanId { get; set; }
        public required Departmanlar Departman { get; set; }
        public required string CihazIP { get; set; }
        public int CihazPort { get; set; } = 4370;
        public int Durum { get; set; } = 0;
        public string? Aciklama { get; set; }
        public int Aktiflik { get; set; } = 1;
        public DateTime IslemZamani { get; set; }
        public int IslemSayisi { get; set; } = 0;
        public int IslemBasari { get; set; } = 0;
        public string? IslemDurum { get; set; }
        public DateTime KontrolZamani { get; set; }
        public int KontrolSayisi { get; set; } = 0;
        public int KontrolBasari { get; set; } = 0;
        public string? KontrolDurum { get; set; }
    }
}
