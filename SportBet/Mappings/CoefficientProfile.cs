using AutoMapper;
using SportBet.Models.Base;
using SportBet.Models.Create;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;

namespace SportBet.Mappings
{
    class CoefficientProfile : Profile
    {
        public CoefficientProfile()
        {
            CreateMap<CoefficientBaseModel, CoefficientBaseDTO>().ReverseMap();

            CreateMap<CoefficientCreateModel, CoefficientCreateDTO>();

            CreateMap<CoefficientDisplayDTO, CoefficientDisplayModel>();

            CreateMap<CoefficientEditModel, CoefficientEditDTO>()
                .ForMember(dest => dest.Win, conf => conf.MapFrom(src => src.NewWin));
        }
    }
}
