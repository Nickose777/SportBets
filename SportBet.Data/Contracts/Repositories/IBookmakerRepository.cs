using System.Collections.Generic;
using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface IBookmakerRepository : IRepository<BookmakerEntity>
    {
        IEnumerable<BookmakerEntity> GetNotDeleted();
    }
}
