namespace SportBet.Controllers
{
    class SuperuserBetController : BetControllerBase
    {
        public SuperuserBetController(SportBet.Services.Contracts.ServiceFactory factory, SportBet.Contracts.FacadeBase<SportBet.Models.Display.BetDisplayModel> facade)
            : base(factory, facade) { }

        protected override CommonControls.Bets.ViewModels.BetListViewModel GetBetListViewModel(Contracts.ISubject subject, Contracts.FacadeBase<Models.Display.BetDisplayModel> facade)
        {
            return new SportBet.CommonControls.Bets.ViewModels.SuperuserBetListViewModel(subject, facade);
        }
    }
}
