using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Edit;

namespace SportBet.Services.Contracts.Services
{
    public interface IAnalyticService : IDisposable
    {
        ServiceMessage Update(AnalyticEditDTO analyticEditDTO);

        ServiceMessage Delete(string login);

        DataServiceMessage<IEnumerable<AnalyticDisplayDTO>> GetAll();
    }
}
