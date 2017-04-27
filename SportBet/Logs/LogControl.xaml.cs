using System;
using System.Windows.Controls;
using SportBet.Contracts;

namespace SportBet.Logs
{
    /// <summary>
    /// Interaction logic for LogControl.xaml
    /// </summary>
    public partial class LogControl : UserControl
    {
        private readonly ILogger logger;

        public LogControl(ILogger logger)
        {
            InitializeComponent();
            this.logger = logger;

            logger.LogsUpdated += (s, e) => UpdateLogs();
            UpdateLogs();
        }

        private void UpdateLogs()
        {
            var items = logList.Items;
            items.Clear();

            var logs = logger.GetLoggs();
            foreach (var log in logs)
            {
                string line = String.Format("{0}! {1}", log.Success ? "Success" : "Fail or error", log.Message);
                items.Add(line);
            }
        }
    }
}
