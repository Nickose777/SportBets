namespace SportBet.Services.DTOModels.Display
{
    public class SportRatingDTO
    {
        public string SportName { get; set; }

        public int BetsCount { get; set; }

        public decimal Profits { get; set; }

        public decimal Costs { get; set; }

        public decimal Income { get; set; }
    }
}
