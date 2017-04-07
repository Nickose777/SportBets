using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        UserEntity GetUserByLogin(string login);
        int GetIdByLogin(string login);
    }
}
