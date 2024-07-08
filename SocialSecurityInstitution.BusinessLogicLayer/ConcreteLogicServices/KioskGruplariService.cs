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
    public class KioskGruplariService : IKioskGruplariService
    {
        private readonly IKioskGruplariDal _kioskGruplariDal;

        public KioskGruplariService(IKioskGruplariDal kioskGruplariDal)
        {
            _kioskGruplariDal = kioskGruplariDal;
        }

        public async Task<bool> TContainsAsync(KioskGruplariDto dto)
        {
            return await _kioskGruplariDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _kioskGruplariDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(KioskGruplariDto dto)
        {
            return await _kioskGruplariDal.DeleteAsync(dto);
        }

        public async Task<List<KioskGruplariDto>> TGetAllAsync()
        {
            return await _kioskGruplariDal.GetAllAsync();
        }

        public async Task<KioskGruplariDto> TGetByIdAsync(int id)
        {
            return await _kioskGruplariDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(KioskGruplariDto dto)
        {
            return await _kioskGruplariDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(KioskGruplariDto dto)
        {
            return await _kioskGruplariDal.UpdateAsync(dto);
        }
    }
}
