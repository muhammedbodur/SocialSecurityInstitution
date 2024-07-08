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
            CreateMap<AtanmaNedenleri, KanallarDto>().ReverseMap();
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

            /*Dto to Dto*/
            CreateMap<LoginDto, LoginLogoutLogDto>()
            .ForMember(dest => dest.TcKimlikNo, opt => opt.MapFrom(src => src.TcKimlikNo))
            .ForMember(dest => dest.LoginTime, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.LogoutTime, opt => opt.MapFrom(src => DateTime.MinValue));
            CreateMap<BankolarDto, BankolarRequestDto>().ReverseMap();
            CreateMap<HizmetBinalariDepartmanlarDto, BankolarHizmetBinalariDepartmanlarDto>();
            CreateMap<PersonellerDto, PersonelRequestDto>().ReverseMap();
            CreateMap<KanalIslemleriDto, KanalIslemleriRequestDto>().ReverseMap();
            CreateMap<PersonellerDto, PersonellerViewDto>();
            CreateMap<KanalPersonelleriDto, KanalPersonelleriViewDto>();
            CreateMap<KanalPersonelleriDto, KanalPersonelleriViewDto>();
            /*Dto to Dto*/
        }
    }
}
