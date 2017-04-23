using SportBet.Services.DTOModels.Base;

namespace SportBet.Services.DTOModels.Display
{
    public class CoefficientDisplayDTO : CoefficientBaseDTO
    {
        public decimal Value { get; set; }

        public bool? Win { get; set; }
    }
}
