﻿using System.Windows;
using SportBet.Contracts;
using SportBet.Logs;
using SportBet.WindowFactories;

namespace SportBet
{
    public class LogWindowBase : SignOutWindowBase
    {
        protected readonly ILogger logger;

        public LogWindowBase(ILogger logger)
        {
            this.logger = logger;
        }

        protected void UpdateLogs(bool success, string message)
        {
            LogObject log = new LogObject(message, success);
            logger.Log(log);
        }

        protected void ShowLogWindow()
        {
            LogControl control = new LogControl(logger);
            Window window = WindowFactory.CreateByContentsSize(control);

            window.Show();
        }
    }
}
