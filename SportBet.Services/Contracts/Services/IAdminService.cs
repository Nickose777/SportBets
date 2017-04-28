using System;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Edit;

namespace SportBet.Services.Contracts.Services
{
    public interface IAdminService : IDisposable
    {
        ServiceMessage Update(AdminEditDTO adminEditDTO);

        ServiceMessage Delete(string login);

        DataServiceMessage<IEnumerable<AdminDisplayDTO>> GetAll();
    }
}
