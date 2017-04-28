using AutoMapper;
using SportBet.Models.Edit;
using SportBet.Services.DTOModels.Edit;

namespace SportBet.Mappings
{
    class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryEditDTO, CountryEditModel>().ReverseMap();
        }
    }
}
