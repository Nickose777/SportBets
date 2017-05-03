using System.Collections.Generic;
using System.Collections.ObjectModel;
using SportBet.Services.DTOModels.Display;

namespace SportBet.AnalyticControls.ViewModels
{
    public class ClientAnalysisViewModel : ObservableObject
    {
        public ClientAnalysisViewModel(IEnumerable<ClientAnalysisDTO> clients)
        {
            this.Clients = new ObservableCollection<ClientAnalysisDTO>(clients);
        }

        public ObservableCollection<ClientAnalysisDTO> Clients { get; private set; }
    }
}
