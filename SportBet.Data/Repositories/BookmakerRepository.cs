using SportBet.Core.Models;
using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Repositories
{
    class BookmakerRepository : RepositoryBase<BookmakerEntity>, IRepository<BookmakerEntity>
    {
        public BookmakerRepository(SportBetDbContext context)
            : base(context) { }
    }
}
