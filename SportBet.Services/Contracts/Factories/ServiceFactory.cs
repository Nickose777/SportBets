using SportBet.Services.Contracts.Services;
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
    }
}
