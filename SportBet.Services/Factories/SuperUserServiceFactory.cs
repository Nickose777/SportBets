using SportBet.Data;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Factories;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.Providers;
using SportBet.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Factories
{
    class SuperuserServiceFactory : ServiceFactory
    {
        public SuperuserServiceFactory(string login, string password)
            : base(login, password) { }

        public override IAccountService CreateAccountService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();
            IRegisterValidator registerValidator = CreateRegisterValidator();

            return new SuperuserAccountService(unitOfWork, registerValidator);
        }

        private IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(login, password);
        }
        private IRegisterValidator CreateRegisterValidator()
        {
            return new RegisterValidator();
        }
    }
}
