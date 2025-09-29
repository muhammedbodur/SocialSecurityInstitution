using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class YetkilerDal : GenericRepository<Yetkiler, YetkilerDto>, IYetkilerDal
    {
        public YetkilerDal(Context context, IMapper mapper, ILogService logService) : base(context, mapper, logService)
        {
        }

        public async Task<List<YetkilerWithPersonelDto>> GetYetkilerByPersonelTcKimlikNoAsync(string tcKimlikNo)
        {
            var yetkilerWithPersonel = await _context.Yetkiler
                .Include(y => y.PersonelYetkileri)
                .Select(y => new YetkilerWithPersonelDto
                {
                    YetkiId = y.YetkiId,
                    YetkiAdi = y.YetkiAdi,
                    YetkiTuru = y.YetkiTuru,
                    Aciklama = y.Aciklama,
                    UstYetkiId = y.UstYetkiId,
                    ControllerAdi = y.ControllerAdi,
                    ActionAdi = y.ActionAdi,
                    EklenmeTarihi = y.EklenmeTarihi,
                    DuzenlenmeTarihi = y.DuzenlenmeTarihi,
                    PersonelYetkileri = y.PersonelYetkileri
                        .Where(py => py.TcKimlikNo == tcKimlikNo)
                        .Select(py => new PersonelYetkileriDto
                        {
                            YetkiId = py.YetkiId,
                            YetkiTipleri = py.YetkiTipleri,
                            TcKimlikNo = py.TcKimlikNo
                        }).ToList()
                })
                .ToListAsync();

            // Build hierarchical structure
            var anaYetkiler = yetkilerWithPersonel
                .Where(y => y.YetkiTuru == YetkiTurleri.AnaYetki)
                .ToList();

            foreach (var ana in anaYetkiler)
            {
                ana.OrtaYetkiler = yetkilerWithPersonel
                    .Where(y => y.UstYetkiId == ana.YetkiId && y.YetkiTuru == YetkiTurleri.OrtaYetki)
                    .ToList();

                foreach (var orta in ana.OrtaYetkiler)
                {
                    orta.AltYetkiler = yetkilerWithPersonel
                        .Where(y => y.UstYetkiId == orta.YetkiId && y.YetkiTuru == YetkiTurleri.AltYetki)
                        .ToList();
                }
            }

            return anaYetkiler;
        }

        public async Task<List<YetkilerDto>> GetOrtaYetkilerByAnaYetkiIdAsync(int anaYetkiId)
        {
            var ortaYetkiler = await _context.Yetkiler
                .Where(y => y.UstYetkiId == anaYetkiId && y.YetkiTuru == YetkiTurleri.OrtaYetki)
                .ToListAsync();

            return _mapper.Map<List<YetkilerDto>>(ortaYetkiler);
        }

        public async Task<List<YetkilerDto>> GetAltYetkilerByOrtaYetkiIdAsync(int ortaYetkiId)
        {
            var altYetkiler = await _context.Yetkiler
                .Where(y => y.UstYetkiId == ortaYetkiId && y.YetkiTuru == YetkiTurleri.AltYetki)
                .ToListAsync();

            return _mapper.Map<List<YetkilerDto>>(altYetkiler);
        }

        public async Task<List<YetkilerDto>> GetAllYetkilerWithIncludesAsync()
        {
            // Ana Yetkiler'i çekiyoruz
            var anaYetkiler = await _context.Yetkiler
                .Where(y => y.YetkiTuru == YetkiTurleri.AnaYetki)
                .OrderBy(y => y.YetkiAdi)
                .ToListAsync();

            // Hiyerarşi oluşturma
            var sortedAnaYetkiler = new List<YetkilerDto>();

            foreach (var ana in anaYetkiler)
            {
                // Ana yetki DTO'sunu oluştur
                var anaYetkiDto = _mapper.Map<YetkilerDto>(ana);

                // Orta Yetkiler'i Ana Yetki'ye bağlı olarak alıyoruz
                var ortaYetkiler = await GetOrtaYetkilerByAnaYetkiIdAsync(ana.YetkiId);

                // Orta Yetkilerin her birine bağlı Alt Yetkiler'i çekiyoruz
                foreach (var orta in ortaYetkiler)
                {
                    var altYetkiler = await GetAltYetkilerByOrtaYetkiIdAsync(orta.YetkiId);
                    orta.AltYetkiler = altYetkiler;
                }

                // Ana yetki DTO'suna orta yetkileri ekle
                anaYetkiDto.OrtaYetkiler = ortaYetkiler;
                sortedAnaYetkiler.Add(anaYetkiDto);
            }

            return sortedAnaYetkiler;
        }

        public async Task<List<PersonelYetkileriDto>> GetPersonelYetkileriAsync(string tcKimlikNo)
        {
            var personelYetkileri = await _context.PersonelYetkileri
                .Where(py => py.TcKimlikNo == tcKimlikNo)
                .ToListAsync();

            return _mapper.Map<List<PersonelYetkileriDto>>(personelYetkileri);
        }
    }
}
