using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Contracts
{
    public interface IAdminPassword
    {
        void SetPassword(string value);
        string GetPassword();
    }
}
