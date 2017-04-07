using System;

namespace SportBet.Models.Registers
{
    public class ClientRegisterModel : RegisterBaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
