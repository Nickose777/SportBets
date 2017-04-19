using System;
using System.Windows;
using AutoMapper;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.Controllers;
using SportBet.Models.Display;
using SportBet.Models.Registers;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;
using SportBet.SuperuserControls.UserControls;
using SportBet.SuperuserControls.ViewModels;
using SportBet.WindowFactories;
using SportBet.Contracts.Controllers;

namespace SportBet.Controllers
{
    class AnalyticController : SubjectBase, ISubject, IAnalyticController
    {
        private readonly FacadeBase<AnalyticDisplayModel> facade;

        public AnalyticController(ServiceFactory factory, FacadeBase<AnalyticDisplayModel> facade)
            : base(factory)
        {
            this.facade = facade;
            facade.ReceivedMessage += RaiseReceivedMessageEvent;
        }

        public void Register()
        {
            AnalyticRegisterViewModel viewModel = new AnalyticRegisterViewModel(new AnalyticRegisterModel());
            RegisterAnalyticControl control = new RegisterAnalyticControl(viewModel);

            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.AnalyticCreated += (s, e) =>
            {
                AnalyticRegisterModel analytic = e.Analytic;
                AnalyticRegisterDTO analyticDTO = Mapper.Map<AnalyticRegisterModel, AnalyticRegisterDTO>(analytic);

                using (IAccountService service = factory.CreateAccountService())
                {
                    ServiceMessage serviceMessage = service.Register(analyticDTO);
                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        viewModel.FirstName = String.Empty;
                        viewModel.LastName = String.Empty;
                        viewModel.PhoneNumber = String.Empty;
                        viewModel.Login = String.Empty;
                        viewModel.Password = String.Empty;
                        viewModel.ConfirmPassword = String.Empty;
                        Notify();
                    }
                }
            };

            window.Show();
        }

        public void Display()
        {
            ManageAnalyticsViewModel viewModel = new ManageAnalyticsViewModel(this, facade);
            ManageAnalyticsControl control = new ManageAnalyticsControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.AnalyticDeleted += (s, e) =>
            {
                using (IAnalyticService service = factory.CreateAnalyticService())
                {
                    AnalyticDisplayDTO deletedAnalytic = Mapper.Map<AnalyticDisplayModel, AnalyticDisplayDTO>(e.Analytic);
                    ServiceMessage serviceMessage = service.Delete(deletedAnalytic.Login);

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
