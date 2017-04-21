using SportBet.CommonControls.Events.UserControls;
using SportBet.CommonControls.Events.ViewModels;
using SportBet.Contracts;
using SportBet.Contracts.Controllers;
using SportBet.Contracts.Subjects;
using SportBet.Models.Display;
using SportBet.Models.Select;
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
            DataServiceMessage<IEnumerable<TournamentDisplayDTO>> tournamentServiceMessage;
            DataServiceMessage<IEnumerable<string>> sportServiceMessage;

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

            if (tournamentServiceMessage.IsSuccessful && sportServiceMessage.IsSuccessful)
            {
                IEnumerable<TournamentSelectModel> tournaments = tournamentServiceMessage
                    .Data
                    .Select(t =>
                    {
                        return new TournamentSelectModel
                        {
                            DateOfStart = t.DateOfStart,
                            SportName = t.SportName,
                            TournamentName = t.Name
                        };
                    });
                IEnumerable<string> sports = sportServiceMessage.Data;

                Create(tournaments, sports);
            }
        }

        private void Create(IEnumerable<TournamentSelectModel> tournaments, IEnumerable<string> sports)
        {
            EventCreateViewModel viewModel = new EventCreateViewModel(tournaments, sports);
            EventCreateControl control = new EventCreateControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.EventCreated += (s, e) =>
            {
                EventCreateModel eventCreateModel = e.Event;
                EventCreateDTO eventCreateDTO = Mapper.Map<EventCreateModel, EventCreateDTO>(eventCreateModel);

                using (IEventService service = factory.CreateEventService())
                {
                    ServiceMessage serviceMessage = service.Create(eventCreateDTO);
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
    }
}
