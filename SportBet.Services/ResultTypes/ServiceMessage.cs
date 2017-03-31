using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.ResultTypes
{
    public class ServiceMessage
    {
        public string Message { get; private set; }
        public bool IsSuccessful { get; private set; }

        public ServiceMessage(string message, bool success)
        {
            this.Message = message;
            this.IsSuccessful = success;
        }
    }
}
