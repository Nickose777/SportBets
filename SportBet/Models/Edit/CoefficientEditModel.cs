using SportBet.Models.Base;

namespace SportBet.Models.Edit
{
    public class CoefficientEditModel : CoefficientBaseModel
    {
        public decimal Value { get; set; }

        public decimal NewValue { get; set; }

        public string NewDescription { get; set; }

        public bool? Win { get; set; }

        public bool? NewWin { get; set; }
    }
}
