using System;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminPassword AdminPassword { get; }

        IUserRepository Users { get; }

        IRoleRepository Roles { get; }

        IAnalyticRepository Analytics { get; }

        IAdminRepository Admins { get; }

        IAccountRepository Accounts { get; }

        IBetRepository Bets { get; }

        IBookmakerRepository Bookmakers { get; }

        IClientRepository Clients { get; }

        ICoefficientRepository Coefficients { get; }

        ICountryRepository Countries { get; }

        IEventRepository Events { get; }

        IParticipantRepository Participants { get; }

        IParticipationRepository Participations { get; }

        ISportRepository Sports { get; }

        ITournamentRepository Tournaments { get; }

        void Reconnect(string login, string password);

        void Commit();
    }
}
