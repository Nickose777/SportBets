using SportBet.Core;
using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportBet.Core.Entities;

namespace SportBet.Data.Repositories
{
    class AccountRepository : IAccountRepository
    {
        private SportBetDbContext context;

        public AccountRepository(SportBetDbContext context)
        {
            this.context = context;
        }

        public void RegisterClient(string login, string password)
        {
            string createQuery = String.Format("CREATE USER \"{0}\" PASSWORD '{1}';", login, password);
            string grantsQuery1 = String.Format("GRANT USAGE ON SCHEMA public TO \"{0}\";", login);
            string grantsQuery2 = String.Format("GRANT SELECT ON TABLE \"Users\", \"Roles\" TO \"{0}\";", login);

            context.Database.ExecuteSqlCommand(createQuery);
            context.Database.ExecuteSqlCommand(grantsQuery1);
            context.Database.ExecuteSqlCommand(grantsQuery2);
        }
    }
}
