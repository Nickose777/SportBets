using System.Collections.Generic;
using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface IAdminRepository : IRepository<AdminEntity>
    {
        IEnumerable<AdminEntity> GetNotDeleted();
    }
}
