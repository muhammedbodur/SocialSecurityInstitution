using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class KanalIslemleriDal : GenericRepository<KanalIslemleri, KanalIslemleriDto>, IKanalIslemleriDal
    {
        public KanalIslemleriDal(Context context, IMapper mapper, ILogService logService) : base(context, mapper, logService)
        {
        }

        public async Task<List<KanalIslemleriRequestDto>> GetKanalIslemleriByHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            var kanalIslemleri = await _context.KanalIslemleri
                .Where(ki => ki.HizmetBinasiId == hizmetBinasiId)
                .Include(ki => ki.Kanallar)
                .Include(ki => ki.HizmetBinalari)
                    .ThenInclude(hb => hb.Departman)
                .ToListAsync();

            var requestDtos = kanalIslemleri.Select(ki => new KanalIslemleriRequestDto
            {
                KanalIslemId = ki.KanalIslemId,
                KanalIslemAdi = ki.Kanallar.KanalAdi,
                HizmetBinasiId = ki.HizmetBinalari.HizmetBinasiId,
                HizmetBinasiAdi = ki.HizmetBinalari.HizmetBinasiAdi,
                DepartmanId = ki.HizmetBinalari.Departman.DepartmanId,
                DepartmanAdi = ki.HizmetBinalari.Departman.DepartmanAdi,
                BaslangicNumara = ki.BaslangicNumara,
                BitisNumara = ki.BitisNumara,
                KanalIslemAktiflik = ki.KanalIslemAktiflik,
                EklenmeTarihi = ki.EklenmeTarihi,
                DuzenlenmeTarihi = ki.DuzenlenmeTarihi
            }).ToList();

            return requestDtos;
        }

        public async Task<KanalIslemleriRequestDto> GetKanalIslemleriByIdWithDetailsAsync(int kanalIslemId)
        {
            var kanalIslemleri = await _context.KanalIslemleri
                .Where(ki => ki.KanalIslemId == kanalIslemId)
                .Include(ki => ki.Kanallar)
                .Include(ki => ki.HizmetBinalari)
                    .ThenInclude(hb => hb.Departman)
                .AsNoTracking().FirstOrDefaultAsync();

            if (kanalIslemleri == null)
            {
                return null;
            }

            var requestDto = new KanalIslemleriRequestDto
            {
                KanalIslemId = kanalIslemleri.KanalIslemId,
                KanalIslemAdi = kanalIslemleri.Kanallar.KanalAdi,
                HizmetBinasiId = kanalIslemleri.HizmetBinalari.HizmetBinasiId,
                HizmetBinasiAdi = kanalIslemleri.HizmetBinalari.HizmetBinasiAdi,
                DepartmanId = kanalIslemleri.HizmetBinalari.Departman.DepartmanId,
                DepartmanAdi = kanalIslemleri.HizmetBinalari.Departman.DepartmanAdi,
                BaslangicNumara = kanalIslemleri.BaslangicNumara,
                BitisNumara = kanalIslemleri.BitisNumara,
                KanalIslemAktiflik = kanalIslemleri.KanalIslemAktiflik,
                EklenmeTarihi = kanalIslemleri.EklenmeTarihi,
                DuzenlenmeTarihi = kanalIslemleri.DuzenlenmeTarihi
            };

            return requestDto;
        }
    }
}
