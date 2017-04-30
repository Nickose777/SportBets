using System;
using System.Collections.Generic;
using SportBet.Models.Display;

namespace SportBet.Models.Edit
{
    public class ClientEditModel
    {
        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public List<BetDisplayModel> Bets { get; set; }
    }
}
