using SportBet.Models.Registers;
using SportBet.Services.Providers.BookmakerServices;
using SportBet.Services.Contracts.Factories;
using SportBet.Services.DTOModels;
using SportBet.Services.ResultTypes;
using SportBet.SuperuserControls.UserControls;
using SportBet.SuperuserControls.ViewModels;
using SportBet.WindowFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AutoMapper;
using SportBet.Models.Display;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.Contracts.Services;

namespace SportBet.SuperuserControls
{
    /// <summary>
    /// Interaction logic for SuperuserMainWindow.xaml
    /// </summary>
    public partial class SuperuserMainWindow : ILogoutWindow
    {
        public event EventHandler SignedOut;

        private readonly ServiceFactory factory;

        public SuperuserMainWindow(ServiceFactory factory, string login)
        {
            InitializeComponent();
            this.factory = factory;
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
                        ServiceMessage result = service.Delete(deletedBookmaker);

                        SetFooterMessage(result.IsSuccessful, result.Message);
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
            DataServiceMessage<IEnumerable<ClientDisplayDTO>> resultMessage;
            using (IClientService service = factory.CreateClientService())
            {
                resultMessage = service.GetAll();
            }

            SetFooterMessage(resultMessage.IsSuccessful, resultMessage.Message);

            if (resultMessage.IsSuccessful)
            {
                IEnumerable<ClientDisplayDTO> clientDTOs = resultMessage.Data;
                IEnumerable<ClientDisplayModel> clients = clientDTOs
                    .Select(dto => Mapper.Map<ClientDisplayDTO, ClientDisplayModel>(dto));

                ManageClientsViewModel viewModel = new ManageClientsViewModel(clients);
                ManageClientsControl control = new ManageClientsControl(viewModel);
                Window window = WindowFactory.CreateByContentsSize(control);

                viewModel.ClientDeleted += (s, e) =>
                {
                    using (IClientService service = factory.CreateClientService())
                    {
                        ClientDisplayDTO deletedClient = Mapper.Map<ClientDisplayModel, ClientDisplayDTO>(e.Client);
                        ServiceMessage result = service.Delete(deletedClient);

                        SetFooterMessage(result.IsSuccessful, result.Message);
                    }
                };

                window.ShowDialog();
            }
            else
            {
                MessageBox.Show(resultMessage.Message);
            }
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

        private void RaiseSignedOutEvent()
        {
            var handler = SignedOut;
            if (handler != null)
            {
                EventArgs e = new EventArgs();
                handler(this, e);
            }
        }
    }
}
