using AutoMapper;
using SportBet.Models.Create;
using SportBet.Services.DTOModels.Create;

namespace SportBet.Mappings
{
    class BetProfile : Profile
    {
        public BetProfile()
        {
            CreateMap<BetCreateModel, BetCreateDTO>();
        }
    }
}
