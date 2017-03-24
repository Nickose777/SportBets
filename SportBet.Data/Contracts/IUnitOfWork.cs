using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Contracts
{
    public interface IUnitOfWork
    {
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

        void Commit();
    }
}
