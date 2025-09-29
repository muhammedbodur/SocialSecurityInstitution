using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessObjectLayer.Extensions;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class TvlerDal : GenericRepository<Tvler, TvlerDto>, ITvlerDal
    {
        public TvlerDal(Context context, IMapper mapper, ILogService logService) : base(context, mapper, logService)
        {
        }

        public async Task<List<TvBankolarRequestDto>> GetTvBankolarEslesenleriGetirAsync(int tvId)
        {
            var bankolar = await _context.Bankolar
                .Join(
                    _context.TvBankolari,
                    b => b.BankoId,
                    tb => tb.BankoId,
                    (b, tb) => new { Banko = b, TvBanko = tb })
                .Where(bt => bt.TvBanko.TvId == tvId) 
                .Select(b => new TvBankolarRequestDto
                {
                    TvBankoId = b.TvBanko.TvBankoId,
                    BankoId = b.Banko.BankoId,
                    HizmetBinasiId = b.Banko.HizmetBinasiId,
                    BankoNo = b.Banko.BankoNo,
                    BankoTipi = b.Banko.BankoTipi,
                    KatTipi = b.Banko.KatTipi,
                    BankoAktiflik = b.Banko.BankoAktiflik,
                    EklenmeTarihi = b.Banko.EklenmeTarihi,
                    DuzenlenmeTarihi = b.Banko.DuzenlenmeTarihi,
                })
                .ToListAsync();

            return bankolar;
        }

        public async Task<List<BankolarDto>> GetTvBankolarEslesmeyenleriGetirAsync(int tvId, int hizmetBinasiId)
        {
            var allBankolar = await _context.Bankolar
                .Where(b => b.HizmetBinasiId == hizmetBinasiId)
                .Select(b => new BankolarDto
                {
                    BankoId = b.BankoId,
                    HizmetBinasiId = b.HizmetBinasiId,
                    BankoNo = b.BankoNo,
                    BankoTipi = b.BankoTipi,
                    KatTipi = b.KatTipi,
                    BankoAktiflik = b.BankoAktiflik,
                    EklenmeTarihi = b.EklenmeTarihi,
                    DuzenlenmeTarihi = b.DuzenlenmeTarihi
                })
                .ToListAsync();

            var matchedBankolar = await _context.Bankolar
                .Join(
                    _context.TvBankolari,
                    b => b.BankoId,
                    tb => tb.BankoId,
                    (b, tb) => new { Banko = b, TvBanko = tb })
                .Where(bt => bt.TvBanko.TvId == tvId && bt.Banko.HizmetBinasiId == hizmetBinasiId)
                .Select(b => new BankolarDto
                {
                    BankoId = b.Banko.BankoId,
                    HizmetBinasiId = b.Banko.HizmetBinasiId,
                    BankoNo = b.Banko.BankoNo,
                    BankoTipi = b.Banko.BankoTipi,
                    KatTipi = b.Banko.KatTipi,
                    BankoAktiflik = b.Banko.BankoAktiflik,
                    EklenmeTarihi = b.Banko.EklenmeTarihi,
                    DuzenlenmeTarihi = b.Banko.DuzenlenmeTarihi
                })
                .ToListAsync();

            // Tüm bankolardan eşleşenleri çıkar
            var unmatchedBankolar = allBankolar
                .Where(ab => !matchedBankolar.Any(mb => mb.BankoId == ab.BankoId))
                .ToList();

            return unmatchedBankolar;
        }

        public async Task<List<TvlerBankolarSayiDto>> GetTvlerBankolarWithSayiAsync(int hizmetBinasiId)
        {
            var tvlerBankolarDto = await _context.Tvler
                .Where(tv => tv.HizmetBinasiId == hizmetBinasiId)
                .GroupJoin(_context.TvBankolari,
                    tv => tv.TvId,
                    tb => tb.TvId,
                    (tv, tbs) => new { Tv = tv, TvBankolari = tbs })
                .SelectMany(
                    tvtb => tvtb.TvBankolari.DefaultIfEmpty(),
                    (tvtb, tb) => new { tvtb.Tv, TvBanko = tb })
                .GroupBy(tvtb => new { tvtb.Tv.TvId, tvtb.Tv.KatTipi })
                .Select(g => new
                {
                    TvId = g.Key.TvId,
                    KatTipi = g.Key.KatTipi,
                    BankoEslesmeSayisi = g.Count(tvtb => tvtb.TvBanko != null)
                })
                .ToListAsync();

            var requestDtos = tvlerBankolarDto.Select(t => new TvlerBankolarSayiDto
            {
                TvId = t.TvId,
                KatTipi = t.KatTipi,
                KatTipiAdi = ((KatTipi)t.KatTipi).GetDisplayName(),
                BankoEslesmeSayisi = t.BankoEslesmeSayisi
            }).ToList();

            return requestDtos;
        }

        public async Task<List<TvlerWithConnectionIdDto>> GetTvlerConnectionWithBankoIdAsync(int bankoId)
        {
            var tvlerWithConnections = await _context.Tvler
                .Join(_context.HubTvConnection,
                    t => t.TvId,
                    htc => htc.TvId,
                    (t, htc) => new { Tv = t, HubTvConnection = htc })
                .Join(_context.TvBankolari,
                    th => th.Tv.TvId,
                    tb => tb.TvId,
                    (th, tb) => new { th.Tv, th.HubTvConnection, TvBankolari = tb })
                .Join(_context.Bankolar,
                    tht => tht.TvBankolari.BankoId,
                    b => b.BankoId,
                    (tht, b) => new { tht.Tv, tht.HubTvConnection, Banko = b })
                .Where(thb => thb.Banko.BankoId == bankoId)
                .Select(thb => new TvlerWithConnectionIdDto
                {
                    TvId = thb.Tv.TvId,
                    HizmetBinasiId = thb.Tv.HizmetBinasiId,
                    ConnectionId = thb.HubTvConnection.ConnectionId,
                    ConnectionStatus = thb.HubTvConnection.ConnectionStatus
                })
                .ToListAsync();

            return tvlerWithConnections;
        }

        public async Task<List<TvlerWithConnectionIdDto>> GetTvlerConnectionWithBankolarKullaniciTcKimlikNoAsync(string tcKimlikNo)
        {
            var tvlerWithConnections = await _context.Tvler
                .Join(_context.HubTvConnection,
                    t => t.TvId,
                    htc => htc.TvId,
                    (t, htc) => new { Tv = t, HubTvConnection = htc })
                .Join(_context.TvBankolari,
                    th => th.Tv.TvId,
                    tb => tb.TvId,
                    (th, tb) => new { th.Tv, th.HubTvConnection, TvBankolari = tb })
                .Join(_context.Bankolar,
                    tht => tht.TvBankolari.BankoId,
                    b => b.BankoId,
                    (tht, b) => new { tht.Tv, tht.HubTvConnection, Banko = b })
                .Join(_context.BankolarKullanici,
                    thtb => thtb.Banko.BankoId,
                    bk => bk.BankoId,
                    (thtb, bk) => new { thtb.Tv, thtb.HubTvConnection, BankoKullanici = bk })
                .Where(thb => thb.BankoKullanici.TcKimlikNo == tcKimlikNo)
                .Select(thb => new TvlerWithConnectionIdDto
                {
                    TvId = thb.Tv.TvId,
                    HizmetBinasiId = thb.Tv.HizmetBinasiId,
                    ConnectionId = thb.HubTvConnection.ConnectionId,
                    ConnectionStatus = thb.HubTvConnection.ConnectionStatus
                })
                .ToListAsync();

            return tvlerWithConnections;
        }

        public async Task<List<TvlerWithConnectionIdDto>> GetTvlerConnectionWithHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            var result = await _context.Tvler
                .Where(t => t.HizmetBinasiId == hizmetBinasiId)
                .Join(
                    _context.HubTvConnection,
                    t => t.TvId,
                    htc => htc.TvId,
                    (t, htc) => new TvlerWithConnectionIdDto
                    {
                        TvId = t.TvId,
                        HizmetBinasiId = t.HizmetBinasiId,
                        ConnectionId = htc.ConnectionId,
                        ConnectionStatus = htc.ConnectionStatus
                    }
                )
                .ToListAsync();

            return result;
        }

        public async Task<List<TvlerDetailDto>> GetTvlerWithHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            var result = await _context.Tvler
                .Where(t => t.HizmetBinasiId == hizmetBinasiId)
                .Join(
                    _context.HizmetBinalari,
                    t => t.HizmetBinasiId,
                    hb => hb.HizmetBinasiId,
                    (t, hb) => new { t, hb }
                )
                .Join(
                    _context.Departmanlar,
                    th => th.hb.DepartmanId,
                    d => d.DepartmanId,
                    (th, d) => new TvlerDetailDto
                    {
                        TvId = th.t.TvId,
                        KatTipi = th.t.KatTipi,
                        Aciklama = th.t.Aciklama,
                        HizmetBinasiId = th.t.HizmetBinasiId,
                        HizmetBinasiAdi = th.hb.HizmetBinasiAdi,
                        DepartmanId = d.DepartmanId,
                        DepartmanAdi = d.DepartmanAdi
                    })
                .ToListAsync();

            return result;
        }

        public async Task<TvlerDetailDto> GetTvWithTvIdAsync(int tvId)
        {
            var result = await _context.Tvler
                .Where(t => t.TvId == tvId)
                .Join(
                    _context.HizmetBinalari,
                    t => t.HizmetBinasiId,
                    hb => hb.HizmetBinasiId,
                    (t, hb) => new { t, hb }
                )
                .Join(
                    _context.Departmanlar,
                    th => th.hb.DepartmanId,
                    d => d.DepartmanId,
                    (th, d) => new TvlerDetailDto
                    {
                        TvId = th.t.TvId,
                        KatTipi = th.t.KatTipi,
                        Aciklama = th.t.Aciklama,
                        HizmetBinasiId = th.t.HizmetBinasiId,
                        HizmetBinasiAdi = th.hb.HizmetBinasiAdi,
                        DepartmanId = d.DepartmanId,
                        DepartmanAdi = d.DepartmanAdi
                    })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
