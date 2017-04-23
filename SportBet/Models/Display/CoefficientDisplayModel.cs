using SportBet.Models.Base;

namespace SportBet.Models.Display
{
    public class CoefficientDisplayModel : CoefficientBaseModel
    {
        public decimal Value { get; set; }

        public bool? Win { get; set; }
    }
}
