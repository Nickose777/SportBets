﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Models.Display
{
    public class ClientDisplayModel
    {
        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
