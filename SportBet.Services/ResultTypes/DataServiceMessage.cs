namespace SportBet.Services.ResultTypes
{
    public class DataServiceMessage<TData> : ServiceMessage where TData : class
    {
        public TData Data { get; private set; }

        public DataServiceMessage(T data, string message, bool success)
            : base(message, success)
        {
            this.Data = data;
        }
    }
}
