using AutoMapper;
using AutoMapper.Configuration;
using SportBet.Models;
using SportBet.Models.Registers;
using SportBet.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Mappings
{
    class ModelsToDTOs : Profile
    {
        public ModelsToDTOs()
        {
            CreateMap<BookmakerRegisterModel, BookmakerRegisterDTO>();
            CreateMap<ClientRegisterModel, ClientRegisterDTO>();
        }
    }
}
