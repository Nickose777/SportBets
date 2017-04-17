using SportBet.Contracts.Controllers;
using SportBet.Controllers;
using SportBet.Facades;
using SportBet.Services.Contracts;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return new AccountController(factory, login);
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

            switch (loginType)
            {
                case LoginType.Superuser:
                    controller = new SuperuserClientController(factory, new ClientFacade(factory));
                    break;
                case LoginType.Bookmaker:
                    controller = new BookmakerClientController(factory, new ClientFacade(factory));
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
    }
}
