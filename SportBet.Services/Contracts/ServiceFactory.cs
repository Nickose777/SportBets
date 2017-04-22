using SportBet.Data;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Encryption;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.Encryption;
using SportBet.Services.Providers;
using SportBet.Services.Validators;

namespace SportBet.Services.Contracts
{
    public abstract class ServiceFactory
    {
        protected readonly string login;
        protected readonly string password;

        public ServiceFactory(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public abstract IAccountService CreateAccountService();

        public abstract IAdminService CreateAdminService();

        public abstract IAnalyticService CreateAnalyticService();

        public abstract IBookmakerService CreateBookmakerService();

        public abstract IClientService CreateClientService();

        public abstract ICountryService CreateCountryService();

        public abstract ISportService CreateSportService();

        public abstract IParticipantService CreateParticipantService();

        public abstract ITournamentService CreateTournamentService();

        public abstract IEventService CreateEventService();

        public abstract ICoefficientService CreateCoefficientService();

        protected IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(login, password);
        }

        protected IRegisterValidator CreateRegisterValidator()
        {
            return new RegisterValidator();
        }

        protected IEncryptor CreateEncryptor()
        {
            return new Encryptor();
        }

        protected ISession CreateSession()
        {
            return new SessionProvider();
        }
    }
}
