namespace SportBet.Logs
{
    public class LogObject
    {
        private readonly string message;
        private readonly bool success;

        public LogObject(string message, bool success)
        {
            this.message = message;
            this.success = success;
        }

        public string Message
        {
            get { return message; }
        }

        public bool Success
        {
            get { return success; }
        }
    }
}
