using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using SportBet.EventHandlers.Create;
using System.Linq;
using SportBet.Models.Base;
using SportBet.Models.Create;
using SportBet.Models.Registers;
using SportBet.Models.Display;
using SportBet.Services.DTOModels.Display;

namespace SportBet.CommonControls.Bets.ViewModels
{
    public class BetCreateViewModel : ObservableObject
    {
        public event BetCreateEventHandler BetCreated;

        private readonly BetCreateModel betCreateModel;

        private string selectedSport;
        private TournamentBaseModel selectedTournament;
        private EventBaseModel selectedEvent;
        private CoefficientDisplayModel selectedCoefficient;
        private ClientDisplayModel selectedClient;

        //TODO
        //this shit
        public BetCreateViewModel(string bookmakerPhoneNumber, IEnumerable<ClientDisplayModel> clients, IEnumerable<string> sports, IEnumerable<TournamentBaseModel> tournaments, IEnumerable<EventBaseModel> events, IEnumerable<CoefficientDisplayModel> coefficients)
        {
            this.betCreateModel = new BetCreateModel
            {
                BookmakerPhoneNumber = bookmakerPhoneNumber
            };

            this.CreateBetCommand = new DelegateCommand(CreateBet, CanCreateBet);

            this.Sports = new ObservableCollection<string>(sports);
            this.Tournaments = new ObservableCollection<TournamentBaseModel>(tournaments);
            this.Events = new ObservableCollection<EventBaseModel>(events);
            this.Coefficients = new ObservableCollection<CoefficientDisplayModel>(coefficients);
            this.Clients = new ObservableCollection<ClientDisplayModel>(clients);

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
                    SelectedSport != null &&
                    SelectedTournament != null &&
                    e.SportName == SelectedSport &&
                    e.TournamentName == SelectedTournament.Name;
            };
            this.SortedCoefficients.Filter = obj =>
            {
                CoefficientBaseModel c = obj as CoefficientBaseModel;

                bool valid =
                    SelectedSport != null &&
                    SelectedTournament != null &&
                    SelectedEvent != null &&
                    c.SportName == SelectedSport &&
                    c.TournamentName == SelectedTournament.Name &&
                    c.DateOfEvent == SelectedEvent.DateOfEvent;

                if (valid)
                {
                    valid = c.Participants.Count == SelectedEvent.Participants.Count;

                    if (valid)
                    {
                        for (int i = 0; i < c.Participants.Count; i++)
                        {
                            var p1 = c.Participants
                                .OrderBy(p => p.Name)
                                .ThenBy(p => p.CountryName)
                                .ThenBy(p => p.SportName)
                                .ElementAt(i);
                            var p2 = SelectedEvent.Participants
                                .OrderBy(p => p.Name)
                                .ThenBy(p => p.CountryName)
                                .ThenBy(p => p.SportName)
                                .ElementAt(i);
                            valid =
                                p1.Name == p2.Name &&
                                p1.CountryName == p2.CountryName &&
                                p1.SportName == p2.SportName;

                            if (!valid)
                            {
                                break;
                            }
                        }
                    }
                }

                return valid;
            };
        }

        public ICommand CreateBetCommand { get; private set; }

        public string BookmakerPhoneNumber
        {
            get { return betCreateModel.BookmakerPhoneNumber; }
        }

        public decimal Sum
        {
            get { return betCreateModel.Sum; }
            set
            {
                betCreateModel.Sum = value;
                RaisePropertyChangedEvent("Sum");
            }
        }

        public string SelectedSport
        {
            get { return selectedSport; }
            set
            {
                selectedSport = value;

                SortedTournaments.Refresh();

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

                SortedCoefficients.Refresh();

                RaisePropertyChangedEvent("SelectedEvent");
            }
        }

        public CoefficientDisplayModel SelectedCoefficient
        {
            get { return selectedCoefficient; }
            set
            {
                selectedCoefficient = value;

                RaisePropertyChangedEvent("SelectedCoefficient");
            }
        }

        public ClientDisplayModel SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                RaisePropertyChangedEvent("SelectedClient");
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

        public ICollectionView SortedCoefficients
        {
            get { return CollectionViewSource.GetDefaultView(Coefficients); }
        }

        public ObservableCollection<string> Sports { get; private set; }

        public ObservableCollection<TournamentBaseModel> Tournaments { get; private set; }

        public ObservableCollection<EventBaseModel> Events { get; private set; }

        public ObservableCollection<CoefficientDisplayModel> Coefficients { get; private set; }

        public ObservableCollection<ClientDisplayModel> Clients { get; private set; }

        private void CreateBet()
        {
            betCreateModel.SportName = SelectedSport;
            betCreateModel.TournamentName = SelectedTournament.Name;
            betCreateModel.DateOfEvent = SelectedEvent.DateOfEvent;
            betCreateModel.EventParticipants = SelectedEvent.Participants;
            betCreateModel.CoefficientDescription = SelectedCoefficient.Description;
            betCreateModel.ClientPhoneNumber = SelectedClient.PhoneNumber;

            RaiseBetCreatedEvent(betCreateModel);
        }

        private bool CanCreateBet(object parameter)
        {
            return
                SelectedSport != null &&
                SelectedTournament != null &&
                SelectedEvent != null &&
                SelectedCoefficient != null &&
                SelectedClient != null &&
                Sum > 0;
        }

        private void RaiseBetCreatedEvent(BetCreateModel betCreateModel)
        {
            var handler = BetCreated;
            if (handler != null)
            {
                BetCreateEventArgs e = new BetCreateEventArgs(betCreateModel);
                handler(this, e);
            }
        }
    }
}
