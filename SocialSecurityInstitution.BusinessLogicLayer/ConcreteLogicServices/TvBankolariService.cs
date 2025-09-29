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
    public class TvBankolariService : ITvBankolariService
    {
        private readonly ITvBankolariDal _tvBankolariDal;

        public TvBankolariService(ITvBankolariDal tvBankolariDal)
        {
            _tvBankolariDal = tvBankolariDal;
        }

        public async Task<bool> TContainsAsync(TvBankolariDto dto)
        {
            return await _tvBankolariDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _tvBankolariDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(TvBankolariDto dto)
        {
            return await _tvBankolariDal.DeleteAsync(dto);
        }

        public async Task<List<TvBankolariDto>> TGetAllAsync()
        {
            return await _tvBankolariDal.GetAllAsync();
        }

        public async Task<TvBankolariDto> TGetByIdAsync(int id)
        {
            return await _tvBankolariDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(TvBankolariDto dto)
        {
            return await _tvBankolariDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(TvBankolariDto dto)
        {
            return await _tvBankolariDal.UpdateAsync(dto);
        }
    }
}
