using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface ISportRepository : IRepository<SportEntity>
    {
        SportEntity Get(string sportName);
    }
}
