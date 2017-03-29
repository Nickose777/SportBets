using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Repositories
{
    class BookmakerRepository : RepositoryBase<BookmakerEntity>, IBookmakerRepository
    {
        public BookmakerRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }
    }
}
