using SportBet.Data;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Factories;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.Encryption;
using SportBet.Services.Providers.AccountServices;
using SportBet.Services.Providers.AdminServices;
using SportBet.Services.Providers.BookmakerServices;
using SportBet.Services.Providers.ClientServices;
using SportBet.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Factories
{
    public class SuperuserServiceFactory : ServiceFactory
    {
        public SuperuserServiceFactory(string login, string password)
            : base(login, password) { }

        public override IAccountService CreateAccountService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();
            IRegisterValidator registerValidator = CreateRegisterValidator();
            IEncryptor encryptor = CreateEncryptor();

            return new SuperuserAccountService(unitOfWork, registerValidator, encryptor);
        }

        public override IBookmakerService CreateBookmakerService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new SuperuserBookmakerService(unitOfWork);
        }

        public override IClientService CreateClientService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new SuperuserClientService(unitOfWork);
        }

        public override IAdminService CreateAdminService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new SuperuserAdminService(unitOfWork);
        }
    }
}
