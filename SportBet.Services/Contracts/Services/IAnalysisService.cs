using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Contracts.Services
{
    public interface IAnalysisService : IDisposable
    {
        DataServiceMessage<IncomeDTO> GetIncome(DateTime startDate, DateTime endDate);

        DataServiceMessage<IEnumerable<SportRatingDTO>> GetSportRating();
    }
}
