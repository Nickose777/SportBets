using System;
using System.Collections.Generic;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class AdminRepository : RepositoryBase<AdminEntity>, IAdminRepository
    {
        public AdminRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public IEnumerable<AdminEntity> GetNotDeleted()
        {
            return GetAll(admin => !admin.IsDeleted);
        }
    }
}
