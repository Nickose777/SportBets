using System.Collections.Generic;
using SportBet.Models.Base;
using SportBet.Models.Edit;

namespace SportBet.CommonControls.Tournaments.ViewModels
{
    public class TournamentManageViewModel : ObservableObject
    {
        public TournamentInfoViewModel InfoViewModel { get; private set; }

        public TournamentParticipantViewModel ParticipantViewModel { get; private set; }

        public TournamentManageViewModel(TournamentBaseModel tournament, IEnumerable<ParticipantBaseModel> tournamentParticipants, IEnumerable<ParticipantBaseModel> sportParticipants)
        {
            TournamentEditModel tournamentEditModel = new TournamentEditModel
            {
                Name = tournament.Name,
                SportName = tournament.SportName,
                DateOfStart = tournament.DateOfStart,
                Participants = new List<ParticipantBaseModel>(tournamentParticipants)
            };

            InfoViewModel = new TournamentInfoViewModel(tournament);
            ParticipantViewModel = new TournamentParticipantViewModel(tournamentEditModel, sportParticipants);
        }
    }
}
