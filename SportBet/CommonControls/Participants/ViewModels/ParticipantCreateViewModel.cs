﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SportBet.EventHandlers.Create;
using SportBet.Models.Create;
using SportBet.Models.Base;

namespace SportBet.CommonControls.Participants.ViewModels
{
    public class ParticipantCreateViewModel : ObservableObject
    {
        public event ParticipantEventHandler ParticipantCreated;

        private ParticipantBaseModel participant;

        public ParticipantCreateViewModel(IEnumerable<string> countries, IEnumerable<string> sports)
        {
            this.participant = new ParticipantBaseModel();

            this.Countries = new ObservableCollection<string>(countries);
            this.Sports = new ObservableCollection<string>(sports);

            this.CreateParticipantCommand = new DelegateCommand(() => RaiseParticipantCreatedEvent(participant), CanCreate);
        }

        public ICommand CreateParticipantCommand { get; private set; }

        private bool CanCreate(object parameter)
        {
            return
                !String.IsNullOrEmpty(participant.Name) &&
                !String.IsNullOrEmpty(participant.SportName) &&
                !String.IsNullOrEmpty(participant.CountryName) &&
                participant.Name.Length <= 20;
        }

        public string ParticipantName
        {
            get { return participant.Name; }
            set
            {
                participant.Name = value;
                RaisePropertyChangedEvent("ParticipantName");
            }
        }

        public string CountryName
        {
            get { return participant.CountryName; }
            set
            {
                participant.CountryName = value;
                RaisePropertyChangedEvent("CountryName");
            }
        }

        public string SportName
        {
            get { return participant.SportName; }
            set
            {
                participant.SportName = value;
                RaisePropertyChangedEvent("SportName");
            }
        }

        public ObservableCollection<string> Countries { get; set; }
        
        public ObservableCollection<string> Sports { get; set; }

        private void RaiseParticipantCreatedEvent(ParticipantBaseModel participant)
        {
            var handler = ParticipantCreated;
            if (handler != null)
            {
                ParticipantEventArgs e = new ParticipantEventArgs(participant);
                handler(this, e);
            }
        }
    }
}
