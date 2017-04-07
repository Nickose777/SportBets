using System.Collections.Generic;
using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface IAnalyticRepository : IRepository<AnalyticEntity>
    {
        IEnumerable<AnalyticEntity> GetNotDeleted();
    }
}
