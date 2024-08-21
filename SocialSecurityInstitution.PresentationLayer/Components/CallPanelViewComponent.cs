using Microsoft.AspNetCore.Mvc;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.PresentationLayer.Components
{
    public class CallPanelViewComponent : ViewComponent
    {
        private readonly ISiralarCustomService _siralarCustomService;
        private readonly IUserContextService _userContextService;

        public CallPanelViewComponent(ISiralarCustomService siralarCustomService, IUserContextService userContextService)
        {
            _siralarCustomService = siralarCustomService;
            _userContextService = userContextService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string tcKimlikNo = _userContextService.TcKimlikNo;
            
            List<siraCagirmaDto> siraListesi = await _siralarCustomService.GetSiraListeAsync(tcKimlikNo);

            return View(siraListesi);
        }
    }
}
