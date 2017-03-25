using SportBet.Services.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Factories
{
    class SuperUserServiceFactory : ServiceFactory
    {
        public SuperUserServiceFactory(string connectionString)
            : base(connectionString) { }
    }
}
