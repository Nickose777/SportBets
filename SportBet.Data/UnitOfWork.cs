﻿using SportBet.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportBet.Data.Contracts.Repositories;
using SportBet.Data.Repositories;

namespace SportBet.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SportBetDbContext context;

        private IAccountRepository accounts;
        private IUserRepository users;
        private IRoleRepository roles;
        private IBetRepository bets;
        private IBookmakerRepository bookmakers;
        private IClientRepository clients;
        private ICoefficientRepository coefficients;
        private ICountryRepository countries;
        private IEventRepository events;
        private IParticipantRepository participants;
        private IParticipationRepository participations;
        private ISportRepository sports;
        private ITournamentRepository tournaments;

        public IAccountRepository Accounts
        {
            get { return accounts ?? (accounts = new AccountRepository(context)); }
        }
        public IUserRepository Users
        {
            get { return users ?? (users = new UserRepository(context)); }
        }
        public IRoleRepository Roles
        {
            get { return roles ?? (roles = new RoleRepository(context)); }
        }
        public IBetRepository Bets
        {
            get { return bets ?? (bets = new BetRepository(context)); }
        }
        public IBookmakerRepository Bookmakers
        {
            get { return bookmakers ?? (bookmakers = new BookmakerRepository(context)); }
        }
        public IClientRepository Clients
        {
            get { return clients ?? (clients = new ClientRepository(context)); }
        }
        public ICoefficientRepository Coefficients
        {
            get { return coefficients ?? (coefficients = new CoefficientRepository(context)); }
        }
        public ICountryRepository Countries
        {
            get { return countries ?? (countries = new CountryRepository(context)); }
        }
        public IEventRepository Events
        {
            get { return events ?? (events = new EventRepository(context)); }
        }
        public IParticipantRepository Participants
        {
            get { return participants ?? (participants = new ParticipantRepository(context)); }
        }
        public IParticipationRepository Participations
        {
            get { return participations ?? (participations = new ParticipationRepository(context)); }
        }
        public ISportRepository Sports
        {
            get { return sports ?? (sports = new SportRepository(context)); }
        }
        public ITournamentRepository Tournaments
        {
            get { return tournaments ?? (tournaments = new TournamentRepository(context)); }
        }

        public UnitOfWork()
        {
            this.context = new SportBetDbContext();
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
