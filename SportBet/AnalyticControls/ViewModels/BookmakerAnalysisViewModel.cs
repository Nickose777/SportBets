using System.Collections.Generic;
using System.Collections.ObjectModel;
using SportBet.Services.DTOModels.Display;

namespace SportBet.AnalyticControls.ViewModels
{
    public class BookmakerAnalysisViewModel : ObservableObject
    {
        public BookmakerAnalysisViewModel(IEnumerable<BookmakerAnalysisDTO> bookmakers)
        {
            this.Bookmakers = new ObservableCollection<BookmakerAnalysisDTO>(bookmakers);
        }

        public ObservableCollection<BookmakerAnalysisDTO> Bookmakers { get; private set; }
    }
}
