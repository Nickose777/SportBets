using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Factories;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Providers.AccountServices;
using SportBet.Services.Providers.ClientServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Factories
{
    public class ClientServiceFactory : ServiceFactory
    {
        public ClientServiceFactory(string login, string password)
            : base(login, password) { }

        public override IAccountService CreateAccountService()
        {
            return new ClientAccountService();
        }

        public override IBookmakerService CreateBookmakerService()
        {
            throw new NotImplementedException();
        }

        public override IClientService CreateClientService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new ClientClientService(unitOfWork);
        }

        public override IAdminService CreateAdminService()
        {
            throw new NotImplementedException();
        }

        public override IAnalyticService CreateAnalyticService()
        {
            throw new NotImplementedException();
        }
    }
}
