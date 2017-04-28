using AutoMapper;
using SportBet.Models.Base;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Models.Extra;
using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.DTOModels.Extra;

namespace SportBet.Mappings
{
    class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            CreateMap<ParticipantBaseModel, ParticipantBaseDTO>().ReverseMap();

            CreateMap<ParticipantBaseDTO, ParticipantDisplayModel>();

            CreateMap<ParticipantEditModel, ParticipantEditDTO>();

            CreateMap<ParticipantTournamentDTO, ParticipantTournamentModel>();
        }
    }
}
