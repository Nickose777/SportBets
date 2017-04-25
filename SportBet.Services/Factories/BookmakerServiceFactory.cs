using System;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Encryption;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.Providers.AccountServices;
using SportBet.Services.Providers.ClientServices;
using SportBet.Services.Providers.BetServices;
using SportBet.Services.Providers.BookmakerServices;
using SportBet.Services.Providers.SportServices;
using SportBet.Services.Providers.TournamentServices;
using SportBet.Services.Providers.EventServices;
using SportBet.Services.Providers.CoefficientServices;

namespace SportBet.Services.Factories
{
    public class BookmakerServiceFactory : ServiceFactory
    {
        public BookmakerServiceFactory(string login, string password)
            : base(login, password) { }

        public override IAccountService CreateAccountService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();
            IRegisterValidator registerValidator = CreateRegisterValidator();
            IEncryptor encryptor = CreateEncryptor();
            ISession session = CreateSession();

            return new BookmakerAccountService(unitOfWork, registerValidator, encryptor, session);
        }

        public override IBookmakerService CreateBookmakerService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();
            ISession session = CreateSession();

            return new BookmakerBookmakerService(unitOfWork, session);
        }

        public override IClientService CreateClientService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new BookmakerClientService(unitOfWork);
        }

        public override IAdminService CreateAdminService()
        {
            throw new NotImplementedException();
        }

        public override IAnalyticService CreateAnalyticService()
        {
            throw new NotImplementedException();
        }

        public override ICountryService CreateCountryService()
        {
            throw new NotImplementedException();
        }

        public override ISportService CreateSportService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new BookmakerSportService(unitOfWork);
        }

        public override IParticipantService CreateParticipantService()
        {
            throw new NotImplementedException();
        }

        public override ITournamentService CreateTournamentService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new BookmakerTournamentService(unitOfWork);
        }

        public override IEventService CreateEventService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new BookmakerEventService(unitOfWork);
        }

        public override ICoefficientService CreateCoefficientService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new BookmakerCoefficientService(unitOfWork);
        }

        public override IBetService CreateBetService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new BookmakerBetService(unitOfWork);
        }
    }
}
