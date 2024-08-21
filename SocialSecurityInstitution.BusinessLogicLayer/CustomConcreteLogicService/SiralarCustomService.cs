using AutoMapper;
using NToastNotify;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.CommonEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Enums = SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using Microsoft.Extensions.Logging;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class SiralarCustomService : ISiralarCustomService
    {
        private readonly IMapper _mapper;
        private readonly ISiralarService _siralarService;
        private readonly Context _context;
        private readonly ILogger<SiralarCustomService> _logger;

        public SiralarCustomService(IMapper mapper, ISiralarService siralarService, Context context, ILogger<SiralarCustomService> logger)
        {
            _mapper = mapper;
            _siralarService = siralarService;
            _context = context;
            _logger = logger;
        }

        public async Task<siraCagirmaDto?> GetSiraCagirmaAsync(string tcKimlikNo)
        {
            try
            {
                var today = DateTime.Today;

                // Uzmanlık seviyesine göre öncelikli çağırılacak işlemler
                var result = await (
                    from s in _context.Siralar.AsNoTracking()
                    join kp in _context.KanalPersonelleri on s.KanalAltIslemId equals kp.KanalAltIslemId
                    join p in _context.Personeller on kp.TcKimlikNo equals p.TcKimlikNo
                    join kai in _context.KanalAltIslemleri on new { kp.KanalAltIslemId, p.HizmetBinasiId } equals new { kai.KanalAltIslemId, kai.HizmetBinasiId }
                    join ka in _context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
                    join bk in _context.BankolarKullanici on p.TcKimlikNo equals bk.TcKimlikNo
                    join b in _context.Bankolar on bk.BankoId equals b.BankoId
                    join hc in _context.HubConnection on p.TcKimlikNo equals hc.TcKimlikNo
                    where p.TcKimlikNo == tcKimlikNo
                    && hc.ConnectionStatus == Enums.ConnectionStatus.online
                    && s.BeklemeDurum == Enums.BeklemeDurum.Beklemede
                    && b.BankoAktiflik == Enums.Aktiflik.Aktif
                    && s.SiraAlisZamani.Date == today
                    && kp.Uzmanlik != Enums.PersonelUzmanlik.BilgisiYok
                    orderby kp.Uzmanlik ascending, s.SiraNo ascending
                    select new
                    {
                        Siralar = s,
                        BankoNo = b.BankoNo,
                        KanalAltId = ka.KanalAltId,
                        KanalAltAdi = ka.KanalAltAdi,
                        TcKimlikNo = p.TcKimlikNo,
                        AdSoyad = p.AdSoyad
                    }).AsNoTracking().FirstOrDefaultAsync();

                if (result != null)
                {
                    // Önceki işlemi bulup ve güncelliyorum
                    var previousSira = await _context.Siralar
                        .Where(s => s.TcKimlikNo == tcKimlikNo && s.BeklemeDurum == Enums.BeklemeDurum.Cagrildi && s.SiraId != result.Siralar.SiraId && s.SiraAlisZamani.Date == today)
                        .OrderByDescending(s => s.IslemBaslamaZamani)
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

                    if (previousSira != null)
                    {
                        var previousSiraDto = _mapper.Map<SiralarDto>(previousSira);
                        previousSiraDto.IslemBitisZamani = DateTime.Now;
                        previousSiraDto.BeklemeDurum = Enums.BeklemeDurum.Bitti;

                        var previousUpdateResult = await _siralarService.TUpdateAsync(previousSiraDto);
                        if (!previousUpdateResult)
                        {
                            _logger.LogWarning("Önceki sıra bilgisi güncellenemedi.");
                        }
                    }

                    // Yeni çağrılan sıra için güncelleme
                    var newSiralarDto = new SiralarDto
                    {
                        SiraId = result.Siralar.SiraId,
                        SiraNo = result.Siralar.SiraNo,
                        KanalAltIslemId = result.Siralar.KanalAltIslemId,
                        KanalAltAdi = result.KanalAltAdi,
                        TcKimlikNo = tcKimlikNo,
                        SiraAlisZamani = result.Siralar.SiraAlisZamani,
                        IslemBaslamaZamani = DateTime.Now,
                        IslemBitisZamani = null,
                        BeklemeDurum = Enums.BeklemeDurum.Cagrildi,
                        HizmetBinasiId = result.Siralar.HizmetBinasiId
                    };

                    var newUpdateResult = await _siralarService.TUpdateAsync(newSiralarDto);

                    if (newUpdateResult)
                    {
                        return new siraCagirmaDto
                        {
                            SiraId = result.Siralar.SiraId,
                            SiraNo = result.Siralar.SiraNo,
                            BankoNo = result.BankoNo,
                            SiraAlisZamani = result.Siralar.SiraAlisZamani,
                            IslemBaslamaZamani = DateTime.Now,
                            IslemBitisZamani = null,
                            BeklemeDurum = Enums.BeklemeDurum.Cagrildi,
                            KanalAltIslemId = result.Siralar.KanalAltIslemId,
                            KanalAltId = result.KanalAltId,
                            KanalAltAdi = result.KanalAltAdi,
                            TcKimlikNo = tcKimlikNo,
                            AdSoyad = result.AdSoyad,
                        };
                    }
                    else
                    {
                        _logger.LogWarning("Yeni sıra bilgisi güncellenemedi.");
                        return null;
                    }
                }
                else
                {
                    _logger.LogWarning("Sıra bilgisi bulunamadı.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Sıra çağırma işlemi sırasında hata oluştu: {tcKimlikNo}");
                throw; // İstemciye hata ile ilgili bilgi göndermek için hatayı fırlatıyoruz
            }
        }

        public async Task<List<SiralarTvListeDto>> GetSiralarForTvWithHizmetBinasi(int hizmetBinasiId)
        {
            try
            {
                /*
                 SELECT TOP(5)s.SiraId,s.SiraNo,s.HizmetBinasiId,s.SiraAlisZamani,s.IslemBaslamaZamani,s.BeklemeDurum,s.TcKimlikNo,b.BankoId,b.BankoNo,b.BankoTipi,b.KatTipi,p.AdSoyad
                    FROM [SocialSecInstDB].[dbo].[Siralar] AS s
                    INNER JOIN [SocialSecInstDB].[dbo].[HizmetBinalari] AS hb ON hb.HizmetBinasiId = s.HizmetBinasiId
                    INNER JOIN [SocialSecInstDB].[dbo].[BankolarKullanici] AS bk ON bk.TcKimlikNo = s.TcKimlikNo
                    INNER JOIN [SocialSecInstDB].[dbo].[Bankolar] AS b ON b.BankoId = bk.BankoId
                    INNER JOIN [SocialSecInstDB].[dbo].[Personeller] AS p ON p.TcKimlikNo = s.TcKimlikNo
                    WHERE s.HizmetBinasiId = 1 
                      AND CAST(s.IslemBaslamaZamani AS DATE) = CAST(CURRENT_TIMESTAMP AS DATE)
                    ORDER BY
                        CASE WHEN s.BeklemeDurum = 1 THEN 1 ELSE 2 END,
                        CASE WHEN s.BeklemeDurum = 1 THEN s.IslemBaslamaZamani END DESC,
                        CASE WHEN s.BeklemeDurum = 2 THEN s.IslemBaslamaZamani END DESC
                 */
                var siralar = await _context.Siralar
                    .Join(_context.HizmetBinalari,
                        s => s.HizmetBinasiId,
                        hb => hb.HizmetBinasiId,
                        (s, hb) => new { s, hb })
                    .Join(_context.BankolarKullanici,
                        shb => shb.s.TcKimlikNo,
                        bk => bk.TcKimlikNo,
                        (shb, bk) => new { shb.s, bk })
                    .Join(_context.Bankolar,
                        sbk => sbk.bk.BankoId,
                        b => b.BankoId,
                        (sbk, b) => new { sbk.s, b })
                    .Join(_context.Personeller,
                        sb => sb.s.TcKimlikNo,
                        p => p.TcKimlikNo,
                        (sb, p) => new SiralarTvListeDto
                        {
                            SiraId = sb.s.SiraId,
                            SiraNo = sb.s.SiraNo,
                            SiraAlisZamani = sb.s.SiraAlisZamani,
                            IslemBaslamaZamani = sb.s.IslemBaslamaZamani,
                            BeklemeDurum = sb.s.BeklemeDurum,
                            TcKimlikNo = sb.s.TcKimlikNo,
                            BankoId = sb.b.BankoId,
                            BankoNo = sb.b.BankoNo,
                            BankoTipi = sb.b.BankoTipi,
                            KatTipi = sb.b.KatTipi,
                            AdSoyad = p.AdSoyad,
                            HizmetBinasiId = sb.s.HizmetBinasiId
                        })
                    .Where(sp => sp.HizmetBinasiId == hizmetBinasiId && sp.IslemBaslamaZamani.HasValue && sp.IslemBaslamaZamani.Value.Date == DateTime.Now.Date)
                    .OrderBy(sp => sp.BeklemeDurum == BeklemeDurum.Cagrildi ? 1 : 2)
                    .ThenByDescending(sp => sp.BeklemeDurum == BeklemeDurum.Cagrildi ? sp.IslemBaslamaZamani : (DateTime?)null)
                    .ThenByDescending(sp => sp.BeklemeDurum == BeklemeDurum.Bitti ? sp.IslemBaslamaZamani : (DateTime?)null)
                    .Take(5)
                    .ToListAsync();

                return siralar;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"TV için Sıra çağırma işlemi sırasında hata oluştu hizmetBinasiId: {hizmetBinasiId}");
                throw; // İstemciye hata ile ilgili bilgi göndermek için hatayı fırlatıyoruz
            }
        }

        public async Task<List<SiralarDto>> GetSiralarWithHizmetBinasiAsync(int hizmetBinasiId)
        {
            try
            {
                var siralar = await _context.Siralar
                    .Include(s => s.HizmetBinalari)
                    .Where(s => s.HizmetBinasiId == hizmetBinasiId &&
                                (s.BeklemeDurum == BeklemeDurum.Beklemede || s.BeklemeDurum == BeklemeDurum.Cagrildi) &&
                                s.SiraAlisZamani.Date == DateTime.Now.Date)
                    .OrderBy(s => s.SiraAlisZamani)
                    .ThenBy(s => s.SiraNo)
                    .ToListAsync();

                var siralarDto = siralar.Select(s => new SiralarDto
                {
                    SiraId = s.SiraId,
                    SiraNo = s.SiraNo,
                    KanalAltIslemId = s.KanalAltIslemId,
                    KanalAltAdi = s.KanalAltAdi,
                    HizmetBinasiId = s.HizmetBinasiId,
                    TcKimlikNo = s.TcKimlikNo,
                    SiraAlisZamani = s.SiraAlisZamani,
                    IslemBaslamaZamani = s.IslemBaslamaZamani,
                    IslemBitisZamani = s.IslemBitisZamani,
                    BeklemeDurum = s.BeklemeDurum
                }).ToList();

                return siralarDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hizmet Binasına ait sıralarda hata oldu!");
                throw;
            }
        }

        public async Task<List<siraCagirmaDto>> GetSiraListeAsync(string tcKimlikNo)
        {
            try
            {
                var result = await (
                    from s in _context.Siralar.AsNoTracking()
                    join kp in _context.KanalPersonelleri on s.KanalAltIslemId equals kp.KanalAltIslemId
                    join p in _context.Personeller on kp.TcKimlikNo equals p.TcKimlikNo
                    join kai in _context.KanalAltIslemleri on new { s.KanalAltIslemId, p.HizmetBinasiId } equals new { kai.KanalAltIslemId, kai.HizmetBinasiId }
                    join ka in _context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
                    join bk in _context.BankolarKullanici on p.TcKimlikNo equals bk.TcKimlikNo
                    join b in _context.Bankolar on bk.BankoId equals b.BankoId
                    where p.TcKimlikNo == tcKimlikNo
                        && s.SiraAlisZamani.Date == DateTime.Now.Date
                        && b.BankoAktiflik == Enums.Aktiflik.Aktif
                        && (s.BeklemeDurum == Enums.BeklemeDurum.Beklemede || (s.BeklemeDurum == Enums.BeklemeDurum.Cagrildi && s.TcKimlikNo == tcKimlikNo))
                        && kp.Uzmanlik != Enums.PersonelUzmanlik.BilgisiYok
                    orderby
                        s.BeklemeDurum == Enums.BeklemeDurum.Cagrildi descending, // BeklemeDurum = 1 olanları en yukarıda
                        kp.Uzmanlik, // Uzmanlık seviyesine göre sıralama
                        s.SiraNo, // Sıra numarasına göre sıralama
                        s.SiraAlisZamani // Sıra alış zamanına göre sıralama
                    select new siraCagirmaDto
                    {
                        SiraId = s.SiraId,
                        SiraNo = s.SiraNo,
                        BeklemeDurum = s.BeklemeDurum,
                        TcKimlikNo = s.TcKimlikNo,
                        KanalAltAdi = s.KanalAltAdi,
                        IslemiYapan = s.TcKimlikNo == p.TcKimlikNo ? "kendisi" : "baskasi",
                        PersonelUzmanlik = kp.Uzmanlik

                    }).ToListAsync();


                return result ?? new List<siraCagirmaDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Sıra listeleme işlemi sırasında hata oluştu: {tcKimlikNo}");
                return new List<siraCagirmaDto>();
            }
        }

        public async Task<SiraNoBilgisiDto> GetSiraNoAsync(int kanalAltIslemId)
        {
            var today = DateTime.Today;

            var kanalIslemId = await _context.KanalAltIslemleri
                .Where(kai => kai.KanalAltIslemId == kanalAltIslemId)
                .Select(kai => kai.KanalIslemId)
                .FirstOrDefaultAsync();

            if (kanalIslemId == default)
            {
                return new SiraNoBilgisiDto
                {
                    SiraNo = 0,
                    HizmetBinasiId = 0,
                    HizmetBinasiAdi = "KanalIslemId bulunamadı!"
                };
            }

            var lastNumberQuery = from kai in _context.KanalAltIslemleri
                                  join ki in _context.KanalIslemleri on kai.KanalIslemId equals ki.KanalIslemId
                                  join s in _context.Siralar.Where(s => s.SiraAlisZamani.Date == today) on kai.KanalAltIslemId equals s.KanalAltIslemId into sJoin
                                  from sLeft in sJoin.DefaultIfEmpty()
                                  where kai.KanalAltIslemAktiflik == Enums.Aktiflik.Aktif && ki.KanalIslemId == kanalIslemId
                                  select (int?)sLeft.SiraNo;

            var lastNumber = (await lastNumberQuery.MaxAsync()) ?? (await _context.KanalIslemleri
                .Where(ki => ki.KanalIslemId == kanalIslemId)
                .Select(ki => ki.BaslangicNumara)
                .FirstOrDefaultAsync()) - 1;

            var query = from kai in _context.KanalAltIslemleri
                        join ka in _context.KanallarAlt on kai.KanalAltId equals ka.KanalAltId
                        join ki in _context.KanalIslemleri on kai.KanalIslemId equals ki.KanalIslemId
                        join hb in _context.HizmetBinalari on kai.HizmetBinasiId equals hb.HizmetBinasiId
                        join b in _context.Bankolar on kai.HizmetBinasiId equals b.HizmetBinasiId
                        join bk in _context.BankolarKullanici on b.BankoId equals bk.BankoId
                        join kp in _context.KanalPersonelleri on new { bk.TcKimlikNo, kai.KanalAltIslemId } equals new { kp.TcKimlikNo, kp.KanalAltIslemId }
                        where kai.KanalAltIslemAktiflik == Enums.Aktiflik.Aktif && kai.KanalAltIslemId == kanalAltIslemId
                        select new
                        {
                            NextSiraNo = lastNumber >= ki.BaslangicNumara && lastNumber < ki.BitisNumara ? lastNumber + 1 : ki.BaslangicNumara,
                            hb.HizmetBinasiId,
                            hb.HizmetBinasiAdi,
                            ka.KanalAltAdi
                        };

            var result = await query.FirstOrDefaultAsync();

            if (result == null)
            {
                return new SiraNoBilgisiDto
                {
                    SiraNo = 0,
                    HizmetBinasiId = 0,
                    HizmetBinasiAdi = "SiraNo Bulunamadı!"
                };
            }
            else
            {
                var newSira = new Siralar
                {
                    SiraNo = result.NextSiraNo,
                    KanalAltIslemId = kanalAltIslemId,
                    HizmetBinasiId = result.HizmetBinasiId,
                    SiraAlisZamani = DateTime.Now,
                    BeklemeDurum = Enums.BeklemeDurum.Beklemede,
                    KanalAltAdi = result.KanalAltAdi
                };

                var insertResult = await _siralarService.TInsertAsync(_mapper.Map<SiralarDto>(newSira));

                if (!insertResult.IsSuccess)
                {
                    return new SiraNoBilgisiDto
                    {
                        SiraNo = 0,
                        HizmetBinasiId = 0,
                        HizmetBinasiAdi = "SiraNo Kaydedilemedi!"
                    };
                }
                else
                {
                    return new SiraNoBilgisiDto
                    {
                        SiraNo = result.NextSiraNo,
                        HizmetBinasiId = result.HizmetBinasiId,
                        HizmetBinasiAdi = result.HizmetBinasiAdi
                    };
                }
            }
        }
    }
}
