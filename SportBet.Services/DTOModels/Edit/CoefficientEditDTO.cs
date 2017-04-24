using SportBet.Services.DTOModels.Base;

namespace SportBet.Services.DTOModels.Edit
{
    public class CoefficientEditDTO : CoefficientBaseDTO
    {
        public decimal Value { get; set; }

        public decimal NewValue { get; set; }

        public string NewDescription { get; set; }
    }
}
