﻿using System;

namespace SportBet.Services.DTOModels.Register
{
    public class ClientRegisterDTO : RegisterBaseDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
