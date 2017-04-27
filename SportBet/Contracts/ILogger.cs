using System;
using System.Collections.Generic;
using SportBet.Logs;

namespace SportBet.Contracts
{
    public interface ILogger
    {
        event EventHandler LogsUpdated;

        void Log(LogObject log);

        void Clear();

        IEnumerable<LogObject> GetLoggs();
    }
}
