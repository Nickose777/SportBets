using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface IParticipantRepository : IRepository<ParticipantEntity>
    {
        bool Exists(string participantName, string sportName, string countryName);
    }
}
