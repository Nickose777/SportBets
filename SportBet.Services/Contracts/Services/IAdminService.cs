using SportBet.Services.DTOModels;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.Contracts.Services
{
    public interface IAdminService : IDisposable
    {
        ServiceMessage Delete(AdminDisplayDTO adminDisplayDTO);

        DataServiceMessage<IEnumerable<AdminDisplayDTO>> GetAll();
    }
}
