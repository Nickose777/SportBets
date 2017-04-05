using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Repositories
{
    class AdminRepository : RepositoryBase<AdminEntity>, IAdminRepository
    {
        public AdminRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }
    }
}
