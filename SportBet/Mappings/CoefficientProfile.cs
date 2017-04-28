using AutoMapper;
using SportBet.Models.Create;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;

namespace SportBet.Mappings
{
    class CoefficientProfile : Profile
    {
        public CoefficientProfile()
        {
            CreateMap<CoefficientCreateModel, CoefficientCreateDTO>();

            CreateMap<CoefficientDisplayDTO, CoefficientDisplayModel>();

            CreateMap<CoefficientEditModel, CoefficientEditDTO>();
        }
    }
}
