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

        public void Create()
        {
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

                Create(sports, tournaments, events);
            }
        }

        public void Display()
        {
            CoefficientListViewModel viewModel = new CoefficientListViewModel(this, facade);
            CoefficientListControl control = new CoefficientListControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            window.Show();
        }

        private void Create(IEnumerable<string> sports, IEnumerable<TournamentBaseModel> tournaments, IEnumerable<EventBaseModel> events)
        {
            CoefficientCreateViewModel viewModel = new CoefficientCreateViewModel(sports, tournaments, events);
            CoefficientCreateControl control = new CoefficientCreateControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

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

            window.Show();
        }
    }
}
