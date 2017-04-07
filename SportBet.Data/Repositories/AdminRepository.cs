using System;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class AdminRepository : RepositoryBase<AdminEntity>, IAdminRepository
    {
        public AdminRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }
    }
}
