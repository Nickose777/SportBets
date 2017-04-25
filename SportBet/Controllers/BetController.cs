using AutoMapper;
using SportBet.CommonControls.Bets.UserControls;
using SportBet.CommonControls.Bets.ViewModels;
using SportBet.Contracts.Controllers;
using SportBet.Contracts.Subjects;
using SportBet.Models.Base;
using SportBet.Models.Create;
using SportBet.Models.Display;
using SportBet.Models.Registers;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
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
    class BetController : SubjectBase, ISubject, IBetController
    {
        public BetController(ServiceFactory factory)
            : base(factory)
        {

        }

        public void Create()
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

                Create(bookmakerPhoneNumber, clients, sports, tournaments, events, coefficients);
            }
        }

        private void Create(string bookmakerPhoneNumber, IEnumerable<ClientDisplayModel> clients, IEnumerable<string> sports, IEnumerable<TournamentDisplayModel> tournaments, IEnumerable<EventDisplayModel> events, IEnumerable<CoefficientDisplayModel> coefficients)
        {
            BetCreateViewModel viewModel = new BetCreateViewModel(bookmakerPhoneNumber, clients, sports, tournaments, events, coefficients);
            BetCreateControl control = new BetCreateControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

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
                        //TODO
                        //viewModel.clear()
                        Notify();
                    }
                }
            };

            window.Show();
        }
    }
}
