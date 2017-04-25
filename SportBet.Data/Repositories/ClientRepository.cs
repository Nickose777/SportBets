using System;
using System.Linq;
using System.Collections.Generic;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class ClientRepository : RepositoryBase<ClientEntity>, IClientRepository
    {
        public ClientRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public IEnumerable<ClientEntity> GetNotDeleted()
        {
            return GetAll(client => !client.IsDeleted);
        }


        public ClientEntity Get(string clientPhoneNumber)
        {
            SportBetDbContext context = GetContext();

            return context.Clients.SingleOrDefault(c => c.PhoneNumber == clientPhoneNumber);
        }
    }
}
