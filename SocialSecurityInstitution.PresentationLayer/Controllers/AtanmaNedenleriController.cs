using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class AtanmaNedenleriController : Controller
    {
        private readonly IAtanmaNedenleriService _atanmaNedenleriService;
        private readonly IMapper _mapper;

        public AtanmaNedenleriController(IAtanmaNedenleriService atanmaNedenleriService, IMapper mapper)
        {
            _atanmaNedenleriService = atanmaNedenleriService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _atanmaNedenleriService.TGetAllAsync();
            var dtoList = _mapper.Map<List<KanallarDto>>(entities);
            return Ok(dtoList);
        }
    }
}
