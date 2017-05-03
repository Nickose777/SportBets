using System.Collections.Generic;
using System.Collections.ObjectModel;
using SportBet.Services.DTOModels.Display;

namespace SportBet.AnalyticControls.ViewModels
{
    public class SportRatingViewModel : ObservableObject
    {
        public SportRatingViewModel(IEnumerable<SportRatingDTO> sportRatings)
        {
            this.SportRatings = new ObservableCollection<SportRatingDTO>(sportRatings);
        }

        public ObservableCollection<SportRatingDTO> SportRatings { get; private set; }
    }
}
