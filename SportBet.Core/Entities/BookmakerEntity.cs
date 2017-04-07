using System.Collections.Generic;

namespace SportBet.Core.Entities
{
    public class BookmakerEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<BetEntity> Bets { get; set; }
    }
}
