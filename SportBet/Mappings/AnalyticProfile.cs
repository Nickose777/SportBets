using AutoMapper;
using SportBet.Models.Display;
using SportBet.Models.Registers;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Register;

namespace SportBet.Mappings
{
    class AnalyticProfile : Profile
    {
        public AnalyticProfile()
        {
            CreateMap<AnalyticRegisterModel, AnalyticRegisterDTO>();

            CreateMap<AnalyticDisplayDTO, AnalyticDisplayModel>();
            CreateMap<AnalyticDisplayModel, AnalyticDisplayDTO>();
        }
    }
}
