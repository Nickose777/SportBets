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

        public DataServiceMessage<IEnumerable<SportRatingDTO>> GetSportRating()
        {
            string message = "";
            bool success = true;
            IEnumerable<SportRatingDTO> sportRating = null;

            try
            {
                sportRating = unitOfWork
                    .Sports.GetAll().Select(sport =>
                    {
                        var betEntities = sport
                                .Tournaments
                                .SelectMany(t => t.Events)
                                .SelectMany(e => e.Coefficients)
                                .SelectMany(c => c.Bets);

                        decimal win = betEntities
                            .Where(b => b.Coefficient.Win.HasValue && b.Coefficient.Win.Value)
                            .Select(b => b.Sum * b.Coefficient.Value)
                            .Sum();
                        decimal lost = betEntities
                            .Where(b => b.Coefficient.Win.HasValue && !b.Coefficient.Win.Value)
                            .Select(b => b.Sum)
                            .Sum();

                        return new SportRatingDTO
                        {
                            SportName = sport.Type,
                            BetsCount = betEntities.Count(),
                            Profits = lost,
                            Costs = win,
                            Income = lost - win
                        };
                    })
                    .OrderBy(sport => sport.SportName)
                    .ToList();

                message = "Gor sport rating";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<SportRatingDTO>>(sportRating, message, success);
        }

        public DataServiceMessage<IEnumerable<BookmakerAnalysisDTO>> GetBookmakerAnalysis()
        {
            string message = "";
            bool success = true;
            IEnumerable<BookmakerAnalysisDTO> bookmakerAnalysis = null;

            try
            {
                bookmakerAnalysis = unitOfWork
                    .Bookmakers
                    .GetAll(b => !b.IsDeleted)
                    .Select(bookmaker =>
                    {
                        IEnumerable<BetEntity> betEntities = bookmaker.Bets;

                        decimal win = betEntities
                            .Where(b => b.Coefficient.Win.HasValue && b.Coefficient.Win.Value)
                            .Select(b => b.Sum * b.Coefficient.Value)
                            .Sum();
                        decimal lost = betEntities
                            .Where(b => b.Coefficient.Win.HasValue && !b.Coefficient.Win.Value)
                            .Select(b => b.Sum)
                            .Sum();

                        return new BookmakerAnalysisDTO
                        {
                            FirstName = bookmaker.FirstName,
                            LastName = bookmaker.LastName,
                            PhoneNumber = bookmaker.PhoneNumber,
                            Profits = lost,
                            Costs = win,
                            Income = lost - win
                        };
                    })
                    .OrderBy(b => b.LastName)
                    .ThenBy(b => b.FirstName)
                    .ToList();

                message = "Got bookmaker analysis";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<BookmakerAnalysisDTO>>(bookmakerAnalysis, message, success);
        }

        public DataServiceMessage<IEnumerable<ClientAnalysisDTO>> GetClientAnalysis()
        {
            string message = "";
            bool success = true;
            IEnumerable<ClientAnalysisDTO> clientAnalysis = null;

            try
            {
                clientAnalysis = unitOfWork
                    .Clients
                    .GetAll(c => !c.IsDeleted)
                    .Select(client =>
                    {
                        IEnumerable<BetEntity> betEntities = client.Bets;

                        decimal win = betEntities
                            .Where(b => b.Coefficient.Win.HasValue && b.Coefficient.Win.Value)
                            .Select(b => b.Sum * b.Coefficient.Value)
                            .Sum();
                        decimal lost = betEntities
                            .Where(b => b.Coefficient.Win.HasValue && !b.Coefficient.Win.Value)
                            .Select(b => b.Sum)
                            .Sum();

                        return new ClientAnalysisDTO
                        {
                            FirstName = client.FirstName,
                            LastName = client.LastName,
                            PhoneNumber = client.PhoneNumber,
                            Profits = lost,
                            Costs = win,
                            Income = lost - win
                        };
                    })
                    .OrderBy(b => b.LastName)
                    .ThenBy(b => b.FirstName)
                    .ToList();

                message = "Got client analysis";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<ClientAnalysisDTO>>(clientAnalysis, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
