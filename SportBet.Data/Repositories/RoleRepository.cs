using System;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class RoleRepository : RepositoryBase<RoleEntity>, IRoleRepository
    {
        public RoleRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }
    }
}
