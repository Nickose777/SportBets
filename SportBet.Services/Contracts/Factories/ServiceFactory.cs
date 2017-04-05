using SportBet.Data;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.Encryption;
using SportBet.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Contracts.Factories
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
    }
}
