using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.PresentationLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.PresentationLayer.Controllers
{
    [Authorize]
    public class PersonelController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPersonelCustomService _personelCustomService;
        private readonly IPersonellerService _personellerService;
        private readonly IPersonelCocuklariService _personelCocuklariService;
        private readonly IDepartmanlarService _departmanlarService;
        private readonly IServislerService _servislerService;
        private readonly IUnvanlarService _unvanlarService;
        private readonly IAtanmaNedenleriService _atanmaNedenleriService;
        private readonly IHizmetBinalariService _hizmetBinalariService;
        private readonly IIllerService _illerService;
        private readonly IIlcelerService _ilcelerService;
        private readonly ISendikalarService _sendikalarService;
        private readonly IToastNotification _toast;

        public PersonelController(IMapper mapper, IPersonelCustomService personelCustomService, IPersonellerService personellerService, IPersonelCocuklariService personelCocuklariService, IDepartmanlarService departmanlarService, IServislerService servislerService, IUnvanlarService unvanlarService, IAtanmaNedenleriService atanmaNedenleriService, IHizmetBinalariService hizmetBinalariService, IIllerService illerService, IIlcelerService ilcelerService, ISendikalarService sendikalarService, IToastNotification toast)
        {
            _mapper = mapper;
            _personelCustomService = personelCustomService;
            _personellerService = personellerService;
            _personelCocuklariService = personelCocuklariService;
            _departmanlarService = departmanlarService;
            _servislerService = servislerService;
            _unvanlarService = unvanlarService;
            _atanmaNedenleriService = atanmaNedenleriService;
            _hizmetBinalariService = hizmetBinalariService;
            _illerService = illerService;
            _ilcelerService = ilcelerService;
            _sendikalarService = sendikalarService;
            _toast = toast;
        }

        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Kaydet(PersonellerDto personellerDto)
        {
            personellerDto.PassWord = personellerDto.TcKimlikNo;

            var insertResult = await _personellerService.TInsertAsync(personellerDto);

            if (insertResult.IsSuccess)
            {
                _toast.AddSuccessToastMessage("Personel Ekleme İşlemi Başarılı");
                return View();
            }
            else
            {
                TempData["ErrorMessage"] = "Personel eklenirken bir hata oluştu.";
                _toast.AddErrorToastMessage("Personel eklenirken bir hata oluştu.");
                return View("ErrorView");
            }
        }



        [HttpGet]
        public async Task<IActionResult> Duzenle(string TcKimlikNo)
        {
            if (!string.IsNullOrEmpty(TcKimlikNo))
            {
                var personelBilgisi = await _personelCustomService.TGetByTcKimlikNoAsync(TcKimlikNo);

                if (personelBilgisi != null)
                {
                    var viewModel = _mapper.Map<PersonellerViewDto>(personelBilgisi);
                    viewModel.DuzenlenmeTarihi = DateTime.Now;

                    viewModel.Departmanlar = await _departmanlarService.TGetAllAsync();
                    viewModel.Servisler = await _servislerService.TGetAllAsync();
                    viewModel.Unvanlar = await _unvanlarService.TGetAllAsync();
                    //viewModel.AtanmaNedenleri = await _atanmaNedenleriService.TGetAllAsync();
                    viewModel.HizmetBinalari = await _hizmetBinalariService.TGetAllAsync();
                    viewModel.Iller = await _illerService.TGetAllAsync();
                    viewModel.Ilceler = await _ilcelerService.TGetAllAsync();
                    viewModel.Sendikalar = await _sendikalarService.TGetAllAsync();
                    return View(viewModel);
                }
            }

            TempData["ErrorMessage"] = "Geçersiz TcKimlikNo parametresi.";
            return RedirectToAction("Listele", "Personel");
        }

        [HttpPost]
        public async Task<IActionResult> Guncelle(string TcKimlikNo)
        {
            if (!string.IsNullOrEmpty(TcKimlikNo))
            {
                var personelBilgisiDto = await _personelCustomService.TGetByTcKimlikNoAsync(TcKimlikNo);

                if (personelBilgisiDto != null)
                {
                    var updateResult = _personellerService.TUpdateAsync(personelBilgisiDto);
                    if (updateResult != null)
                    {
                        _toast.AddSuccessToastMessage("Personel Güncelleme İşlemi Başarılı");
                        return View(updateResult);
                    }
                    else
                    {
                        _toast.AddErrorToastMessage("Personel Güncelleme İşlemi Başarısız!");
                        return View(updateResult);
                    }
                }
            }

            _toast.AddErrorToastMessage("Personel Bilgisi Bulunamadı!");
            return RedirectToAction("Listele", "Personel");
        }

        [HttpGet]
        public async Task<IActionResult> Listele()
        {
            List<PersonelRequestDto> personelRequests = await _personelCustomService.GetPersonellerWithDetailsAsync();
            
            return View("Listele", personelRequests);
        }
    }
}
