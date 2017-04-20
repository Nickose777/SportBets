using System;
using System.Windows;
using SportBet.AdminControls.UserControls;
using SportBet.AdminControls.ViewModels;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;
using SportBet.Models.Edit;
using SportBet.Services.DTOModels.Edit;
using AutoMapper;
using SportBet.Contracts.Controllers;
using SportBet.Models.Create;
using SportBet.Services.DTOModels.Create;

namespace SportBet.Controllers
{
    class SportController : SubjectBase, ISubject, ISportController
    {
        private readonly FacadeBase<string> facade;

        public SportController(ServiceFactory factory, FacadeBase<string> facade)
            : base(factory)
        {
            this.facade = facade;
            facade.ReceivedMessage += RaiseReceivedMessageEvent;
        }

        public void Add()
        {
            SportCreateViewModel viewModel = new SportCreateViewModel();
            SportCreateControl control = new SportCreateControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.SportCreated += (s, e) =>
            {
                SportCreateModel sportCreateModel = e.Sport;
                SportCreateDTO sportCreateDTO = Mapper.Map<SportCreateModel, SportCreateDTO>(sportCreateModel);

                using (ISportService service = factory.CreateSportService())
                {
                    ServiceMessage serviceMessage = service.Create(sportCreateDTO);
                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        viewModel.SportName = String.Empty;
                        Notify();
                    }
                }
            };

            window.Show();
        }

        public void Display()
        {
            SportListViewModel viewModel = new SportListViewModel(this, facade);
            SportListControl control = new SportListControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.SportSelected += (s, e) => EditSport(e.SportName);

            window.Show();
        }

        private void EditSport(string sportName)
        {
            SportEditViewModel viewModel = new SportEditViewModel(sportName);
            SportEditControl control = new SportEditControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.SportEdited += (s, e) =>
            {
                SportEditModel sportEditModel = e.Sport;
                SportEditDTO sportEditDTO = Mapper.Map<SportEditModel, SportEditDTO>(sportEditModel);

                using (ISportService service = factory.CreateSportService())
                {
                    ServiceMessage serviceMessage = service.Update(sportEditDTO);
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
