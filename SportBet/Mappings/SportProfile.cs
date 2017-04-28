using AutoMapper;
using SportBet.Models.Create;
using SportBet.Models.Edit;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Edit;

namespace SportBet.Mappings
{
    class SportProfile : Profile
    {
        public SportProfile()
        {
            CreateMap<SportCreateModel, SportCreateDTO>();

            CreateMap<SportEditDTO, SportEditModel>().ReverseMap();
        }
    }
}
