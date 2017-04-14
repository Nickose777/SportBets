﻿using System;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Providers.CountryServices;
using SportBet.Services.Providers.SportServices;

namespace SportBet.Services.Factories
{
    public class AdminServiceFactory : ServiceFactory
    {
        public AdminServiceFactory(string login, string password)
            : base(login, password) { }

        public override IAccountService CreateAccountService()
        {
            throw new NotImplementedException();
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
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new AdminCountryService(unitOfWork);
        }

        public override ISportService CreateSportService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();

            return new AdminSportService(unitOfWork);
        }
    }
}
