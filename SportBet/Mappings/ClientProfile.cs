using AutoMapper;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Models.Registers;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.DTOModels.Register;

namespace SportBet.Mappings
{
    class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientRegisterModel, ClientRegisterDTO>();

            CreateMap<ClientEditDTO, ClientEditModel>();
            CreateMap<ClientEditModel, ClientEditDTO>();

            CreateMap<ClientDisplayDTO, ClientDisplayModel>();
            CreateMap<ClientDisplayModel, ClientDisplayDTO>();
        }
    }
}
