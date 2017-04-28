using AutoMapper;
using SportBet.Models.Base;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;

namespace SportBet.Mappings
{
    class TournamentProfile : Profile
    {
        public TournamentProfile()
        {
            CreateMap<TournamentBaseDTO, TournamentBaseModel>();
            CreateMap<TournamentBaseModel, TournamentBaseDTO>();

            CreateMap<TournamentDisplayDTO, TournamentDisplayModel>();
            CreateMap<TournamentDisplayModel, TournamentBaseDTO>();

            CreateMap<TournamentEditModel, TournamentEditDTO>();

            CreateMap<TournamentDisplayDTO, TournamentBaseModel>();
        }
    }
}
