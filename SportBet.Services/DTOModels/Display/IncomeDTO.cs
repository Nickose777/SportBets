namespace SportBet.Services.DTOModels.Display
{
    public class IncomeDTO
    {
        public int WonBets { get; set; }

        public int LostBets { get; set; }

        public decimal Profits { get; set; }

        public decimal Costs { get; set; }

        public decimal Income { get; set; }
    }
}
