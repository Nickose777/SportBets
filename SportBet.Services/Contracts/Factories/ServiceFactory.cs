using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Contracts.Factories
{
    public abstract class ServiceFactory
    {
        protected readonly string connectionString;

        public ServiceFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
