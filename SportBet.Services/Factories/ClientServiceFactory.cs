﻿using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Factories
{
    class ClientServiceFactory : IServiceFactory
    {
        private string connectionString;

        public ClientServiceFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
