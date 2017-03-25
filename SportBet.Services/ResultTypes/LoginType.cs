using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.ResultTypes
{
    public enum LoginType
    {
        Superuser,
        Admin,
        Analytic,
        Bookmaker,
        Client,
        NoLogin
    }
}
