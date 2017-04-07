using System;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;
using System.Collections.Generic;

namespace SportBet.Services.Contracts.Services
{
    public interface IAdminService : IDisposable
    {
        ServiceMessage Delete(AdminDisplayDTO adminDisplayDTO);

        DataServiceMessage<IEnumerable<AdminDisplayDTO>> GetAll();
    }
}
