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

        public void CreateDefaultSuperuser(string login, string password)
        {
            Register("register_superuser", login, password);
        }

        public void RegisterClient(string login, string password)
        {
            Register("register_client", login, password);
        }

        public void RegisterBookmaker(string login, string password)
        {
            Register("register_bookmaker", login, password);
        }

        public void RegisterAdmin(string login, string password)
        {
            Register("register_admin", login, password);
        }

        public void RegisterAnalytic(string login, string password)
        {
            Register("register_analytic", login, password);
        }

        private void Register(string functionName, string login, string password)
        {
            SportBetDbContext context = GetContext();
            string query = String.Format("DO $$ BEGIN PERFORM {0}('{1}', '{2}'); END $$;", functionName, login, password);
            context.Database.ExecuteSqlCommand(query);
        }

        public void DeleteBookmaker(string login)
        {
            Delete("delete_bookmaker_role", login);
        }

        public void DeleteClient(string login)
        {
            Delete("delete_client_role", login);
        }

        public void DeleteAdmin(string login)
        {
            Delete("delete_admin_role", login);
        }

        public void DeleteAnalytic(string login)
        {
            Delete("delete_analytic_role", login);
        }

        private void Delete(string functionName, string login)
        {
            SportBetDbContext context = GetContext();
            string query = String.Format("DO $$ BEGIN PERFORM {0}('{1}'); END $$;", functionName, login);
            context.Database.ExecuteSqlCommand(query);
        }
    }
}
