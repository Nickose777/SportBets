using SportBet.Core.Models;
using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Repositories
{
    class ClientRepository : RepositoryBase<ClientEntity>, IRepository<ClientEntity>
    {
        public ClientRepository(SportBetDbContext context)
            : base(context) { }
    }
}
