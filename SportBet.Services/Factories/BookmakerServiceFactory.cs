using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Factories;
using SportBet.Services.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Factories
{
    class BookmakerServiceFactory : ServiceFactory
    {
        public BookmakerServiceFactory(string login, string password)
            : base(login, password) { }

        public override IAccountService CreateAccountService()
        {
            //TODO create BookmakerAccountService
            throw new NotImplementedException();
        }
    }
}
