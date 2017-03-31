using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.ResultTypes
{
    public class DataServiceMessage<T> : ServiceMessage where T : class
    {
        public T Data { get; private set; }

        public DataServiceMessage(T data, string message, bool success)
            : base(message, success)
        {
            this.Data = data;
        }
    }
}
