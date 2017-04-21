using SportBet.EventHandlers.Create;
using SportBet.Models.Create;
using SportBet.Models.Select;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SportBet.CommonControls.Events.ViewModels
{
    public class EventCreateViewModel : ObservableObject
    {
        public event EventCreateEventHandler EventCreated;

        private readonly EventCreateModel eventCreateModel;

        private TournamentSelectModel tournament;

        public EventCreateViewModel(IEnumerable<TournamentSelectModel> tournaments, IEnumerable<string> sports)
        {
            this.eventCreateModel = new EventCreateModel
            {
                DateOfEvent = DateTime.Now.AddMonths(2)
            };

            this.CreateEventCommand = new DelegateCommand(
                () => RaiseEventCreatedEvent(eventCreateModel),
                CanCreate
                );

            this.Sports = new ObservableCollection<string>(sports);
            this.Tournaments = new ObservableCollection<TournamentSelectModel>(tournaments);
            this.SortedTournaments.Filter = FilterTournament;
        }

        public ICommand CreateEventCommand { get; private set; }

        public string SportName
        {
            get { return eventCreateModel.SportName; }
            set
            {
                eventCreateModel.SportName = value;
                RaisePropertyChangedEvent("SportName");
                SortedTournaments.Refresh();
            }
        }

        public string TournamentName
        {
            get { return eventCreateModel.TournamentName; }
        }

        public string DateOfTournamentStart
        {
            get
            {
                string date = String.Empty;

                if (eventCreateModel.DateOfTournamentStart != default(DateTime))
                {
                    DateTime dateTime = eventCreateModel.DateOfTournamentStart;
                    date = String.Format("{0}.{1}.{2} ({3})", 
                        dateTime.Day.ToString().PadLeft(2, '0'),
                        dateTime.Month.ToString().PadLeft(2, '0'), 
                        dateTime.Year, 
                        dateTime.DayOfWeek);
                }

                return date;
            }
        }

        public DateTime DateOfEvent
        {
            get { return eventCreateModel.DateOfEvent; }
            set
            {
                eventCreateModel.DateOfEvent = value.Date;
                RaisePropertyChangedEvent("DateOfEvent");
            }
        }

        public string Notes
        {
            get { return eventCreateModel.Notes; }
            set
            {
                eventCreateModel.Notes = value;
                RaisePropertyChangedEvent("Notes");
            }
        }

        public TournamentSelectModel SelectedTournament
        {
            get { return tournament; }
            set
            {
                tournament = value;
                eventCreateModel.TournamentName = tournament != null ? tournament.TournamentName : String.Empty;
                eventCreateModel.DateOfTournamentStart = tournament != null ? tournament.DateOfStart : default(DateTime);

                RaisePropertyChangedEvent("SelectedTournament");
                RaisePropertyChangedEvent("TournamentName");
                RaisePropertyChangedEvent("DateOfTournamentStart");
            }
        }

        public ICollectionView SortedTournaments
        {
            get { return CollectionViewSource.GetDefaultView(Tournaments); }
        }

        public ObservableCollection<string> Sports { get; private set; }

        public ObservableCollection<TournamentSelectModel> Tournaments { get; private set; }

        private bool FilterTournament(object obj)
        {
            TournamentSelectModel tournament = obj as TournamentSelectModel;

            return tournament.SportName == SportName;
        }

        private bool CanCreate(object parameter)
        {
            return
                !String.IsNullOrEmpty(Notes) &&
                !String.IsNullOrEmpty(SportName) &&
                !String.IsNullOrEmpty(TournamentName) &&
                DateOfEvent >= eventCreateModel.DateOfTournamentStart;
        }

        private void RaiseEventCreatedEvent(EventCreateModel eventCreateModel)
        {
            var handler = EventCreated;
            if (handler != null)
            {
                EventEventArgs e = new EventEventArgs(eventCreateModel);
                handler(this, e);
            }
        }
    }
}
