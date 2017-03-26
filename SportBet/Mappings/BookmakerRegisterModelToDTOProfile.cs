using AutoMapper;
using AutoMapper.Configuration;
using SportBet.Models;
using SportBet.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Mappings
{
    class BookmakerRegisterModelToDTOProfile : Profile
    {
        public BookmakerRegisterModelToDTOProfile()
        {
            CreateMap<BookmakerRegisterModel, BookmakerRegisterDTO>();
        }
    }
}
