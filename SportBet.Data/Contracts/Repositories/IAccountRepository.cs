namespace SportBet.Data.Contracts.Repositories
{
    public interface IAccountRepository
    {
        void CreateDefaultSuperuser(string login, string password);

        void RegisterBookmakerRole(string login, string password);

        void RegisterClientRole(string login, string password);

        void RegisterAdminRole(string login, string password);

        void RegisterAnalyticRole(string login, string password);

        void DeleteBookmakerRole(string login);

        void DeleteClientRole(string login);

        void DeleteAdminRole(string login);

        void DeleteAnalyticRole(string login);
    }
}
