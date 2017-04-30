using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Display;
using System;
using System.Collections.Generic;

namespace SportBet.Services.DTOModels.Edit
{
    public class ClientEditDTO
    {
        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public List<BetDisplayDTO> Bets { get; set; }
    }
}
