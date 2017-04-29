using SportBet.Models.Base;

namespace SportBet.Models.Edit
{
    public class BetEditModel : BetBaseModel
    {
        public decimal OldSum { get; set; }

        public decimal NewSum { get; set; }
    }
}
