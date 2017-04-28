using AutoMapper;
using SportBet.CommonControls.Errors;
using SportBet.CommonControls.Participants.UserControls;
using SportBet.CommonControls.Participants.ViewModels;
using SportBet.Contracts;
using SportBet.Contracts.Controllers;
using SportBet.Models.Base;
using SportBet.Models.Create;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Base;
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

        public void Add()
        {
            UIElement element = GetAddElement();
            Window window = WindowFactory.CreateByContentsSize(element);

            window.Show();
        }

        public void Display()
        {
            UIElement element = GetDisplayElement();
            Window window = WindowFactory.CreateByContentsSize(element);

            window.Show();
        }

        public UIElement GetAddElement()
        {
            UIElement element = null;

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
                IEnumerable<string> countries = countryServiceMessage.Data;
                IEnumerable<string> sports = sportServiceMessage.Data;

                element = Create(countries, sports);
            }
            else
            {
                List<ServiceMessage> messages = new List<ServiceMessage>()
                {
                    countryServiceMessage,
                    sportServiceMessage
                };

                ErrorViewModel viewModel = new ErrorViewModel(messages);
                ErrorControl control = new ErrorControl(viewModel);

                element = control;
            }

            return element;
        }

        public UIElement GetDisplayElement()
        {
            ParticipantListViewModel viewModel = new ParticipantListViewModel(this, facade);
            ParticipantListControl control = new ParticipantListControl(viewModel);

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

            return control;
        }

        private UIElement Create(IEnumerable<string> countries, IEnumerable<string> sports)
        {
            ParticipantCreateViewModel viewModel = new ParticipantCreateViewModel(countries, sports);
            ParticipantCreateControl control = new ParticipantCreateControl(viewModel);

            viewModel.ParticipantCreated += (s, e) =>
            {
                ParticipantBaseModel participantModel = e.Participant;
                ParticipantBaseDTO participantDTO = Mapper.Map<ParticipantBaseModel, ParticipantBaseDTO>(participantModel);

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

            return control;
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
