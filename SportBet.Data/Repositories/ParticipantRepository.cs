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

        public bool Exists(string participantName, int sportId, int countryId)
        {
            SportBetDbContext context = GetContext();

            ParticipantEntity participantEntity = context
                .Participants
                .SingleOrDefault(participant => 
                    participant.CountryId == countryId &&
                    participant.SportId == sportId &&
                    participant.Name == participantName);

            return participantEntity != null;
        }
    }
}
