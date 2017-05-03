using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.DTOModels.Display
{
    public class ClientAnalysisDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Profits { get; set; }

        public decimal Costs { get; set; }

        public decimal Income { get; set; }
    }
}
