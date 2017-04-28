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
    class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventBaseModel, EventBaseDTO>();
            CreateMap<EventBaseDTO, EventBaseModel>();

            CreateMap<EventCreateModel, EventCreateDTO>();

            CreateMap<EventDisplayDTO, EventDisplayModel>();

            CreateMap<EventEditModel, EventEditDTO>();
        }
    }
}
