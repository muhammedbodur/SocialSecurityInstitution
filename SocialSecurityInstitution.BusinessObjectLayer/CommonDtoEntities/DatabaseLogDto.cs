using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class DatabaseLogDto
    {
        public int DatabaseLogId { get; set; }
        public required string TcKimlikNo { get; set; }
        public DatabaseAction DatabaseAction { get; set; } // INSERT, UPDATE, DELETE
        public required string TableName { get; set; }
        public DateTime ActionTime { get; set; }
        public string BeforeData { get; set; } //değişiklik öncesi veri
        public string AfterData { get; set; } //değişiklik sonrası veri
    }
}
