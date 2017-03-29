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
        private Func<SportBetDbContext> GetContext;

        public AccountRepository(Func<SportBetDbContext> GetContext)
        {
            this.GetContext = GetContext;
        }

        public void CreateDefaultSuperuser(string password)
        {
            SportBetDbContext context = GetContext();

            string query = String.Format("CREATE ROLE admin PASSWORD '{0}' LOGIN SUPERUSER", password);
            context.Database.ExecuteSqlCommand(query);
        }

        public void RegisterClient(string login, string password)
        {
            Register("register_client", login, password);
        }
        public void RegisterBookmaker(string login, string password)
        {
            Register("register_bookmaker", login, password);
        }

        private void Register(string functionName, string login, string password)
        {
            SportBetDbContext context = GetContext();
            string query = String.Format("DO $$ BEGIN PERFORM {0}('{1}', '{2}'); END $$;", functionName, login, password);
            context.Database.ExecuteSqlCommand(query);
        }
    }
}
