using System;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class ClientRepository : RepositoryBase<ClientEntity>, IClientRepository
    {
        public ClientRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }
    }
}
