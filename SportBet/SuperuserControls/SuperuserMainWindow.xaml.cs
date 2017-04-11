using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AutoMapper;
using SportBet.Models.Display;
using SportBet.Models.Registers;
using SportBet.Services.Contracts.Factories;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;
using SportBet.Subjects;
using SportBet.SuperuserControls.UserControls;
using SportBet.SuperuserControls.ViewModels;
using SportBet.WindowFactories;

namespace SportBet.SuperuserControls
{
    /// <summary>
    /// Interaction logic for SuperuserMainWindow.xaml
    /// </summary>
    public partial class SuperuserMainWindow : MainWindowBase
    {
        public SuperuserMainWindow(ServiceFactory factory, string login)
            : base(factory, login)
        {
            InitializeComponent();

            SetFooterMessage(true, String.Format("Welcome, {0} (superuser)", login));
        }

        private void RegisterBookmaker_Click(object sender, RoutedEventArgs e)
        {
            RegisterBookmaker();
        }
        private void RegisterBookmaker()
        {
            BookmakerRegisterViewModel viewModel = new BookmakerRegisterViewModel(new BookmakerRegisterModel());
            RegisterBookmakerControl control = new RegisterBookmakerControl(viewModel);

            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.BookmakerCreated += (s, ea) =>
            {
                BookmakerRegisterModel bookmaker = ea.Bookmaker;
                BookmakerRegisterDTO bookmakerDTO = Mapper.Map<BookmakerRegisterModel, BookmakerRegisterDTO>(bookmaker);
                ServiceMessage result;

                using (IAccountService service = factory.CreateAccountService())
                {
                    result = service.Register(bookmakerDTO);
                }

                if (result.IsSuccessful)
                    window.Close();

                SetFooterMessage(result.IsSuccessful, result.Message);
            };

            window.ShowDialog();
        }

        private void RegisterAdmin_Click(object sender, RoutedEventArgs e)
        {
            RegisterAdmin();
        }
        private void RegisterAdmin()
        {
            AdminRegisterViewModel viewModel = new AdminRegisterViewModel(new AdminRegisterModel());
            RegisterAdminControl control = new RegisterAdminControl(viewModel);

            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.AdminCreated += (s, ea) =>
            {
                AdminRegisterModel admin = ea.Admin;
                AdminRegisterDTO adminDTO = Mapper.Map<AdminRegisterModel, AdminRegisterDTO>(admin);
                ServiceMessage result;

                using (IAccountService service = factory.CreateAccountService())
                {
                    result = service.Register(adminDTO);
                }

                if (result.IsSuccessful)
                    window.Close();

                SetFooterMessage(result.IsSuccessful, result.Message);
            };

            window.ShowDialog();
        }

        private void RegisterAnalytic_Click(object sender, RoutedEventArgs e)
        {
            RegisterAnalytic();
        }
        private void RegisterAnalytic()
        {
            AnalyticRegisterViewModel viewModel = new AnalyticRegisterViewModel(new AnalyticRegisterModel());
            RegisterAnalyticControl control = new RegisterAnalyticControl(viewModel);

            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.AnalyticCreated += (s, ea) =>
            {
                AnalyticRegisterModel analytic = ea.Analytic;
                AnalyticRegisterDTO analyticDTO = Mapper.Map<AnalyticRegisterModel, AnalyticRegisterDTO>(analytic);
                ServiceMessage result;

                using (IAccountService service = factory.CreateAccountService())
                {
                    result = service.Register(analyticDTO);
                }

                if (result.IsSuccessful)
                    window.Close();

                SetFooterMessage(result.IsSuccessful, result.Message);
            };

            window.ShowDialog();
        }

        private void ManageAdmins_Click(object sender, RoutedEventArgs e)
        {
            ManageAdmins();
        }
        private void ManageAdmins()
        {
            DataServiceMessage<IEnumerable<AdminDisplayDTO>> resultMessage;
            using (IAdminService service = factory.CreateAdminService())
            {
                resultMessage = service.GetAll();
            }

            SetFooterMessage(resultMessage.IsSuccessful, resultMessage.Message);

            if (resultMessage.IsSuccessful)
            {
                IEnumerable<AdminDisplayDTO> adminDTOs = resultMessage.Data;
                IEnumerable<AdminDisplayModel> admins = adminDTOs
                    .Select(dto => Mapper.Map<AdminDisplayDTO, AdminDisplayModel>(dto));

                ManageAdminsViewModel viewModel = new ManageAdminsViewModel(admins);
                ManageAdminsControl control = new ManageAdminsControl(viewModel);
                Window window = WindowFactory.CreateByContentsSize(control);

                viewModel.AdminDeleted += (s, e) =>
                {
                    using (IAdminService service = factory.CreateAdminService())
                    {
                        AdminDisplayDTO deletedAdmin = Mapper.Map<AdminDisplayModel, AdminDisplayDTO>(e.Admin);
                        ServiceMessage result = service.Delete(deletedAdmin.Login);

                        SetFooterMessage(result.IsSuccessful, result.Message);

                        if (result.IsSuccessful)
                        {
                            DataServiceMessage<IEnumerable<AdminDisplayDTO>> serviceMessage = service.GetAll();
                            adminDTOs = serviceMessage.Data;
                            admins = adminDTOs
                                .Select(dto => Mapper.Map<AdminDisplayDTO, AdminDisplayModel>(dto));

                            viewModel.Refresh(admins);
                        }
                    }
                };

                window.ShowDialog();
            }
        }

        private void ManageAnalytics_Click(object sender, RoutedEventArgs e)
        {
            ManageAnalytics();
        }
        private void ManageAnalytics()
        {
            DataServiceMessage<IEnumerable<AnalyticDisplayDTO>> resultMessage;
            using (IAnalyticService service = factory.CreateAnalyticService())
            {
                resultMessage = service.GetAll();
            }

            SetFooterMessage(resultMessage.IsSuccessful, resultMessage.Message);

            if (resultMessage.IsSuccessful)
            {
                IEnumerable<AnalyticDisplayDTO> analyticDTOs = resultMessage.Data;
                IEnumerable<AnalyticDisplayModel> analytics = analyticDTOs
                    .Select(dto => Mapper.Map<AnalyticDisplayDTO, AnalyticDisplayModel>(dto));

                ManageAnalyticsViewModel viewModel = new ManageAnalyticsViewModel(analytics);
                ManageAnalyticsControl control = new ManageAnalyticsControl(viewModel);
                Window window = WindowFactory.CreateByContentsSize(control);

                viewModel.AnalyticDeleted += (s, e) =>
                {
                    using (IAnalyticService service = factory.CreateAnalyticService())
                    {
                        AnalyticDisplayDTO deletedAnalytic = Mapper.Map<AnalyticDisplayModel, AnalyticDisplayDTO>(e.Analytic);
                        ServiceMessage result = service.Delete(deletedAnalytic.Login);

                        SetFooterMessage(result.IsSuccessful, result.Message);

                        if (result.IsSuccessful)
                        {
                            DataServiceMessage<IEnumerable<AnalyticDisplayDTO>> serviceMessage = service.GetAll();
                            analyticDTOs = serviceMessage.Data;
                            analytics = analyticDTOs
                                .Select(dto => Mapper.Map<AnalyticDisplayDTO, AnalyticDisplayModel>(dto));

                            viewModel.Refresh(analytics);
                        }
                    }
                };

                window.ShowDialog();
            }
        }

        private void ManageBookmakers_Click(object sender, RoutedEventArgs e)
        {
            ManageBookmakers();
        }
        private void ManageBookmakers()
        {
            DataServiceMessage<IEnumerable<BookmakerDisplayDTO>> resultMessage;
            using (IBookmakerService service = factory.CreateBookmakerService())
            {
                resultMessage = service.GetAll();
            }

            SetFooterMessage(resultMessage.IsSuccessful, resultMessage.Message);

            if (resultMessage.IsSuccessful)
            {
                IEnumerable<BookmakerDisplayDTO> bookmakerDTOs = resultMessage.Data;
                IEnumerable<BookmakerDisplayModel> bookmakers = bookmakerDTOs
                    .Select(dto => Mapper.Map<BookmakerDisplayDTO, BookmakerDisplayModel>(dto));

                ManageBookmakersViewModel viewModel = new ManageBookmakersViewModel(bookmakers);
                ManageBookmakersControl control = new ManageBookmakersControl(viewModel);
                Window window = WindowFactory.CreateByContentsSize(control);

                viewModel.BookmakerDeleted += (s, e) =>
                {
                    using (IBookmakerService service = factory.CreateBookmakerService())
                    {
                        BookmakerDisplayDTO deletedBookmaker = Mapper.Map<BookmakerDisplayModel, BookmakerDisplayDTO>(e.Bookmaker);
                        ServiceMessage result = service.Delete(deletedBookmaker.Login);

                        SetFooterMessage(result.IsSuccessful, result.Message);

                        if (result.IsSuccessful)
                        {
                            DataServiceMessage<IEnumerable<BookmakerDisplayDTO>> serviceMessage = service.GetAll();
                            bookmakerDTOs = serviceMessage.Data;
                            bookmakers = bookmakerDTOs
                                .Select(dto => Mapper.Map<BookmakerDisplayDTO, BookmakerDisplayModel>(dto));

                            viewModel.Refresh(bookmakers);
                        }
                    }
                };

                window.ShowDialog();
            }
        }

        private void ManageClients_Click(object sender, RoutedEventArgs e)
        {
            ManageClients();
        }
        private void ManageClients()
        {
            ClientDisplayManager clientDisplayManager = new ClientDisplayManager(factory);
            clientDisplayManager.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);

            clientDisplayManager.DisplayClients();
        }

        private void SetFooterMessage(bool success, string message)
        {
            footer.StatusText = success ? "Success!" : "Fail or error!";
            footer.MessageText = message;
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            //MessageBox for question
            RaiseSignedOutEvent();
        }
    }
}
