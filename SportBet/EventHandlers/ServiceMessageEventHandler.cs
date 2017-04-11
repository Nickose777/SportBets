using System;

namespace SportBet.EventHandlers
{
    public delegate void ServiceMessageEventHandler(object sender, ServiceMessageEventArgs e);

    public class ServiceMessageEventArgs : EventArgs
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public ServiceMessageEventArgs(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
    }
}
