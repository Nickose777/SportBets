using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AutoMapper;
using SportBet.CommonControls.Errors;
using SportBet.CommonControls.Tournaments.UserControls;
using SportBet.CommonControls.Tournaments.ViewModels;
using SportBet.Contracts;
using SportBet.Contracts.Controllers;
using SportBet.Contracts.Subjects;
using SportBet.Models.Base;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;

namespace SportBet.Controllers
{
    class TournamentController : SubjectBase, ISubject, ITournamentController
    {
        private readonly FacadeBase<TournamentDisplayModel> facade;

        public TournamentController(ServiceFactory factory, FacadeBase<TournamentDisplayModel> facade)
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

            using (ISportService service = factory.CreateSportService())
            {
                DataServiceMessage<IEnumerable<string>> serviceMessage = service.GetAll();
                RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                if (serviceMessage.IsSuccessful)
                {
                    IEnumerable<string> sports = serviceMessage.Data;
                    element = Add(sports);
                }
                else
                {
                    List<ServiceMessage> messages = new List<ServiceMessage>()
                    {
                        serviceMessage
                    };

                    ErrorViewModel viewModel = new ErrorViewModel(messages);
                    ErrorControl control = new ErrorControl(viewModel);

                    element = control;
                }
            }

            return element;
        }

        public UIElement GetDisplayElement()
        {
            TournamentListViewModel viewModel = new TournamentListViewModel(this, facade);
            TournamentListControl control = new TournamentListControl(viewModel);

            viewModel.TournamentSelected += (s, e) =>
            {
                TournamentDisplayModel tournamentDisplayModel = e.Tournament;
                TournamentBaseDTO tournamentBaseDTO = Mapper.Map<TournamentDisplayModel, TournamentBaseDTO>(tournamentDisplayModel);

                using (IParticipantService service = factory.CreateParticipantService())
                {
                    DataServiceMessage<IEnumerable<ParticipantBaseDTO>> serviceMessage1 = service.GetBySport(tournamentBaseDTO.SportName);
                    DataServiceMessage<IEnumerable<ParticipantBaseDTO>> serviceMessage2 = service.GetByTournament(tournamentBaseDTO);

                    RaiseReceivedMessageEvent(serviceMessage1.IsSuccessful, serviceMessage1.Message);
                    RaiseReceivedMessageEvent(serviceMessage2.IsSuccessful, serviceMessage2.Message);

                    if (serviceMessage1.IsSuccessful && serviceMessage2.IsSuccessful)
                    {
                        IEnumerable<ParticipantBaseModel> sportParticipants = serviceMessage1
                            .Data
                            .Select(p => Mapper.Map<ParticipantBaseDTO, ParticipantBaseModel>(p))
                            .ToList();
                        IEnumerable<ParticipantBaseModel> tournamentParticipants = serviceMessage2
                            .Data
                            .Select(p => Mapper.Map<ParticipantBaseDTO, ParticipantBaseModel>(p))
                            .ToList();

                        Edit(tournamentDisplayModel, tournamentParticipants, sportParticipants);
                    }
                }
            };

            return control;
        }

        private UIElement Add(IEnumerable<string> sportNames)
        {
            TournamentCreateViewModel viewModel = new TournamentCreateViewModel(sportNames);
            TournamentCreateControl control = new TournamentCreateControl(viewModel);

            viewModel.TournamentCreated += (s, e) =>
            {
                TournamentBaseModel tournamentCreateModel = e.Tournament;
                TournamentBaseDTO tournamentCreateDTO = Mapper.Map<TournamentBaseModel, TournamentBaseDTO>(tournamentCreateModel);

                using (ITournamentService service = factory.CreateTournamentService())
                {
                    ServiceMessage serviceMessage = service.Create(tournamentCreateDTO);
                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        Notify();
                        viewModel.Name = String.Empty;
                    }
                }
            };

            return control;
        }

        private void Edit(TournamentBaseModel tournament, IEnumerable<ParticipantBaseModel> tournamentParticipants, IEnumerable<ParticipantBaseModel> sportParticipants)
        {
            TournamentManageViewModel viewModel = new TournamentManageViewModel(tournament, tournamentParticipants, sportParticipants);
            TournamentManageControl control = new TournamentManageControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.InfoViewModel.TournamentEdited += (s, e) =>
            {
                TournamentEditModel tournamentEditModel = e.Tournament;
                TournamentEditDTO tournamentEditDTO = Mapper.Map<TournamentEditModel, TournamentEditDTO>(tournamentEditModel);

                using (ITournamentService service = factory.CreateTournamentService())
                {
                    ServiceMessage serviceMessage = service.Update(tournamentEditDTO);
                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        Notify();
                    }
                }
            };
            viewModel.ParticipantViewModel.TournamentEdited += (s, e) =>
            {
                TournamentEditModel tournamentEditModel = e.Tournament;
                TournamentEditDTO tournamentEditDTO = Mapper.Map<TournamentEditModel, TournamentEditDTO>(tournamentEditModel);

                using (ITournamentService service = factory.CreateTournamentService())
                {
                    ServiceMessage serviceMessage = service.UpdateParticipants(tournamentEditDTO);
                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

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
