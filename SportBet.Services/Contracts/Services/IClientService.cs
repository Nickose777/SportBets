﻿using SportBet.Services.DTOModels;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Contracts.Services
{
    public interface IClientService : IDisposable
    {
        ServiceMessage Delete(ClientDisplayDTO clientDisplayDTO);

        DataServiceMessage<IEnumerable<ClientDisplayDTO>> GetAll();
    }
}