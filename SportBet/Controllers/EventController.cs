using SportBet.CommonControls.Events.UserControls;
using SportBet.CommonControls.Events.ViewModels;
using SportBet.Contracts;
using SportBet.Contracts.Controllers;
using SportBet.Contracts.Subjects;
using SportBet.Models.Display;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using SportBet.Models.Create;
using SportBet.Services.DTOModels.Create;
using AutoMapper;
using System;
using SportBet.Models.Base;
using SportBet.Models.Extra;
using SportBet.Services.DTOModels.Extra;
using SportBet.Models.Edit;
using SportBet.Services.DTOModels.Edit;

namespace SportBet.Controllers
{
    class EventController : SubjectBase, ISubject, IEventController
    {
        private readonly FacadeBase<EventDisplayModel> facade;

        public EventController(ServiceFactory factory, FacadeBase<EventDisplayModel> facade)
            : base(factory)
        {
            this.facade = facade;
            facade.ReceivedMessage += RaiseReceivedMessageEvent;
        }

        public void Create()
        {
            DataServiceMessage<IEnumerable<string>> sportServiceMessage;
            DataServiceMessage<IEnumerable<TournamentDisplayDTO>> tournamentServiceMessage;
            DataServiceMessage<IEnumerable<ParticipantTournamentDTO>> participantServiceMessage;

            using (ITournamentService service = factory.CreateTournamentService())
            {
                tournamentServiceMessage = service.GetAll();
                RaiseReceivedMessageEvent(tournamentServiceMessage.IsSuccessful, tournamentServiceMessage.Message);
            }
            using (ISportService service = factory.CreateSportService())
            {
                sportServiceMessage = service.GetAll();
                RaiseReceivedMessageEvent(sportServiceMessage.IsSuccessful, sportServiceMessage.Message);
            }
            using (IParticipantService service = factory.CreateParticipantService())
            {
                participantServiceMessage = service.GetAllWithTournaments();
                RaiseReceivedMessageEvent(sportServiceMessage.IsSuccessful, sportServiceMessage.Message);
            }

            if (tournamentServiceMessage.IsSuccessful && sportServiceMessage.IsSuccessful && participantServiceMessage.IsSuccessful)
            {
                IEnumerable<string> sports = sportServiceMessage.Data;
                IEnumerable<TournamentDisplayDTO> tournamentDisplayDTOs = tournamentServiceMessage.Data;
                IEnumerable<ParticipantTournamentDTO> participantTournamentDTOs = participantServiceMessage.Data;

                IEnumerable<TournamentBaseModel> tournamentBaseModels = tournamentDisplayDTOs
                    .Select(t => Mapper.Map<TournamentDisplayDTO, TournamentBaseModel>(t))
                    .ToList();
                IEnumerable<ParticipantTournamentModel> participantTournamentModels = participantTournamentDTOs
                    .Select(p => Mapper.Map<ParticipantTournamentDTO, ParticipantTournamentModel>(p))
                    .ToList();

                Create(sports, tournamentBaseModels, participantTournamentModels);
            }
        }

        public void Display()
        {
            EventListViewModel viewModel = new EventListViewModel(this, facade);
            EventListControl control = new EventListControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.EventSelected += (s, e) => Edit(e.Event);

            window.Show();
        }

        private void Create(IEnumerable<string> sports, IEnumerable<TournamentBaseModel> tournaments, IEnumerable<ParticipantTournamentModel> participants)
        {
            EventCreateViewModel viewModel = new EventCreateViewModel(sports, tournaments, participants);
            EventCreateControl control = new EventCreateControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.EventCreated += (s, e) =>
            {
                EventCreateModel eventCreateModel = e.Event;
                EventCreateDTO eventCreateDTO = Mapper.Map<EventCreateModel, EventCreateDTO>(eventCreateModel);

                using (IEventService service = factory.CreateEventService())
                {
                    ServiceMessage serviceMessage = service.CreateWithParticipants(eventCreateDTO);
                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        viewModel.Notes = String.Empty;
                        Notify();
                    }
                }
            };

            window.Show();
        }

        private void Edit(EventDisplayModel eventDisplayModel)
        {
            EventManageViewModel viewModel = new EventManageViewModel(eventDisplayModel);
            EventManageControl control = new EventManageControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.InfoViewModel.EventEdited += (s, e) =>
            {
                EventEditModel eventEditModel = e.Event;
                EventEditDTO eventEditDTO = Mapper.Map<EventEditModel, EventEditDTO>(eventEditModel);

                //TODO
                //move to Mapper config
                eventEditDTO.Notes = eventEditModel.NewNotes;

                using (IEventService service = factory.CreateEventService())
                {
                    ServiceMessage serviceMessage = service.Update(eventEditDTO);
                    RaiseReceivedMessageEvent(serviceMessage);

                    if (serviceMessage.IsSuccessful)
                    {
                        Notify();
                    }
                }
            };

            window.Show();
        }
    }
}
