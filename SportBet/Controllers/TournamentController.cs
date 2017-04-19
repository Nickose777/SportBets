using AutoMapper;
using SportBet.CommonControls.Tournaments.UserControls;
using SportBet.CommonControls.Tournaments.ViewModels;
using SportBet.Contracts;
using SportBet.Contracts.Controllers;
using SportBet.Contracts.Subjects;
using SportBet.Models.Create;
using SportBet.Models.Display;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Create;
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
    class TournamentController : SubjectBase, ISubject, ITournamentController
    {
        private readonly FacadeBase<TournamentDisplayModel> facade;

        public TournamentController(ServiceFactory factory, FacadeBase<TournamentDisplayModel> facade)
            : base(factory)
        {
            this.facade = facade;
        }

        public void Create()
        {
            using (ISportService service = factory.CreateSportService())
            {
                DataServiceMessage<IEnumerable<string>> serviceMessage = service.GetAll();
                RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                if (serviceMessage.IsSuccessful)
                {
                    var sports = serviceMessage.Data;
                    Create(sports);
                }
            }
        }

        private void Create(IEnumerable<string> sportNames)
        {
            TournamentCreateViewModel viewModel = new TournamentCreateViewModel(sportNames);
            TournamentCreateControl control = new TournamentCreateControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.TournamentCreated += (s, e) =>
            {
                TournamentCreateModel tournamentCreateModel = e.Tournament;
                TournamentCreateDTO tournamentCreateDTO = Mapper.Map<TournamentCreateModel, TournamentCreateDTO>(tournamentCreateModel);

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

            window.Show();
        }
    }
}
