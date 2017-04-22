using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface ICoefficientRepository : IRepository<CoefficientEntity>
    {
        bool Exists(int eventId, decimal value, string description);
    }
}
