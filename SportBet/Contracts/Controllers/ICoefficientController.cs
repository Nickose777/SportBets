using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Contracts.Controllers
{
    public interface ICoefficientController : IReceiveMessage
    {
        void Create();
    }
}
