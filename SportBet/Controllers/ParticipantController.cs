using AutoMapper;
using SportBet.CommonControls.Participants.UserControls;
using SportBet.CommonControls.Participants.ViewModels;
using SportBet.Contracts;
using SportBet.Contracts.Controllers;
using SportBet.Contracts.Subjects;
using SportBet.Models.Create;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SportBet.Controllers
{
    class ParticipantController : SubjectBase, ISubject, IParticipantController
    {
        private readonly FacadeBase<ParticipantDisplayModel> facade;

        public ParticipantController(ServiceFactory factory, FacadeBase<ParticipantDisplayModel> facade)
            : base(factory)
        {
            this.facade = facade;
            facade.ReceivedMessage += RaiseReceivedMessageEvent;
        }

        public void Create()
        {
            DataServiceMessage<IEnumerable<string>> countryServiceMessage;
            DataServiceMessage<IEnumerable<string>> sportServiceMessage;

            using (ICountryService service = factory.CreateCountryService())
            {
                countryServiceMessage = service.GetAll();
                RaiseReceivedMessageEvent(countryServiceMessage.IsSuccessful, countryServiceMessage.Message);
            }
            using (ISportService service = factory.CreateSportService())
            {
                sportServiceMessage = service.GetAll();
                RaiseReceivedMessageEvent(sportServiceMessage.IsSuccessful, sportServiceMessage.Message);
            }

            if (countryServiceMessage.IsSuccessful && sportServiceMessage.IsSuccessful)
            {
                var countries = countryServiceMessage.Data;
                var sports = sportServiceMessage.Data;

                Create(countries, sports);
            }
        }

        public void Display()
        {
            ParticipantListViewModel viewModel = new ParticipantListViewModel(this, facade);
            ParticipantListControl control = new ParticipantListControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.ParticipantSelected += (s, e) =>
            {
                DataServiceMessage<IEnumerable<string>> countryServiceMessage;
                DataServiceMessage<IEnumerable<string>> sportServiceMessage;

                using (ICountryService service = factory.CreateCountryService())
                {
                    countryServiceMessage = service.GetAll();
                    RaiseReceivedMessageEvent(countryServiceMessage.IsSuccessful, countryServiceMessage.Message);
                }
                using (ISportService service = factory.CreateSportService())
                {
                    sportServiceMessage = service.GetAll();
                    RaiseReceivedMessageEvent(sportServiceMessage.IsSuccessful, sportServiceMessage.Message);
                }

                if (countryServiceMessage.IsSuccessful && sportServiceMessage.IsSuccessful)
                {
                    var countries = countryServiceMessage.Data;
                    var sports = sportServiceMessage.Data;

                    Edit(e.Participant, countries, sports);
                }
            };

            window.Show();
        }

        private void Create(IEnumerable<string> countries, IEnumerable<string> sports)
        {
            ParticipantCreateViewModel viewModel = new ParticipantCreateViewModel(countries, sports);
            ParticipantCreateControl control = new ParticipantCreateControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.ParticipantCreated += (s, e) =>
            {
                ParticipantCreateModel participantModel = e.Participant;
                ParticipantCreateDTO participantDTO = Mapper.Map<ParticipantCreateModel, ParticipantCreateDTO>(participantModel);

                using (IParticipantService service = factory.CreateParticipantService())
                {
                    ServiceMessage serviceMessage = service.Create(participantDTO);
                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        Notify();
                        viewModel.ParticipantName = String.Empty;
                    }
                }
            };

            window.Show();
        }

        private void Edit(ParticipantDisplayModel participantDisplayModel, IEnumerable<string> countries, IEnumerable<string> sports)
        {
            ParticipantInfoViewModel viewModel = new ParticipantInfoViewModel(participantDisplayModel, sports, countries);
            ParticipantInfoControl control = new ParticipantInfoControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.ParticipantEdited += (s, e) =>
            {
                ParticipantEditModel participantEditModel = e.Participant;
                ParticipantEditDTO participantEditDTO = Mapper.Map<ParticipantEditModel, ParticipantEditDTO>(participantEditModel);

                using (IParticipantService service = factory.CreateParticipantService())
                {
                    ServiceMessage serviceMessage = service.Update(participantEditDTO);
                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        window.Close();
                        Notify();
                    }
                }
            };

            window.Show();
        }
    }
}
