using System;
using System.Collections.Generic;
using SportBet.Contracts;

namespace SportBet.Logs
{
    class Logger : ILogger
    {
        public event EventHandler LogsUpdated;

        private readonly List<LogObject> logs;

        public Logger()
        {
            logs = new List<LogObject>();
        }

        public void Log(LogObject log)
        {
            logs.Add(log);
            RaiseLogsUpdatedEvent();
        }

        public void Clear()
        {
            logs.Clear();
            RaiseLogsUpdatedEvent();
        }

        public IEnumerable<LogObject> GetLoggs()
        {
            return logs;
        }

        private void RaiseLogsUpdatedEvent()
        {
            var handler = LogsUpdated;
            if (handler != null)
            {
                EventArgs e = new EventArgs();
                handler(this, e);
            }
        }
    }
}
