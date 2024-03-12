using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;

namespace SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices
{
    public class AtanmaNedenleriService : IAtanmaNedenleriService, IGenericService<AtanmaNedenleriDto>
    {
        private readonly IAtanmaNedenleriDal _atanmaNedenleriDal;
        private readonly IMapper _mapper;

        public AtanmaNedenleriService(IAtanmaNedenleriDal atanmaNedenleriDal, IMapper mapper)
        {
            _atanmaNedenleriDal = atanmaNedenleriDal;
            _mapper = mapper;
        }

        public async Task TInsertAsync(AtanmaNedenleriDto entity)
        {
            var atanmaNedenleri = _mapper.Map<AtanmaNedenleri>(entity);
            await _atanmaNedenleriDal.InsertAsync(atanmaNedenleri);
        }

        public async Task TUpdateAsync(AtanmaNedenleriDto entity)
        {
            var atanmaNedenleri = _mapper.Map<AtanmaNedenleri>(entity);
            await _atanmaNedenleriDal.UpdateAsync(atanmaNedenleri);
        }

        public async Task TDeleteAsync(AtanmaNedenleriDto entity)
        {
            var atanmaNedenleri = _mapper.Map<AtanmaNedenleri>(entity);
            await _atanmaNedenleriDal.DeleteAsync(atanmaNedenleri);
        }

        public async Task<AtanmaNedenleriDto> TGetByIdAsync(int id)
        {
            var atanmaNedenleri = await _atanmaNedenleriDal.GetByIdAsync(id);
            return _mapper.Map<AtanmaNedenleriDto>(atanmaNedenleri);
        }

        public Task<bool> TContainsAsync(AtanmaNedenleriDto entity)
        {
            var atanmaNedenleri = _mapper.Map<AtanmaNedenleri>(entity);
            return _atanmaNedenleriDal.ContainsAsync(atanmaNedenleri);
        }

        public Task<int> TCountAsync()
        {
            return _atanmaNedenleriDal.CountAsync();
        }

        public async Task<List<AtanmaNedenleriDto>> TGetAllAsync()
        {
            var atanmaNedenleriList = await _atanmaNedenleriDal.GetAllAsync();
            return _mapper.Map<List<AtanmaNedenleriDto>>(atanmaNedenleriList);
        }
    }
}
