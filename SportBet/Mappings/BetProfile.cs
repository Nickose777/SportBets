using AutoMapper;
using SportBet.Models.Create;
using SportBet.Models.Display;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;

namespace SportBet.Mappings
{
    class BetProfile : Profile
    {
        public BetProfile()
        {
            CreateMap<BetCreateModel, BetCreateDTO>();

            CreateMap<BetDisplayDTO, BetDisplayModel>();
        }
    }
}
