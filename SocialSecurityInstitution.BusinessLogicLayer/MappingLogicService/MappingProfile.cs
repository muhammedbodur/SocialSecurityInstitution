using AutoMapper;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SocialSecurityInstitution.BusinessLogicLayer.MappingLogicService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AtanmaNedenleri, AtanmaNedenleriDto>().ReverseMap();
            CreateMap<LoginLogoutLog, LoginLogoutLogDto>().ReverseMap();
            CreateMap<Personeller, PersonellerDto>().ReverseMap();
            CreateMap<Departmanlar, DepartmanlarDto>().ReverseMap();
            CreateMap<HizmetBinalari, HizmetBinalariDto>().ReverseMap();
            CreateMap<Servisler, ServislerDto>().ReverseMap();
            CreateMap<Unvanlar, UnvanlarDto>().ReverseMap();
            CreateMap<Bankolar, BankolarDto>().ReverseMap();
            CreateMap<BankolarKullanici, BankolarKullaniciDto>().ReverseMap();
            CreateMap<Kanallar, KanallarDto>().ReverseMap();
            CreateMap<KanallarAlt, KanallarAltDto>().ReverseMap();
            CreateMap<KanalIslemleri, KanalIslemleriDto>().ReverseMap();
            CreateMap<KanalAltIslemleri, KanalAltIslemleriDto>().ReverseMap();
            CreateMap<KanalPersonelleri, KanalPersonelleriDto>().ReverseMap();
            CreateMap<KioskGruplari, KioskGruplariDto>().ReverseMap();
            CreateMap<KioskIslemGruplari, KioskIslemGruplariDto>().ReverseMap();
            CreateMap<Iller, IllerDto>().ReverseMap();
            CreateMap<Ilceler, IlcelerDto>().ReverseMap();
            CreateMap<Sendikalar, SendikalarDto>().ReverseMap();
            CreateMap<Siralar, SiralarDto>().ReverseMap();
            CreateMap<HubConnection, HubConnectionDto>().ReverseMap();
            CreateMap<Tvler, TvlerDto>().ReverseMap();
            CreateMap<HubTvConnection, HubTvConnectionDto>().ReverseMap();
            CreateMap<TvBankolariDto, TvBankolari>().ReverseMap();
            CreateMap<Yetkiler, YetkilerDto>().ReverseMap();
            CreateMap<PersonelYetkileri, PersonelYetkileriDto>().ReverseMap();

            /*Dto to Dto*/
            CreateMap<LoginDto, LoginLogoutLogDto>()
            .ForMember(dest => dest.TcKimlikNo, opt => opt.MapFrom(src => src.TcKimlikNo))
            .ForMember(dest => dest.LoginTime, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.LogoutTime, opt => opt.MapFrom(src => DateTime.MinValue));
            CreateMap<BankolarDto, BankolarRequestDto>().ReverseMap();
            CreateMap<HizmetBinalariDepartmanlarDto, BankolarHizmetBinalariDepartmanlarDto>();
            CreateMap<PersonellerDto, PersonelRequestDto>().ReverseMap();
            CreateMap<PersonellerDto, PersonellerLiteDto>().ReverseMap();
            CreateMap<KanalIslemleriDto, KanalIslemleriRequestDto>().ReverseMap();

            CreateMap<PersonellerDto, PersonellerViewDto>()
                .ForMember(dest => dest.IlId, opt => opt.MapFrom(src => src.Il != null ? src.Il.IlId : 0))
                .ForMember(dest => dest.IlAdi, opt => opt.MapFrom(src => src.Il != null ? src.Il.IlAdi : null))
                .ForMember(dest => dest.IlceId, opt => opt.MapFrom(src => src.Ilce != null ? src.Ilce.IlceId : 0))
                .ForMember(dest => dest.IlceAdi, opt => opt.MapFrom(src => src.Ilce != null ? src.Ilce.IlceAdi : null))
                .ForMember(dest => dest.SendikaId, opt => opt.MapFrom(src => src.Sendika != null ? src.Sendika.SendikaId : 0))
                .ForMember(dest => dest.SendikaAdi, opt => opt.MapFrom(src => src.Sendika != null ? src.Sendika.SendikaAdi : null))
                .ForMember(dest => dest.EsininIsIlId, opt => opt.MapFrom(src => src.EsininIsIl != null ? src.EsininIsIl.IlId : 0))
                .ForMember(dest => dest.EsininIsIlAdi, opt => opt.MapFrom(src => src.EsininIsIl != null ? src.EsininIsIl.IlAdi : null))
                .ForMember(dest => dest.EsininIsIlceId, opt => opt.MapFrom(src => src.EsininIsIlce != null ? src.EsininIsIlce.IlceId : 0))
                .ForMember(dest => dest.EsininIsIlceAdi, opt => opt.MapFrom(src => src.EsininIsIlce != null ? src.EsininIsIlce.IlceAdi : null))
                .ForMember(dest => dest.Departmanlar, opt => opt.Ignore())
                .ForMember(dest => dest.Servisler, opt => opt.Ignore())
                .ForMember(dest => dest.Unvanlar, opt => opt.Ignore())
                .ForMember(dest => dest.AtanmaNedenleri, opt => opt.Ignore())
                .ForMember(dest => dest.HizmetBinalari, opt => opt.Ignore())
                .ForMember(dest => dest.Iller, opt => opt.Ignore())
                .ForMember(dest => dest.Ilceler, opt => opt.Ignore())
                .ForMember(dest => dest.Sendikalar, opt => opt.Ignore());

            // 1. PersonelUpdateDto -> PersonellerDto mapping (Korumalı alanlar ignore edilir)
            CreateMap<PersonelUpdateDto, PersonellerDto>()
                // Korunması gereken alanları ignore et
                .ForMember(dest => dest.PassWord, opt => opt.Ignore())
                .ForMember(dest => dest.SessionID, opt => opt.Ignore())
                .ForMember(dest => dest.EklenmeTarihi, opt => opt.Ignore())
                .ForMember(dest => dest.NickName, opt => opt.Ignore())
                .ForMember(dest => dest.PersonelKayitNo, opt => opt.Ignore())

                // Navigation property'leri ignore et
                .ForMember(dest => dest.Il, opt => opt.Ignore())
                .ForMember(dest => dest.Ilce, opt => opt.Ignore())
                .ForMember(dest => dest.Sendika, opt => opt.Ignore())
                .ForMember(dest => dest.EsininIsIl, opt => opt.Ignore())
                .ForMember(dest => dest.EsininIsIlce, opt => opt.Ignore())
                .ForMember(dest => dest.BankolarKullanici, opt => opt.Ignore())
                .ForMember(dest => dest.KanalPersonelleri, opt => opt.Ignore())

                // Özel dönüşümler
                .ForMember(dest => dest.DuzenlenmeTarihi, opt => opt.MapFrom(src => DateTime.Now))

                // Null foreign key'leri 0'a çevir (veritabanı constraint'leri için)
                .ForMember(dest => dest.IlId, opt => opt.MapFrom(src => src.IlId ?? 0))
                .ForMember(dest => dest.IlceId, opt => opt.MapFrom(src => src.IlceId ?? 0))
                .ForMember(dest => dest.SendikaId, opt => opt.MapFrom(src => src.SendikaId ?? 0))
                .ForMember(dest => dest.EsininIsIlId, opt => opt.MapFrom(src => src.EsininIsIlId ?? 0))
                .ForMember(dest => dest.EsininIsIlceId, opt => opt.MapFrom(src => src.EsininIsIlceId ?? 0));

            // 2. PersonellerViewDto -> PersonelUpdateDto mapping (Form'dan gelen veriler için)
            CreateMap<PersonellerViewDto, PersonelUpdateDto>()
                // Nullable foreign key'ler için özel mapping
                .ForMember(dest => dest.IlId, opt => opt.MapFrom(src => src.IlId == 0 ? (int?)null : src.IlId))
                .ForMember(dest => dest.IlceId, opt => opt.MapFrom(src => src.IlceId == 0 ? (int?)null : src.IlceId))
                .ForMember(dest => dest.SendikaId, opt => opt.MapFrom(src => src.SendikaId == 0 ? (int?)null : src.SendikaId))
                .ForMember(dest => dest.EsininIsIlId, opt => opt.MapFrom(src => src.EsininIsIlId == 0 ? (int?)null : src.EsininIsIlId))
                .ForMember(dest => dest.EsininIsIlceId, opt => opt.MapFrom(src => src.EsininIsIlceId == 0 ? (int?)null : src.EsininIsIlceId));

            // 3. Mevcut PersonellerViewDto -> PersonellerDto mapping'ini güncelleme
            CreateMap<PersonellerViewDto, PersonellerDto>()
                .ForMember(dest => dest.Il, opt => opt.Ignore())
                .ForMember(dest => dest.Ilce, opt => opt.Ignore())
                .ForMember(dest => dest.Sendika, opt => opt.Ignore())
                .ForMember(dest => dest.EsininIsIl, opt => opt.Ignore())
                .ForMember(dest => dest.EsininIsIlce, opt => opt.Ignore())
                .ForMember(dest => dest.BankolarKullanici, opt => opt.Ignore())
                .ForMember(dest => dest.KanalPersonelleri, opt => opt.Ignore())

                // ÖNEMLİ: Bu alanları ignore etmeyin, mevcut değerleri korunmalı
                .ForMember(dest => dest.PassWord, opt => opt.Ignore())
                .ForMember(dest => dest.SessionID, opt => opt.Ignore())
                .ForMember(dest => dest.EklenmeTarihi, opt => opt.Ignore())

                .ForMember(dest => dest.DuzenlenmeTarihi, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.AtanmaNedeniId, opt => opt.MapFrom(src => src.AtanmaNedeniId == 0 ? 1 : src.AtanmaNedeniId))
                .ForMember(dest => dest.HizmetBinasiId, opt => opt.MapFrom(src => src.HizmetBinasiId == 0 ? 1 : src.HizmetBinasiId));

            CreateMap<KanalPersonelleriDto, KanalPersonelleriViewDto>();

            CreateMap<KanalAltIslemleriDto, KanalAltIslemleriRequestDto>().ReverseMap();
            /*Dto to Dto*/
        }
    }
}