using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts.Repositories;

namespace SportBet.Data.Repositories
{
    class BetRepository : RepositoryBase<BetEntity>, IBetRepository
    {
        public BetRepository(Func<SportBetDbContext> GetContext)
            : base(GetContext) { }

        public override BetEntity Get(int id)
        {
            throw new InvalidOperationException();
        }

        public bool Exists(int coefficientId, string clientPhoneNumber)
        {
            BetEntity betEntity = Get(coefficientId, clientPhoneNumber);

            return betEntity != null;
        }

        public BetEntity Get(int coefficientId, string clientPhoneNumber)
        {
            SportBetDbContext context = GetContext();

            ClientEntity clientEntity = context.Clients.Single(c => c.PhoneNumber == clientPhoneNumber);
            BetEntity betEntity = context.Bets.SingleOrDefault(b => b.ClientId == clientEntity.Id && b.CoefficientId == coefficientId);

            return betEntity;
        }
    }
}
