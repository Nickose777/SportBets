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

        public ParticipantEntity Get(string participantName, int sportId, int countryId)
        {
            SportBetDbContext context = GetContext();

            ParticipantEntity participantEntity = context
                .Participants
                .SingleOrDefault(participant =>
                    participant.CountryId == countryId &&
                    participant.SportId == sportId &&
                    participant.Name == participantName);

            return participantEntity;
        }


        public ParticipantEntity Get(string participantName, string sportName, string countryName)
        {
            SportBetDbContext context = GetContext();

            ParticipantEntity participantEntity = context
                .Participants
                .SingleOrDefault(participant =>
                    participant.Country.Name == countryName &&
                    participant.Sport.Type == sportName &&
                    participant.Name == participantName);

            return participantEntity;
        }
    }
}
