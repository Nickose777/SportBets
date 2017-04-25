using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface IBetRepository : IRepository<BetEntity>
    {
        bool Exists(int coefficientId, string clientPhoneNumber);
    }
}
