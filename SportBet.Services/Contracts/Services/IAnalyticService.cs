using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface IAnalyticService : IDisposable
    {
        ServiceMessage Delete(string login);

        DataServiceMessage<IEnumerable<AnalyticDisplayDTO>> GetAll();
    }
}
