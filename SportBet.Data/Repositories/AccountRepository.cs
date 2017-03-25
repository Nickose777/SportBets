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
            string query = String.Format("DO $$ BEGIN PERFORM register_client('{0}', '{1}'); END $$;", login, password);
            context.Database.ExecuteSqlCommand(query);
        }
    }
}
