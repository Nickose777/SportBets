namespace SportBet.Core.Entities
{
    public class ParticipationEntity
    {
        public int EventId { get; set; }
        public virtual EventEntity Event { get; set; }

        public int ParticipantId { get; set; }
        public virtual ParticipantEntity Participant { get; set; }

        public short? Position { get; set; }
    }
}
