using AutoMapper;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Models.Registers;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.DTOModels.Register;

namespace SportBet.Mappings
{
    class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<AdminRegisterModel, AdminRegisterDTO>();

            CreateMap<AdminEditModel, AdminEditDTO>();

            CreateMap<AdminDisplayDTO, AdminDisplayModel>().ReverseMap();
        }
    }
}
