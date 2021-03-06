﻿using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;
using System.Collections.Generic;

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

        public bool IsBusyOn(int participantId, DateTime date)
        {
            var eventsOfParticipant = GetParticipantEvents(participantId);
            return eventsOfParticipant.Count(e => e.DateOfEvent == date) > 0;
        }

        private IEnumerable<EventEntity> GetParticipantEvents(int participantId)
        {
            SportBetDbContext context = GetContext();

            ParticipantEntity participantEntity = context.Participants.Find(participantId);
            var eventsOfParticipant = participantEntity.Participations.Select(part => part.Event);

            return eventsOfParticipant;
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


        public IEnumerable<ParticipantEntity> GetByEvent(int eventId)
        {
            SportBetDbContext context = GetContext();

            IEnumerable<ParticipantEntity> participantEntities = context
                .Participants
                .Where(p => p.Participations.Select(part => part.EventId).Contains(eventId))
                .ToList();

            return participantEntities;
        }
    }
}
