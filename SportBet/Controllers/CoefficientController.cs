using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AutoMapper;
using SportBet.CommonControls.Coefficients.UserControls;
using SportBet.CommonControls.Coefficients.ViewModels;
using SportBet.Contracts.Controllers;
using SportBet.Contracts.Subjects;
using SportBet.Models.Base;
using SportBet.Models.Create;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Base;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;
using SportBet.Contracts;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Services.DTOModels.Edit;
using SportBet.CommonControls.Errors;

namespace SportBet.Controllers
{
    class CoefficientController : SubjectBase, ISubject, ICoefficientController
    {
        private FacadeBase<CoefficientDisplayModel> facade;

        public CoefficientController(ServiceFactory factory, FacadeBase<CoefficientDisplayModel> facade)
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
            UIElement element = null;

            DataServiceMessage<IEnumerable<string>> sportServiceMessage;
            DataServiceMessage<IEnumerable<TournamentDisplayDTO>> tournamentServiceMessage;
            DataServiceMessage<IEnumerable<EventDisplayDTO>> eventServiceMessage;

            using (ISportService service = factory.CreateSportService())
            {
                sportServiceMessage = service.GetAll();
                RaiseReceivedMessageEvent(sportServiceMessage.IsSuccessful, sportServiceMessage.Message);
            }
            using (ITournamentService service = factory.CreateTournamentService())
            {
                tournamentServiceMessage = service.GetAll();
                RaiseReceivedMessageEvent(sportServiceMessage.IsSuccessful, sportServiceMessage.Message);
            }
            using (IEventService service = factory.CreateEventService())
            {
                eventServiceMessage = service.GetAll();
                RaiseReceivedMessageEvent(sportServiceMessage.IsSuccessful, sportServiceMessage.Message);
            }

            if (sportServiceMessage.IsSuccessful && tournamentServiceMessage.IsSuccessful && eventServiceMessage.IsSuccessful)
            {
                IEnumerable<TournamentBaseDTO> tournamentDTOs = tournamentServiceMessage.Data.AsEnumerable<TournamentBaseDTO>();
                IEnumerable<EventBaseDTO> eventDTOs = eventServiceMessage.Data.AsEnumerable<EventBaseDTO>();

                IEnumerable<string> sports = sportServiceMessage.Data;
                IEnumerable<TournamentBaseModel> tournaments = tournamentDTOs
                    .Select(t => Mapper.Map<TournamentBaseDTO, TournamentBaseModel>(t));
                IEnumerable<EventBaseModel> events = eventDTOs
                    .Select(e => Mapper.Map<EventBaseDTO, EventBaseModel>(e));

                element = Add(sports, tournaments, events);
            }
            else
            {
                List<ServiceMessage> messages = new List<ServiceMessage>()
                {
                    sportServiceMessage,
                    tournamentServiceMessage,
                    eventServiceMessage
                };

                ErrorViewModel viewModel = new ErrorViewModel(messages);
                ErrorControl control = new ErrorControl(viewModel);

                element = control;
            }

            return element;
        }

        public UIElement GetDisplayElement()
        {
            CoefficientListViewModel viewModel = new CoefficientListViewModel(this, facade);
            CoefficientListControl control = new CoefficientListControl(viewModel);

            viewModel.CoefficientSelected += (s, e) => Edit(e.Coefficient);

            return control;
        }

        private UIElement Add(IEnumerable<string> sports, IEnumerable<TournamentBaseModel> tournaments, IEnumerable<EventBaseModel> events)
        {
            CoefficientCreateViewModel viewModel = new CoefficientCreateViewModel(sports, tournaments, events);
            CoefficientCreateControl control = new CoefficientCreateControl(viewModel);

            viewModel.CoefficientCreated += (s, e) =>
            {
                CoefficientCreateModel coefficientBaseModel = e.Coefficient;
                CoefficientCreateDTO coefficientBaseDTO = Mapper.Map<CoefficientCreateModel, CoefficientCreateDTO>(coefficientBaseModel);

                using (ICoefficientService service = factory.CreateCoefficientService())
                {
                    ServiceMessage serviceMessage = service.Create(coefficientBaseDTO);
                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        viewModel.CoefficientValue = 0;
                        viewModel.Description = String.Empty;
                        Notify();
                    }
                }
            };

            return control;
        }

        private void Edit(CoefficientDisplayModel coefficientDisplayModel)
        {
            CoefficientInfoViewModel viewModel = new CoefficientInfoViewModel(coefficientDisplayModel);
            CoefficientInfoControl control = new CoefficientInfoControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.CoefficientEdited += (s, e) =>
            {
                CoefficientEditModel coefficientEditModel = e.Coefficient;
                CoefficientEditDTO coefficientEditDTO = Mapper.Map<CoefficientEditModel, CoefficientEditDTO>(coefficientEditModel);

                using (ICoefficientService service = factory.CreateCoefficientService())
                {
                    ServiceMessage serviceMessage = service.Update(coefficientEditDTO);
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
