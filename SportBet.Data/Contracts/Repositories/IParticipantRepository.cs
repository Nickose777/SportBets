using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface IParticipantRepository : IRepository<ParticipantEntity>
    {
        bool Exists(string participantName, int sportId, int countryId);

        ParticipantEntity Get(string participantName, int sportId, int countryId);

        ParticipantEntity Get(string participantName, string sportName, string countryName);
    }
}
