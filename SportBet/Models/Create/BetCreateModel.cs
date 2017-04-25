using SportBet.Models.Base;

namespace SportBet.Models.Create
{
    public class BetCreateModel : BetBaseModel
    {
        public string BookmakerPhoneNumber { get; set; }

        public decimal Sum { get; set; }
    }
}
