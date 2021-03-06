﻿using AutoMapper;
using SportBet.CommonControls.Bets.UserControls;
using SportBet.CommonControls.Bets.ViewModels;
using SportBet.CommonControls.Errors;
using SportBet.Contracts;
using SportBet.Contracts.Controllers;
using SportBet.Models.Base;
using SportBet.Models.Create;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Models.Registers;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SportBet.Controllers
{
    public abstract class BetControllerBase : SubjectBase, ISubject, IBetController
    {
        private readonly FacadeBase<BetDisplayModel> facade;

        public BetControllerBase(ServiceFactory factory, FacadeBase<BetDisplayModel> facade)
            : base(factory)
        {
            this.facade = facade;
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
            DataServiceMessage<string> bookmakerServiceMessage;
            DataServiceMessage<IEnumerable<ClientDisplayDTO>> clientServiceMessage;
            DataServiceMessage<IEnumerable<string>> sportServiceMessage;
            DataServiceMessage<IEnumerable<TournamentDisplayDTO>> tournamentServiceMessage;
            DataServiceMessage<IEnumerable<EventDisplayDTO>> eventServiceMessage;
            DataServiceMessage<IEnumerable<CoefficientDisplayDTO>> coefficientServiceMessage;

            using (IBookmakerService service = factory.CreateBookmakerService())
            {
                bookmakerServiceMessage = service.GetLoggedInBookmakersPhoneNumber();
                RaiseReceivedMessageEvent(bookmakerServiceMessage);
            }
            using (IClientService service = factory.CreateClientService())
            {
                clientServiceMessage = service.GetAll();
                RaiseReceivedMessageEvent(clientServiceMessage);
            }
            using (ISportService service = factory.CreateSportService())
            {
                sportServiceMessage = service.GetAll();
                RaiseReceivedMessageEvent(sportServiceMessage);
            }
            using (ITournamentService service = factory.CreateTournamentService())
            {
                tournamentServiceMessage = service.GetAll();
                RaiseReceivedMessageEvent(tournamentServiceMessage);
            }
            using (IEventService service = factory.CreateEventService())
            {
                eventServiceMessage = service.GetAll();
                RaiseReceivedMessageEvent(eventServiceMessage);
            }
            using (ICoefficientService service = factory.CreateCoefficientService())
            {
                coefficientServiceMessage = service.GetAll();
                RaiseReceivedMessageEvent(coefficientServiceMessage);
            }

            bool success =
                bookmakerServiceMessage.IsSuccessful &&
                clientServiceMessage.IsSuccessful &&
                sportServiceMessage.IsSuccessful &&
                tournamentServiceMessage.IsSuccessful &&
                eventServiceMessage.IsSuccessful &&
                coefficientServiceMessage.IsSuccessful;

            UIElement element;
            if (success)
            {
                string bookmakerPhoneNumber = bookmakerServiceMessage.Data;
                IEnumerable<ClientDisplayModel> clients = clientServiceMessage
                    .Data
                    .Select(client => Mapper.Map<ClientDisplayDTO, ClientDisplayModel>(client));
                IEnumerable<string> sports = sportServiceMessage.Data;
                IEnumerable<TournamentDisplayModel> tournaments = tournamentServiceMessage
                    .Data
                    .Select(tournament => Mapper.Map<TournamentDisplayDTO, TournamentDisplayModel>(tournament));
                IEnumerable<EventDisplayModel> events = eventServiceMessage
                    .Data
                    .Select(_event => Mapper.Map<EventDisplayDTO, EventDisplayModel>(_event));
                IEnumerable<CoefficientDisplayModel> coefficients = coefficientServiceMessage
                    .Data
                    .Select(coefficient => Mapper.Map<CoefficientDisplayDTO, CoefficientDisplayModel>(coefficient));

                element = Add(bookmakerPhoneNumber, clients, sports, tournaments, events, coefficients);
            }
            else
            {
                List<ServiceMessage> messages = new List<ServiceMessage>()
                {
                    bookmakerServiceMessage,
                    clientServiceMessage,
                    sportServiceMessage,
                    tournamentServiceMessage,
                    eventServiceMessage,
                    coefficientServiceMessage
                };

                ErrorViewModel viewModel = new ErrorViewModel(messages);
                ErrorControl control = new ErrorControl(viewModel);

                element = control;
            }

            return element;
        }

        public UIElement GetDisplayElement()
        {
            BetListViewModel viewModel = GetBetListViewModel(this, facade);
            BetListControl control = new BetListControl(viewModel);

            viewModel.BetSelected += (s, e) => Edit(e.Bet);

            return control;
        }

        protected abstract BetListViewModel GetBetListViewModel(ISubject subject, FacadeBase<BetDisplayModel> facade);

        private UIElement Add(string bookmakerPhoneNumber, IEnumerable<ClientDisplayModel> clients, IEnumerable<string> sports, IEnumerable<TournamentDisplayModel> tournaments, IEnumerable<EventDisplayModel> events, IEnumerable<CoefficientDisplayModel> coefficients)
        {
            BetCreateViewModel viewModel = new BetCreateViewModel(bookmakerPhoneNumber, clients, sports, tournaments, events, coefficients);
            BetCreateControl control = new BetCreateControl(viewModel);

            viewModel.BetCreated += (s, e) =>
            {
                BetCreateModel betCreateModel = e.Bet;
                BetCreateDTO betCreateDTO = Mapper.Map<BetCreateModel, BetCreateDTO>(betCreateModel);

                using (IBetService service = factory.CreateBetService())
                {
                    ServiceMessage serviceMessage = service.Create(betCreateDTO);
                    RaiseReceivedMessageEvent(serviceMessage);

                    if (serviceMessage.IsSuccessful)
                    {
                        viewModel.Sum = 0;
                        Notify();
                    }
                }
            };

            return control;
        }

        private void Edit(BetDisplayModel betDisplayModel)
        {
            BetInfoViewModel viewModel = new BetInfoViewModel(betDisplayModel);
            BetInfoControl control = new BetInfoControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.BetEdited += (s, e) =>
            {
                BetEditModel betEditModel = e.Bet;
                BetEditDTO betEditDTO = Mapper.Map<BetEditModel, BetEditDTO>(betEditModel);

                using (IBetService service = factory.CreateBetService())
                {
                    ServiceMessage serviceMessage = service.Update(betEditDTO);
                    RaiseReceivedMessageEvent(serviceMessage);

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
