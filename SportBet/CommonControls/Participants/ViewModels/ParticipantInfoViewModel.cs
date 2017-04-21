using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SportBet.EventHandlers.Edit;
using SportBet.Models.Display;
using SportBet.Models.Edit;

namespace SportBet.CommonControls.Participants.ViewModels
{
    public class ParticipantInfoViewModel : ObservableObject
    {
        public event ParticipantEditEventHandler ParticipantEdited;

        private readonly ParticipantEditModel participant;

        public ParticipantInfoViewModel(ParticipantDisplayModel participant, IEnumerable<string> sports, IEnumerable<string> countries)
        {
            this.participant = new ParticipantEditModel
            {
                Name = participant.Name,
                NewParticipantName = participant.Name,

                CountryName = participant.CountryName,
                NewCountryName = participant.CountryName,

                SportName = participant.SportName,
                NewSportName = participant.SportName
            };

            this.Sports = new ObservableCollection<string>(sports);
            this.Countries = new ObservableCollection<string>(countries);

            this.SaveParticipantCommand = new DelegateCommand(() => RaiseParticipantEditedEvent(this.participant), CanSave);
            this.UndoCommand = new DelegateCommand(() => Undo(), IsDirty);
        }

        public ICommand SaveParticipantCommand { get; private set; }

        public ICommand UndoCommand { get; private set; }

        public string Name
        {
            get { return participant.NewParticipantName; }
            set
            {
                participant.NewParticipantName = value;
                RaisePropertyChangedEvent("Name");
            }
        }

        public string SportName
        {
            get { return participant.NewSportName; }
            set
            {
                participant.NewSportName = value;
                RaisePropertyChangedEvent("SportName");
            }
        }

        public string CountryName
        {
            get { return participant.NewCountryName; }
            set
            {
                participant.NewCountryName = value;
                RaisePropertyChangedEvent("CountryName");
            }
        }

        public ObservableCollection<string> Sports { get; private set; }

        public ObservableCollection<string> Countries { get; private set; }

        private bool CanSave(object parameter)
        {
            return
                IsDirty(parameter) &&
                !String.IsNullOrEmpty(Name) &&
                !String.IsNullOrEmpty(SportName) &&
                !String.IsNullOrEmpty(CountryName) &&
                Name.Length <= 20;
        }

        private void Undo()
        {
            Name = participant.Name;
            SportName = participant.SportName;
            CountryName = participant.CountryName;
        }

        private bool IsDirty(object parameter)
        {
            return
                participant.Name != participant.NewParticipantName ||
                participant.SportName != participant.NewSportName ||
                participant.CountryName != participant.NewCountryName;
        }

        private void RaiseParticipantEditedEvent(ParticipantEditModel participant)
        {
            var handler = ParticipantEdited;
            if (handler != null)
            {
                ParticipantEditEventArgs e = new ParticipantEditEventArgs(participant);
                handler(this, e);
            }
        }
    }
}
