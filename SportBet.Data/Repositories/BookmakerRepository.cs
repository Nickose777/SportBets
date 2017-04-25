using System;
using System.Linq;
using System.Collections.Generic;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class BookmakerRepository : RepositoryBase<BookmakerEntity>, IBookmakerRepository
    {
        public BookmakerRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public IEnumerable<BookmakerEntity> GetNotDeleted()
        {
            return GetAll(bookmaker => !bookmaker.IsDeleted);
        }

        public BookmakerEntity Get(string bookmakerPhoneNumber)
        {
            SportBetDbContext context = GetContext();

            return context.Bookmakers.SingleOrDefault(b => b.PhoneNumber == bookmakerPhoneNumber);
        }
    }
}
