using AutoMapper;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Models.Registers;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.DTOModels.Register;

namespace SportBet.Mappings
{
    class BookmakerProfile : Profile
    {
        public BookmakerProfile()
        {
            CreateMap<BookmakerRegisterModel, BookmakerRegisterDTO>();

            CreateMap<BookmakerDisplayModel, BookmakerDisplayDTO>().ReverseMap();

            CreateMap<BookmakerDisplayModel, BookmakerEditModel>();

            CreateMap<BookmakerEditModel, BookmakerEditDTO>();
        }
    }
}
