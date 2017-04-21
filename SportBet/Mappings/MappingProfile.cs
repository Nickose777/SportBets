using AutoMapper;
using SportBet.Models.Base;
using SportBet.Models.Create;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Models.Extra;
using SportBet.Models.Registers;
using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.DTOModels.Extra;
using SportBet.Services.DTOModels.Register;

namespace SportBet.Mappings
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookmakerRegisterModel, BookmakerRegisterDTO>();
            CreateMap<ClientRegisterModel, ClientRegisterDTO>();
            CreateMap<AdminRegisterModel, AdminRegisterDTO>();
            CreateMap<AnalyticRegisterModel, AnalyticRegisterDTO>();

            CreateMap<ClientEditDTO, ClientEditModel>();
            CreateMap<ClientEditModel, ClientEditDTO>();

            CreateMap<BookmakerDisplayModel, BookmakerDisplayDTO>();
            CreateMap<BookmakerDisplayDTO, BookmakerDisplayModel>();

            CreateMap<ClientDisplayModel, ClientDisplayDTO>();
            CreateMap<ClientDisplayDTO, ClientDisplayModel>();

            CreateMap<AdminDisplayModel, AdminDisplayDTO>();
            CreateMap<AdminDisplayDTO, AdminDisplayModel>();

            CreateMap<AnalyticDisplayModel, AnalyticDisplayDTO>();
            CreateMap<AnalyticDisplayDTO, AnalyticDisplayModel>();

            CreateMap<CountryEditDTO, CountryEditModel>();
            CreateMap<CountryEditModel, CountryEditDTO>();

            CreateMap<SportEditDTO, SportEditModel>();
            CreateMap<SportEditModel, SportEditDTO>();

            CreateMap<SportCreateModel, SportCreateDTO>();

            CreateMap<ParticipantBaseModel, ParticipantBaseDTO>();
            CreateMap<ParticipantBaseDTO, ParticipantDisplayModel>();
            CreateMap<ParticipantBaseDTO, ParticipantBaseModel>();

            CreateMap<TournamentBaseDTO, TournamentBaseModel>();
            CreateMap<TournamentBaseModel, TournamentBaseDTO>();
            CreateMap<TournamentDisplayDTO, TournamentDisplayModel>();
            CreateMap<TournamentDisplayModel, TournamentBaseDTO>();

            CreateMap<BookmakerDisplayModel, BookmakerEditModel>();
            CreateMap<BookmakerEditModel, BookmakerEditDTO>();

            CreateMap<ParticipantEditModel, ParticipantEditDTO>();

            CreateMap<TournamentEditModel, TournamentEditDTO>();

            CreateMap<EventBaseModel, EventBaseDTO>();
            CreateMap<EventCreateModel, EventCreateDTO>();

            CreateMap<ParticipantTournamentDTO, ParticipantTournamentModel>();
            CreateMap<TournamentDisplayDTO, TournamentBaseModel>();
        }
    }
}
