using SportBet.Core;
using SportBet.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportBet.Core.Entities;

namespace SportBet.Data.Repositories
{
    class AccountRepository : IAccountRepository
    {
        private SportBetDbContext context;

        public AccountRepository(SportBetDbContext context)
        {
            this.context = context;
        }

        public void RegisterClient(ClientRegister clientRegister)
        {
            context.Database.ExecuteSqlCommand("CREATE ROLE @p0 PASSWORD @p1", clientRegister.Login, clientRegister.Password);
            UserEntity userEntity = new UserEntity
            {
                Login = clientRegister.Login,
                RoleId = 5
            };
            context.Users.Add(userEntity);

            ClientEntity clientEntity = new ClientEntity
            {
                Id = userEntity.Id,
                FirstName = clientRegister.FirstName,
                LastName = clientRegister.LastName,
                PhoneNumber = clientRegister.PhoneNumber,
                DateOfBirth = clientRegister.DateOfBirth,
                DateOfRegistration = clientRegister.DateOfRegistration
            };
            context.Clients.Add(clientEntity);
        }
    }
}
