using SportBet.EventHandlers.Create;
using SportBet.Models.Base;
using SportBet.Models.Create;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SportBet.CommonControls.Coefficients.ViewModels
{
    //TODO
    //display event participants too
    public class CoefficientCreateViewModel : ObservableObject
    {
        public event CoefficientCreateEventHandler CoefficientCreated;

        private readonly CoefficientCreateModel coefficient;

        private string selectedSport;
        private TournamentBaseModel selectedTournament;
        private EventBaseModel selectedEvent;

        public CoefficientCreateViewModel(IEnumerable<string> sports, IEnumerable<TournamentBaseModel> tournaments, IEnumerable<EventBaseModel> events)
        {
            this.coefficient = new CoefficientCreateModel();

            this.CreateCoefficientCommand = new DelegateCommand(() => Create(coefficient), CanCreate);

            this.Sports = new ObservableCollection<string>(sports);
            this.Tournaments = new ObservableCollection<TournamentBaseModel>(tournaments);
            this.Events = new ObservableCollection<EventBaseModel>(events);

            this.SortedTournaments.Filter = obj =>
            {
                TournamentBaseModel t = obj as TournamentBaseModel;
                return 
                    SelectedSport != null &&
                    t.SportName == SelectedSport;
            };
            this.SortedEvents.Filter = obj =>
            {
                EventBaseModel e = obj as EventBaseModel;
                return 
                    SelectedTournament != null &&
                    e.TournamentName == SelectedTournament.Name && 
                    e.DateOfTournamentStart == SelectedTournament.DateOfStart;
            };
        }

        public ICommand CreateCoefficientCommand { get; private set; }

        public decimal CoefficientValue
        {
            get { return coefficient.Value; }
            set
            {
                coefficient.Value = value;
                RaisePropertyChangedEvent("CoefficientValue");
            }
        }

        public string Description
        {
            get { return coefficient.Description; }
            set
            {
                coefficient.Description = value;
                RaisePropertyChangedEvent("Description");
            }
        }

        //TODO
        //use object SportBaseModel
        public string SelectedSport
        {
            get { return selectedSport; }
            set
            {
                selectedSport = value;

                SortedTournaments.Refresh();
                SortedEvents.Refresh();

                RaisePropertyChangedEvent("SelectedSport");
            }
        }

        public TournamentBaseModel SelectedTournament
        {
            get { return selectedTournament; }
            set
            {
                selectedTournament = value;

                SortedEvents.Refresh();

                RaisePropertyChangedEvent("SelectedTournament");
            }
        }

        public EventBaseModel SelectedEvent
        {
            get { return selectedEvent; }
            set
            {
                selectedEvent = value;
                RaisePropertyChangedEvent("SelectedEvent");
            }
        }

        public ICollectionView SortedTournaments
        {
            get { return CollectionViewSource.GetDefaultView(Tournaments); }
        }

        public ICollectionView SortedEvents
        {
            get { return CollectionViewSource.GetDefaultView(Events); }
        }

        public ObservableCollection<string> Sports { get; private set; }

        public ObservableCollection<TournamentBaseModel> Tournaments { get; private set; }

        public ObservableCollection<EventBaseModel> Events { get; private set; }

        private void Create(CoefficientCreateModel coefficient)
        {
            coefficient.SportName = SelectedSport;
            coefficient.TournamentName = SelectedTournament.Name;
            coefficient.DateOfEvent = SelectedEvent.DateOfEvent;
            coefficient.Participants = SelectedEvent.Participants;

            RaiseCoefficientCreatedEvent(coefficient);
        }

        private bool CanCreate(object parameter)
        {
            return
                CoefficientValue > 0 &&
                !String.IsNullOrEmpty(Description) &&
                SelectedSport != null &&
                SelectedTournament != null &&
                SelectedEvent != null;
        }

        private void RaiseCoefficientCreatedEvent(CoefficientCreateModel coefficient)
        {
            var handler = CoefficientCreated;
            if (handler != null)
            {
                CoefficientCreateEventArgs e = new CoefficientCreateEventArgs(coefficient);
                handler(this, e);
            }
        }
    }
}
