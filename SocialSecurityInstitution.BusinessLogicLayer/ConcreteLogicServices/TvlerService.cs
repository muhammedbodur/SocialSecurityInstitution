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
    public class TvlerService : ITvlerService
    {
        private readonly ITvlerDal _tvlerDal;

        public TvlerService(ITvlerDal tvlerDal)
        {
            _tvlerDal = tvlerDal;
        }

        public async Task<bool> TContainsAsync(TvlerDto dto)
        {
            return await _tvlerDal.ContainsAsync(dto);
        }

        public async Task<int> TCountAsync()
        {
            return await _tvlerDal.CountAsync();
        }

        public async Task<bool> TDeleteAsync(TvlerDto dto)
        {
            return await _tvlerDal.DeleteAsync(dto);
        }

        public async Task<List<TvlerDto>> TGetAllAsync()
        {
            return await _tvlerDal.GetAllAsync();
        }

        public async Task<TvlerDto> TGetByIdAsync(int id)
        {
            return await _tvlerDal.GetByIdAsync(id);
        }

        public async Task<InsertResult> TInsertAsync(TvlerDto dto)
        {
            return await _tvlerDal.InsertAsync(dto);
        }

        public async Task<bool> TUpdateAsync(TvlerDto dto)
        {
            return await _tvlerDal.UpdateAsync(dto);
        }
    }
}
