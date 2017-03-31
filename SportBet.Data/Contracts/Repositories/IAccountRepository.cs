using SportBet.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Contracts.Repositories
{
    public interface IAccountRepository
    {
        void CreateDefaultSuperuser(string password);
        void RegisterClient(string login, string password);
        void RegisterBookmaker(string login, string password);

        void DeleteBookmaker(string login);
    }
}
