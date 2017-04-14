using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AutoMapper;
using SportBet.Models.Display;
using SportBet.Models.Registers;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;
using SportBet.Controllers;
using SportBet.SuperuserControls.UserControls;
using SportBet.SuperuserControls.ViewModels;
using SportBet.WindowFactories;
using SportBet.Facades;

namespace SportBet.SuperuserControls
{
    /// <summary>
    /// Interaction logic for SuperuserMainWindow.xaml
    /// </summary>
    public partial class SuperuserMainWindow : MainWindowBase
    {
        private readonly ClientController clientController;

        public SuperuserMainWindow(ServiceFactory factory, string login)
            : base(factory, login)
        {
            InitializeComponent();

            clientController = new ClientController(factory, new ClientFacade(factory));
            clientController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);

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

                using (IAccountService service = factory.CreateAccountService())
                {
                    ServiceMessage serviceMessage = service.Register(bookmakerDTO);
                    SetFooterMessage(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                        window.Close();
                }
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

                using (IAccountService service = factory.CreateAccountService())
                {
                    ServiceMessage serviceMessage = service.Register(adminDTO);
                    SetFooterMessage(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                        window.Close();
                }
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

                using (IAccountService service = factory.CreateAccountService())
                {
                    ServiceMessage serviceMessage = service.Register(analyticDTO);
                    SetFooterMessage(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                        window.Close();
                }
            };

            window.ShowDialog();
        }

        private void ManageAdmins_Click(object sender, RoutedEventArgs e)
        {
            ManageAdmins();
        }
        private void ManageAdmins()
        {
            using (IAdminService service = factory.CreateAdminService())
            {
                DataServiceMessage<IEnumerable<AdminDisplayDTO>> serviceMessage = service.GetAll();
                SetFooterMessage(serviceMessage.IsSuccessful, serviceMessage.Message);

                if (serviceMessage.IsSuccessful)
                {
                    IEnumerable<AdminDisplayDTO> adminDTOs = serviceMessage.Data;
                    IEnumerable<AdminDisplayModel> admins = adminDTOs
                        .Select(dto => Mapper.Map<AdminDisplayDTO, AdminDisplayModel>(dto));
                    DisplayAdmins(admins);
                }
            }
        }

        private void DisplayAdmins(IEnumerable<AdminDisplayModel> admins)
        {
            ManageAdminsViewModel viewModel = new ManageAdminsViewModel(admins);
            ManageAdminsControl control = new ManageAdminsControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.AdminDeleted += (s, e) =>
            {
                using (IAdminService service = factory.CreateAdminService())
                {
                    AdminDisplayDTO deletedAdmin = Mapper.Map<AdminDisplayModel, AdminDisplayDTO>(e.Admin);
                    ServiceMessage serviceMessage = service.Delete(deletedAdmin.Login);

                    SetFooterMessage(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        DataServiceMessage<IEnumerable<AdminDisplayDTO>> dataServiceMessage = service.GetAll();
                        IEnumerable<AdminDisplayDTO> adminDTOs = dataServiceMessage.Data;
                        admins = adminDTOs
                            .Select(dto => Mapper.Map<AdminDisplayDTO, AdminDisplayModel>(dto));

                        viewModel.Refresh(admins);
                    }
                }
            };

            window.ShowDialog();
        }

        private void ManageAnalytics_Click(object sender, RoutedEventArgs e)
        {
            ManageAnalytics();
        }
        private void ManageAnalytics()
        {
            using (IAnalyticService service = factory.CreateAnalyticService())
            {
                DataServiceMessage<IEnumerable<AnalyticDisplayDTO>> serviceMessage = service.GetAll();
                SetFooterMessage(serviceMessage.IsSuccessful, serviceMessage.Message);

                if (serviceMessage.IsSuccessful)
                {
                    IEnumerable<AnalyticDisplayDTO> analyticDTOs = serviceMessage.Data;
                    IEnumerable<AnalyticDisplayModel> analytics = analyticDTOs
                        .Select(dto => Mapper.Map<AnalyticDisplayDTO, AnalyticDisplayModel>(dto));
                    DisplayAnalytics(analytics);
                }
            }
        }

        private void DisplayAnalytics(IEnumerable<AnalyticDisplayModel> analytics)
        {
            ManageAnalyticsViewModel viewModel = new ManageAnalyticsViewModel(analytics);
            ManageAnalyticsControl control = new ManageAnalyticsControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.AnalyticDeleted += (s, e) =>
            {
                using (IAnalyticService service = factory.CreateAnalyticService())
                {
                    AnalyticDisplayDTO deletedAnalytic = Mapper.Map<AnalyticDisplayModel, AnalyticDisplayDTO>(e.Analytic);
                    ServiceMessage serviceMessage = service.Delete(deletedAnalytic.Login);

                    SetFooterMessage(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        DataServiceMessage<IEnumerable<AnalyticDisplayDTO>> dataServiceMessage = service.GetAll();
                        IEnumerable<AnalyticDisplayDTO> analyticDTOs = dataServiceMessage.Data;
                        analytics = analyticDTOs
                            .Select(dto => Mapper.Map<AnalyticDisplayDTO, AnalyticDisplayModel>(dto));

                        viewModel.Refresh(analytics);
                    }
                }
            };

            window.ShowDialog();
        }

        private void ManageBookmakers_Click(object sender, RoutedEventArgs e)
        {
            ManageBookmakers();
        }
        private void ManageBookmakers()
        {
            using (IBookmakerService service = factory.CreateBookmakerService())
            {
                DataServiceMessage<IEnumerable<BookmakerDisplayDTO>> serviceMessage = service.GetAll();
                SetFooterMessage(serviceMessage.IsSuccessful, serviceMessage.Message);

                if (serviceMessage.IsSuccessful)
                {
                    IEnumerable<BookmakerDisplayDTO> bookmakerDTOs = serviceMessage.Data;
                    IEnumerable<BookmakerDisplayModel> bookmakers = bookmakerDTOs
                        .Select(dto => Mapper.Map<BookmakerDisplayDTO, BookmakerDisplayModel>(dto));
                    DisplayBookmakers(bookmakers);
                }
            }
        }

        private void DisplayBookmakers(IEnumerable<BookmakerDisplayModel> bookmakers)
        {
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
                        IEnumerable<BookmakerDisplayDTO> bookmakerDTOs = serviceMessage.Data;
                        bookmakers = bookmakerDTOs
                            .Select(dto => Mapper.Map<BookmakerDisplayDTO, BookmakerDisplayModel>(dto));

                        viewModel.Refresh(bookmakers);
                    }
                }
            };
        }

        private void ManageClients_Click(object sender, RoutedEventArgs e)
        {
            clientController.DisplayClientsForAdmin();
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
