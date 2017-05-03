using SportBet.Services.DTOModels.Display;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

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
