using System;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Providers.CountryServices;
using SportBet.Services.Providers.SportServices;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.Contracts.Encryption;
using SportBet.Services.Providers.AccountServices;
using SportBet.Services.Providers.ParticipantServices;
using SportBet.Services.Providers.TournamentServices;
using SportBet.Services.Providers.EventServices;
using SportBet.Services.Providers.CoefficientServices;

namespace SportBet.Services.Factories
{
    public class AdminServiceFactory : ServiceFactory
    {
        public AdminServiceFactory(string login, string password)
            : base(login, password) { }

        public override IAccountService CreateAccountService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();
            IRegisterValidator registerValidator = CreateRegisterValidator();
            IEncryptor encryptor = CreateEncryptor();
            ISession session = CreateSession();

            return new AdminAccountService(unitOfWork, registerValidator, encryptor, session); 
        }

        public override IBookmakerService CreateBookmakerService()
        {
            throw new NotImplementedException();
        }

        public override IClientService CreateClientService()
        {
            throw new NotImplementedException();
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
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new AdminCountryService(unitOfWork);
        }

        public override ISportService CreateSportService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new AdminSportService(unitOfWork);
        }

        public override IParticipantService CreateParticipantService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new AdminParticipantService(unitOfWork);
        }

        public override ITournamentService CreateTournamentService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new AdminTournamentService(unitOfWork);
        }

        public override IEventService CreateEventService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new AdminEventService(unitOfWork);
        }

        public override ICoefficientService CreateCoefficientService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new AdminCoefficientService(unitOfWork);
        }
    }
}
