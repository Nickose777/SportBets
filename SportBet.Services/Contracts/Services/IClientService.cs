﻿using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface IClientService : IDisposable
    {
        ServiceMessage Update(ClientEditDTO clientEditDTO);

        ServiceMessage Delete(string login);

        DataServiceMessage<ClientEditDTO> GetAuthorizedClientInfo();

        DataServiceMessage<ClientEditDTO> GetClientInfo(string login);

        DataServiceMessage<IEnumerable<ClientDisplayDTO>> GetAll();
    }
}
