using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class ParticipantRepository : RepositoryBase<ParticipantEntity>, IParticipantRepository
    {
        public ParticipantRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public bool Exists(string participantName, string sportName, string countryName)
        {
            SportBetDbContext context = GetContext();

            ParticipantEntity participantEntity = context
                .Participants
                .SingleOrDefault(
                    participant => participant.Country.Name == countryName &&
                    participant.Sport.Type == sportName &&
                    participant.Name == participantName);

            return participantEntity != null;
        }
    }
}
