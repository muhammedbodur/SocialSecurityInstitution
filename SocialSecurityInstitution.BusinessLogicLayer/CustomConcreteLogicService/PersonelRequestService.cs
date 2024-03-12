using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class PersonelRequestService : IPersonelRequestService
    {
        public async Task<List<PersonelRequestDto>> GetPersonellerWithDetailsAsync()
        {
            using var context = new Context();
            var personeller = await context.Personeller
            .Include(p => p.Departman)
            .Include(p => p.Servis)
            .Include(p => p.Unvan)
            .Include(p => p.Sendika)
            .Include(p => p.Il)
            .Include(p => p.Ilce)
            .Include(p => p.EsininIsIl)
            .Include(p => p.EsininIsIlce)
            .ToListAsync();

            var personelDtoList = personeller.Select(p => new PersonelRequestDto
            {
                TcKimlikNo = p.TcKimlikNo,
                AdSoyad = p.AdSoyad,
                SicilNo = p.SicilNo,
                DepartmanId = p.DepartmanId,
                DepartmanAdi = p.Departman?.DepartmanAdi,
                ServisId = p.ServisId,
                ServisAdi = p.Servis?.ServisAdi,
                UnvanId = p.UnvanId,
                UnvanAdi = p.Unvan?.UnvanAdi,
                Gorev = p.Gorev,
                Uzmanlik = p.Uzmanlik,
                AtanmaNedeni = p.AtanmaNedeni?.AtanmaNedeni,
                SendikaAdi = p.Sendika?.SendikaAdi,
                IlAdi = p.Il?.IlAdi,
                IlceAdi = p.Ilce?.IlceAdi,
                EsininIsIlAdi = p.EsininIsIl?.IlAdi,
                EsininIsIlceAdi = p.EsininIsIlce?.IlceAdi
            }).ToList();

            return personelDtoList;
        }
    }
}
