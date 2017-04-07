using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        int GetIdByLogin(string login);
    }
}
