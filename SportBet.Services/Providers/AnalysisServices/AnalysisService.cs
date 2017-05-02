using System;
using System.Collections.Generic;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;

namespace SportBet.Services.Providers.AnalysisServices
{
    class AnalysisService : IAnalysisService
    {
        private readonly IUnitOfWork unitOfWork;

        public AnalysisService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public DataServiceMessage<IncomeDTO> GetIncome(DateTime startDate, DateTime endDate)
        {
            string message = "";
            bool success = true;
            IncomeDTO income = new IncomeDTO();

            try
            {
                IEnumerable<BetEntity> betEntities = unitOfWork
                    .Bets
                    .GetAll(b => 
                        startDate.Date < b.RegistrationDate &&
                        b.RegistrationDate < endDate.Date);

                decimal win = betEntities
                    .Where(b => b.Coefficient.Win.HasValue && b.Coefficient.Win.Value)
                    .Select(b => b.Sum * b.Coefficient.Value)
                    .Sum();
                decimal lost = betEntities
                    .Where(b => b.Coefficient.Win.HasValue && !b.Coefficient.Win.Value)
                    .Select(b => b.Sum)
                    .Sum();

                income.WonBets = betEntities.Count(b => b.Coefficient.Win.HasValue && b.Coefficient.Win.Value);
                income.LostBets = betEntities.Count(b => b.Coefficient.Win.HasValue && !b.Coefficient.Win.Value);

                income.Profits = lost;
                income.Costs = win;
                income.Income = lost - win;

                message = "Analyzed income";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IncomeDTO>(income, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
