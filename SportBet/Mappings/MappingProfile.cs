using AutoMapper;
using AutoMapper.Configuration;
using SportBet.Models;
using SportBet.Models.Display;
using SportBet.Models.Registers;
using SportBet.Services.DTOModels;
using SportBet.Services.DTOModels.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            CreateMap<BookmakerDisplayModel, BookmakerDisplayDTO>();
            CreateMap<BookmakerDisplayDTO, BookmakerDisplayModel>();

            CreateMap<ClientDisplayModel, ClientDisplayDTO>();
            CreateMap<ClientDisplayDTO, ClientDisplayModel>();
        }
    }
}
