﻿using System;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Encryption;
using SportBet.Services.Contracts.Services;
using SportBet.Services.Providers.AccountServices;
using SportBet.Services.Providers.ClientServices;
using SportBet.Services.Contracts.Validators;
using SportBet.Services.Providers.BetServices;

namespace SportBet.Services.Factories
{
    public class ClientServiceFactory : ServiceFactory
    {
        public ClientServiceFactory(string login, string password)
            : base(login, password) { }

        public override IAccountService CreateAccountService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();
            IRegisterValidator registerValidator = CreateRegisterValidator();
            IEncryptor encryptor = CreateEncryptor();
            ISession session = CreateSession();

            return new ClientAccountService(unitOfWork, registerValidator, encryptor, session);
        }

        public override IBookmakerService CreateBookmakerService()
        {
            throw new NotImplementedException();
        }

        public override IClientService CreateClientService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();
            ISession session = CreateSession();

            return new ClientClientService(unitOfWork, session);
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

        public override IParticipantService CreateParticipantService()
        {
            throw new NotImplementedException();
        }

        public override ITournamentService CreateTournamentService()
        {
            throw new NotImplementedException();
        }

        public override IEventService CreateEventService()
        {
            throw new NotImplementedException();
        }

        public override ICoefficientService CreateCoefficientService()
        {
            throw new NotImplementedException();
        }

        public override IBetService CreateBetService()
        {
            IUnitOfWork unitOfWork = CreateUnitOfWork();
            ISession session = CreateSession();

            return new ClientBetService(unitOfWork, session);
        }

        public override IAnalysisService CreateAnalysisService()
        {
            throw new NotImplementedException();
        }
    }
}
