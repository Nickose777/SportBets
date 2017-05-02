using SportBet.Contracts.Controllers;
using SportBet.Controllers;
using SportBet.Facades;
using SportBet.Services.Contracts;
using SportBet.Services.ResultTypes;

namespace SportBet.ControllerFactories
{
    public class ControllerFactory
    {
        private readonly ServiceFactory factory;
        private readonly LoginType loginType;
        private readonly string login;

        public ControllerFactory(ServiceFactory factory, LoginType loginType, string login)
        {
            this.factory = factory;
            this.loginType = loginType;
            this.login = login;
        }

        public IAccountController CreateAccountController()
        {
            IAccountController controller = null;

            switch (loginType)
            {
                case LoginType.Client:
                    controller = new ClientAccountController(factory, login);
                    break;
                case LoginType.Bookmaker:
                case LoginType.Superuser:
                    controller = new BookmakerAccountController(factory, login);
                    break;
            }
            return controller;
        }

        public IAdminController CreateAdminController()
        {
            return new AdminController(factory, new AdminFacade(factory));
        }

        public IAnalyticController CreateAnalyticController()
        {
            return new AnalyticController(factory, new AnalyticFacade(factory));
        }

        public IBookmakerController CreateBookmakerController()
        {
            return new BookmakerController(factory, new BookmakerFacade(factory));
        }

        public IClientController CreateClientController()
        {
            IClientController controller = null;
            ClientFacade facade = new ClientFacade(factory);

            switch (loginType)
            {
                case LoginType.Superuser:
                    controller = new SuperuserClientController(factory, facade);
                    break;
                case LoginType.Bookmaker:
                    controller = new BookmakerClientController(factory, facade);
                    break;
                default:
                    break;
            }

            return controller;
        }

        public ICountryController CreateCountryController()
        {
            return new CountryController(factory, new CountryFacade(factory));
        }

        public ISportController CreateSportController()
        {
            return new SportController(factory, new SportFacade(factory));
        }

        public IParticipantController CreateParticipantController()
        {
            return new ParticipantController(factory, new ParticipantFacade(factory));
        }

        public ITournamentController CreateTournamentController()
        {
            return new TournamentController(factory, new TournamentFacade(factory));
        }

        public IEventController CreateEventController()
        {
            return new EventController(factory, new EventFacade(factory));
        }

        public ICoefficientController CreateCoefficientController()
        {
            return new CoefficientController(factory, new CoefficientFacade(factory));
        }

        public IBetController CreateBetController()
        {
            BetControllerBase controller = null;
            BetFacade facade = new BetFacade(factory);

            switch (loginType)
            {
                case LoginType.Client:
                    controller = new ClientBetController(factory, facade);
                    break;
                case LoginType.Bookmaker:
                    controller = new BookmakerBetController(factory, facade);
                    break;
                case LoginType.Superuser:
                    controller = new SuperuserBetController(factory, facade);
                    break;
                default:
                    break;
            }

            return controller;
        }
    }
}
