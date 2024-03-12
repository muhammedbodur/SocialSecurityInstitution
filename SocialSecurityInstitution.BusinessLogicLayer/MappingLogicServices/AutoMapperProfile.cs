using AutoMapper;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;

namespace SocialSecurityInstitution.BusinessLogicLayer.MappingLogicServices
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AtanmaNedenleri, AtanmaNedenleriDto>().ReverseMap();
            CreateMap<PersonelCocuklari, PersonelCocuklariDto>().ReverseMap();
        }
    }
}
