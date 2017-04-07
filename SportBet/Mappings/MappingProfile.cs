using AutoMapper;
using AutoMapper.Configuration;
using SportBet.Models;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Models.Registers;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
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
        }
    }
}
