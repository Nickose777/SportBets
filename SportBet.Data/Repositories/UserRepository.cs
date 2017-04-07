using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public int GetIdByLogin(string login)
        {
            SportBetDbContext context = GetContext();
            UserEntity userEntity = context.Users.Single(user => user.Login == login);
            return userEntity.Id;
        }
    }
}
