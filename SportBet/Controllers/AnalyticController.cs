using System;
using System.Windows;
using AutoMapper;
using SportBet.Contracts;
using SportBet.Contracts.Controllers;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Models.Registers;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;
using SportBet.SuperuserControls.UserControls;
using SportBet.SuperuserControls.ViewModels;
using SportBet.WindowFactories;

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
            UIElement element = GetRegisterElement();
            Window window = WindowFactory.CreateByContentsSize(element);

            window.Show();
        }

        public void Display()
        {
            UIElement element = GetDisplayElement();
            Window window = WindowFactory.CreateByContentsSize(element);

            window.Show();
        }

        public UIElement GetRegisterElement()
        {
            AnalyticRegisterViewModel viewModel = new AnalyticRegisterViewModel(new AnalyticRegisterModel());
            RegisterAnalyticControl control = new RegisterAnalyticControl(viewModel);

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

            return control;
        }

        public UIElement GetDisplayElement()
        {
            ManageAnalyticsViewModel viewModel = new ManageAnalyticsViewModel(this, facade);
            ManageAnalyticsControl control = new ManageAnalyticsControl(viewModel);

            viewModel.AnalyticEdit += (s, e) => Edit(e.Analytic);
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

            return control;
        }

        private void Edit(AnalyticDisplayModel analyticDisplayModel)
        {
            AnalyticEditModel analytic = new AnalyticEditModel
            {
                FirstName = analyticDisplayModel.FirstName,
                LastName = analyticDisplayModel.LastName,
                PhoneNumber = analyticDisplayModel.PhoneNumber,
                Login = analyticDisplayModel.Login
            };

            AnalyticInfoViewModel viewModel = new AnalyticInfoViewModel(analytic);
            AnalyticInfoControl control = new AnalyticInfoControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.AnalyticEdited += (s, e) =>
            {
                AnalyticEditModel analyticEditModel = e.Analytic;
                AnalyticEditDTO analyticEditDTO = Mapper.Map<AnalyticEditModel, AnalyticEditDTO>(analyticEditModel);

                using (IAnalyticService service = factory.CreateAnalyticService())
                {
                    ServiceMessage serviceMessage = service.Update(analyticEditDTO);
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
