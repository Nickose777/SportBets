using System;

namespace SportBet.Services.DTOModels.Display
{
    public class ClientDisplayDTO
    {
        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfRegistration { get; set; }
    }
}
