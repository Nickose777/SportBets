using System;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Encryption;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.Providers.AccountServices;

namespace SportBet.Services.Factories
{
    public class AnalyticServiceFactory : ServiceFactory
    {
        public AnalyticServiceFactory(string login, string password)
            : base(login, password) { }

        public override IAccountService CreateAccountService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();
            IRegisterValidator registerValidator = CreateRegisterValidator();
            IEncryptor encryptor = CreateEncryptor();
            ISession session = CreateSession();

            return new AnalyticAccountService(unitOfWork, registerValidator, encryptor, session);
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
            throw new NotImplementedException();
        }

        public override ISportService CreateSportService()
        {
            throw new NotImplementedException();
        }
    }
}
