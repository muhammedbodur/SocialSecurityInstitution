using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer;
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
    public class HizmetBinalariService : IHizmetBinalariService
    {
        private readonly IHizmetBinalariDal _hizmetBinalariDal;

        public HizmetBinalariService(IHizmetBinalariDal hizmetBinalariDal)
        {
            _hizmetBinalariDal = hizmetBinalariDal;
        }

        public async Task<bool> TContainsAsync(HizmetBinalariDto dto)
        {
            return await _hizmetBinalariDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _hizmetBinalariDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(HizmetBinalariDto dto)
        {
            return await _hizmetBinalariDal.DeleteAsync(dto);
        }

        public async Task<List<HizmetBinalariDto>> TGetAllAsync()
        {
            return await _hizmetBinalariDal.GetAllAsync();
        }

        public async Task<HizmetBinalariDto> TGetByIdAsync(int id)
        {
            return await _hizmetBinalariDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(HizmetBinalariDto dto)
        {
            return await _hizmetBinalariDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(HizmetBinalariDto dto)
        {
            return await _hizmetBinalariDal.UpdateAsync(dto);
        }
    }
}
