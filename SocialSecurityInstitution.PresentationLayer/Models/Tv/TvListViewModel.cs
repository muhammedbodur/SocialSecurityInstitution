using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;

namespace SocialSecurityInstitution.PresentationLayer.Models.Tv
{
    public class TvListViewModel
    {
        public List<SiralarTvListeDto>? SiralarTvListe { get; set; }
        public HizmetBinalariDto? HizmetBinasi { get; set; }
        public TvlerDto Tvler { get; set; }
    }
}