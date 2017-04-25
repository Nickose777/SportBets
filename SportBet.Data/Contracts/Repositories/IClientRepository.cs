using System.Collections.Generic;
using SportBet.Core.Entities;

namespace SportBet.Data.Contracts.Repositories
{
    public interface IClientRepository : IRepository<ClientEntity>
    {
        IEnumerable<ClientEntity> GetNotDeleted();

        ClientEntity Get(string clientPhoneNumber);
    }
}
