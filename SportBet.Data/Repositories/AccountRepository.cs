using System;
using SportBet.Data.Contracts.Repositories;

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

        public void RegisterClientRole(string login, string password)
        {
            Register("register_client", login, password);
        }

        public void RegisterBookmakerRole(string login, string password)
        {
            Register("register_bookmaker", login, password);
        }

        public void RegisterAdminRole(string login, string password)
        {
            Register("register_admin", login, password);
        }

        public void RegisterAnalyticRole(string login, string password)
        {
            Register("register_analytic", login, password);
        }

        private void Register(string functionName, string login, string password)
        {
            SportBetDbContext context = GetContext();
            string query = String.Format("DO $$ BEGIN PERFORM {0}('{1}', '{2}'); END $$;", functionName, login, password);
            context.Database.ExecuteSqlCommand(query);
        }

        public void DeleteBookmakerRole(string login)
        {
            Delete("delete_bookmaker_role", login);
        }

        public void DeleteClientRole(string login)
        {
            Delete("delete_client_role", login);
        }

        public void DeleteAdminRole(string login)
        {
            Delete("delete_admin_role", login);
        }

        public void DeleteAnalyticRole(string login)
        {
            Delete("delete_analytic_role", login);
        }

        private void Delete(string functionName, string login)
        {
            SportBetDbContext context = GetContext();
            string query = String.Format("DO $$ BEGIN PERFORM {0}('{1}'); END $$;", functionName, login);
            context.Database.ExecuteSqlCommand(query);
        }

        public void ChangePassword(string login, string newPassword)
        {
            SportBetDbContext context = GetContext();
            string query = String.Format("DO $$ BEGIN PERFORM change_password('{0}', '{1}'); END $$;", login, newPassword);
            context.Database.ExecuteSqlCommand(query);
        }
    }
}
