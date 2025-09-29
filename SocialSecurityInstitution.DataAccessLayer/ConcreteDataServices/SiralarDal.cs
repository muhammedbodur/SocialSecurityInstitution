using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.CommonEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enums = SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class SiralarDal : GenericRepository<Siralar, SiralarDto>, ISiralarDal
    {
        public SiralarDal(Context context, IMapper mapper, ILogService logService) : base(context, mapper, logService)
        {
        }

        public async Task<siraCagirmaDto?> GetSiraCagirmaAsync(string tcKimlikNo)
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
                    // Önceki sırayı bitir
                    previousSira.IslemBitisZamani = DateTime.Now;
                    previousSira.BeklemeDurum = Enums.BeklemeDurum.Bitti;
                    _context.Siralar.Update(previousSira);
                    await _context.SaveChangesAsync();
                }

                // Yeni çağrılan sıra için güncelleme
                var newUpdateResult = await _context.Siralar
                    .Where(s => s.SiraId == result.Siralar.SiraId)
                    .FirstOrDefaultAsync();

                if (newUpdateResult != null)
                {
                    newUpdateResult.IslemBaslamaZamani = DateTime.Now;
                    newUpdateResult.BeklemeDurum = Enums.BeklemeDurum.Cagrildi;
                    _context.Siralar.Update(newUpdateResult);
                    await _context.SaveChangesAsync();

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
            }

            return null;
        }

        public async Task<List<SiralarForTvDto>> GetSiralarForTvWithHizmetBinasiAsync(int hizmetBinasiId)
        {
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
                    (sb, p) => new SiralarForTvDto
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
                .OrderBy(sp => sp.BeklemeDurum == Enums.BeklemeDurum.Cagrildi ? 1 : 2)
                .ThenByDescending(sp => sp.BeklemeDurum == Enums.BeklemeDurum.Cagrildi ? sp.IslemBaslamaZamani : (DateTime?)null)
                .ThenByDescending(sp => sp.BeklemeDurum == Enums.BeklemeDurum.Bitti ? sp.IslemBaslamaZamani : (DateTime?)null)
                .Take(5)
                .ToListAsync();

            return siralar;
        }

        public async Task<List<SiralarForTvDto>> GetSiralarForTvWithTvIdAsync(int tvId)
        {
            var siralar = await _context.Siralar
                .Join(_context.BankolarKullanici,
                    s => s.TcKimlikNo,
                    bk => bk.TcKimlikNo,
                    (s, bk) => new { s, bk })
                .Join(_context.Bankolar,
                    sbk => sbk.bk.BankoId,
                    b => b.BankoId,
                    (sbk, b) => new { sbk.s, sbk.bk, b })
                .Join(_context.TvBankolari,
                    sb => sb.b.BankoId,
                    tb => tb.BankoId,
                    (sb, tb) => new { sb.s, sb.bk, sb.b, tb })
                .Join(_context.Tvler,
                    sbtb => sbtb.tb.TvId,
                    tv => tv.TvId,
                    (sbtb, tv) => new { sbtb.s, sbtb.bk, sbtb.b, sbtb.tb, tv })
                .Join(_context.HubTvConnection,
                    sbtbtv => sbtbtv.tv.TvId,
                    htc => htc.TvId,
                    (sbtbtv, htc) => new { sbtbtv.s, sbtbtv.bk, sbtbtv.b, sbtbtv.tv, sbtbtv.tb, HubTvConnection = htc })
                .Join(_context.Personeller,
                    sbtbtvh => sbtbtvh.s.TcKimlikNo,
                    p => p.TcKimlikNo,
                    (sbtbtvh, p) => new SiralarForTvDto
                    {
                        SiraId = sbtbtvh.s.SiraId,
                        SiraNo = sbtbtvh.s.SiraNo,
                        SiraAlisZamani = sbtbtvh.s.SiraAlisZamani,
                        IslemBaslamaZamani = sbtbtvh.s.IslemBaslamaZamani,
                        BeklemeDurum = sbtbtvh.s.BeklemeDurum,
                        TcKimlikNo = sbtbtvh.s.TcKimlikNo,
                        BankoId = sbtbtvh.b.BankoId,
                        BankoNo = sbtbtvh.b.BankoNo,
                        BankoTipi = sbtbtvh.b.BankoTipi,
                        KatTipi = sbtbtvh.b.KatTipi,
                        AdSoyad = p.AdSoyad,
                        TvId = sbtbtvh.tv.TvId,
                        ConnectionId = sbtbtvh.HubTvConnection.ConnectionId
                    })
                .Where(sbtbtvh => sbtbtvh.TvId == tvId && sbtbtvh.IslemBaslamaZamani.HasValue && sbtbtvh.IslemBaslamaZamani.Value.Date == DateTime.Now.Date)
                .OrderBy(sp => sp.BeklemeDurum == Enums.BeklemeDurum.Cagrildi ? 1 : 2)
                .ThenByDescending(sp => sp.BeklemeDurum == Enums.BeklemeDurum.Cagrildi ? sp.IslemBaslamaZamani : (DateTime?)null)
                .ThenByDescending(sp => sp.BeklemeDurum == Enums.BeklemeDurum.Bitti ? sp.IslemBaslamaZamani : (DateTime?)null)
                .Take(5)
                .ToListAsync();

            return siralar;
        }

        public async Task<List<SiralarRequestDto>> GetSiralarWithHizmetBinasiAsync(int hizmetBinasiId)
        {
            var result = await _context.Siralar
                .Join(_context.KanalAltIslemleri,
                    s => s.KanalAltIslemId,
                    kai => kai.KanalAltIslemId,
                    (s, kai) => new { s, kai })
                .Join(_context.KanallarAlt,
                    sk => sk.kai.KanalAltId,
                    ka => ka.KanalAltId,
                    (sk, ka) => new { sk.s, sk.kai, ka })
                .Join(_context.HizmetBinalari,
                    ska => ska.s.HizmetBinasiId,
                    hb => hb.HizmetBinasiId,
                    (ska, hb) => new { ska.s, ska.kai, ska.ka, hb })
                .Join(_context.Personeller,
                    skah => skah.s.TcKimlikNo,
                    p => p.TcKimlikNo,
                    (skah, p) => new SiralarRequestDto
                    {
                        SiraId = skah.s.SiraId,
                        SiraNo = skah.s.SiraNo,
                        KanalAltIslemId = skah.s.KanalAltIslemId,
                        KanalAltAdi = skah.ka.KanalAltAdi,
                        HizmetBinasiId = skah.s.HizmetBinasiId,
                        HizmetBinasiAdi = skah.hb.HizmetBinasiAdi,
                        TcKimlikNo = skah.s.TcKimlikNo,
                        AdSoyad = p.AdSoyad,
                        SiraAlisZamani = skah.s.SiraAlisZamani,
                        IslemBaslamaZamani = skah.s.IslemBaslamaZamani,
                        IslemBitisZamani = skah.s.IslemBitisZamani,
                        BeklemeDurum = skah.s.BeklemeDurum,
                        BeklemeDurumAdi = skah.s.BeklemeDurum.ToString()
                    })
                .Where(sr => sr.HizmetBinasiId == hizmetBinasiId &&
                            (sr.BeklemeDurum == Enums.BeklemeDurum.Beklemede || sr.BeklemeDurum == Enums.BeklemeDurum.Cagrildi) &&
                            sr.SiraAlisZamani.Date == DateTime.Now.Date)
                .OrderBy(sr => sr.SiraAlisZamani)
                .ThenBy(sr => sr.SiraNo)
                .ToListAsync();

            return result;
        }

        public async Task<List<siraCagirmaDto>> GetSiraListeAsync(string tcKimlikNo)
        {
            var result = await (
                from s in _context.Siralar.AsNoTracking()
                join kp in _context.KanalPersonelleri on s.KanalAltIslemId equals kp.KanalAltIslemId
                join p in _context.Personeller on kp.TcKimlikNo equals p.TcKimlikNo
                join kai in _context.KanalAltIslemleri on new { kp.KanalAltIslemId, p.HizmetBinasiId } equals new { kai.KanalAltIslemId, kai.HizmetBinasiId }
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

        public async Task<SiraNoBilgisiDto> GetSiraNoAsync(int kanalAltIslemId)
        {
            var today = DateTime.Today;

            // KanalIslemId'yi al
            var kanalIslemId = await _context.KanalAltIslemleri
                .Where(kai => kai.KanalAltIslemId == kanalAltIslemId)
                .Select(kai => kai.KanalIslemId)
                .FirstOrDefaultAsync();

            if (kanalIslemId == null || kanalIslemId == 0)
            {
                return new SiraNoBilgisiDto
                {
                    SiraNo = 0,
                    HizmetBinasiId = 0,
                    HizmetBinasiAdi = "KanalIslemId bulunamadı!",
                    KanalAltAdi = "KanalIslemId bulunamadı!"
                };
            }

            var lastNumberQuery = from kai in _context.KanalAltIslemleri
                                  join ki in _context.KanalIslemleri on kai.KanalIslemId equals ki.KanalIslemId
                                  join s in _context.Siralar.Where(s => s.SiraAlisZamani.Date == today) on kai.KanalAltIslemId equals s.KanalAltIslemId into sJoin
                                  from sLeft in sJoin.DefaultIfEmpty()
                                  where kai.KanalAltIslemAktiflik == Enums.Aktiflik.Aktif && ki.KanalIslemId == kanalIslemId.Value
                                  select (int?)sLeft.SiraNo;

            var maxSiraNo = await lastNumberQuery.MaxAsync();
            var baslangicNumara = await _context.KanalIslemleri
                .Where(ki => ki.KanalIslemId == kanalIslemId.Value)
                .Select(ki => ki.BaslangicNumara)
                .FirstOrDefaultAsync();

            var lastNumber = maxSiraNo.HasValue ? maxSiraNo.Value : (baslangicNumara - 1);

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
                    HizmetBinasiAdi = "SiraNo Bulunamadı!",
                    KanalAltAdi = "SiraNo Bulunamadı!"
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

                _context.Siralar.Add(newSira);
                var insertedRows = await _context.SaveChangesAsync();

                if (insertedRows > 0)
                {
                    return new SiraNoBilgisiDto
                    {
                        SiraNo = result.NextSiraNo,
                        HizmetBinasiId = result.HizmetBinasiId,
                        HizmetBinasiAdi = result.HizmetBinasiAdi,
                        KanalAltAdi = result.KanalAltAdi
                    };
                }
                else
                {
                    return new SiraNoBilgisiDto
                    {
                        SiraNo = 0,
                        HizmetBinasiId = 0,
                        HizmetBinasiAdi = "SiraNo Kaydedilemedi!",
                        KanalAltAdi = "SiraNo Kaydedilemedi!"
                    };
                }
            }
        }
    }
}