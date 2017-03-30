using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Encryption
{
    public interface IEncryptor
    {
        string Encrypt(string message);
    }
}
