using System;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;

namespace SportBet.Data
{
    class AdminPassword : IAdminPassword
    {
        private readonly Func<SportBetDbContext> GetContext;

        public AdminPassword(Func<SportBetDbContext> GetContext)
        {
            this.GetContext = GetContext;
        }

        public void SetPassword(string value)
        {
            SportBetDbContext context = GetContext();

            AdminPasswordEntity adminPasswordEntity = context.AdminPassword.SingleOrDefault();
            if (adminPasswordEntity != null)
            {
                adminPasswordEntity.Value = value;
            }
            else
            {
                adminPasswordEntity = new AdminPasswordEntity
                {
                    Value = value
                };
                context.AdminPassword.Add(adminPasswordEntity);
            }
        }

        public string GetPassword()
        {
            string password = null;

            SportBetDbContext context = GetContext();

            AdminPasswordEntity adminPasswordEntity = context.AdminPassword.SingleOrDefault();
            if (adminPasswordEntity != null)
            {
                password = adminPasswordEntity.Value;
            }

            return password;
        }
    }
}
