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
    class BetProfile : Profile
    {
        public BetProfile()
        {
            CreateMap<BetBaseDTO, BetBaseModel>().ReverseMap();

            CreateMap<BetCreateModel, BetCreateDTO>();

            CreateMap<BetDisplayDTO, BetDisplayModel>();

            CreateMap<BetEditModel, BetEditDTO>()
                .ForMember(dest => dest.Sum, conf => conf.MapFrom(src => src.NewSum));
        }
    }
}
