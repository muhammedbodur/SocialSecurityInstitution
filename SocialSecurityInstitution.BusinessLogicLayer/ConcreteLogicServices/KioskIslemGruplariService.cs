using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices
{
    public class KioskIslemGruplariService : IKioskIslemGruplariService
    {
        private readonly IKioskIslemGruplariDal _kioskIslemGruplariDal;

        public KioskIslemGruplariService(IKioskIslemGruplariDal kioskIslemGruplariDal)
        {
            _kioskIslemGruplariDal = kioskIslemGruplariDal;
        }

        public async Task<bool> TContainsAsync(KioskIslemGruplariDto dto)
        {
            return await _kioskIslemGruplariDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _kioskIslemGruplariDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(KioskIslemGruplariDto dto)
        {
            return await _kioskIslemGruplariDal.DeleteAsync(dto);
        }

        public async Task<List<KioskIslemGruplariDto>> TGetAllAsync()
        {
            return await _kioskIslemGruplariDal.GetAllAsync();
        }

        public async Task<KioskIslemGruplariDto> TGetByIdAsync(int id)
        {
            return await _kioskIslemGruplariDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(KioskIslemGruplariDto dto)
        {
            return await _kioskIslemGruplariDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(KioskIslemGruplariDto dto)
        {
            return await _kioskIslemGruplariDal.UpdateAsync(dto);
        }
    }
}
