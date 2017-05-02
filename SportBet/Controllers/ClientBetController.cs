namespace SportBet.Controllers
{
    class ClientBetController : BetControllerBase
    {
        public ClientBetController(SportBet.Services.Contracts.ServiceFactory factory, SportBet.Contracts.FacadeBase<SportBet.Models.Display.BetDisplayModel> facade)
            : base(factory, facade) { }

        protected override SportBet.CommonControls.Bets.ViewModels.BetListViewModel GetBetListViewModel(Contracts.ISubject subject, Contracts.FacadeBase<Models.Display.BetDisplayModel> facade)
        {
            return new SportBet.CommonControls.Bets.ViewModels.ClientBetListViewModel(subject, facade);
        }
    }
}
