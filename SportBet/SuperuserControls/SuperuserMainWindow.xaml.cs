using SportBet.Models.Registers;
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

namespace SportBet.SuperuserControls
{
    /// <summary>
    /// Interaction logic for SuperuserMainWindow.xaml
    /// </summary>
    public partial class SuperuserMainWindow : ILogoutWindow
    {
        public event EventHandler SignedOut;

        private readonly ServiceFactory factory;

        public SuperuserMainWindow(ServiceFactory factory)
        {
            InitializeComponent();
            this.factory = factory;
        }

        private void RegisterBookmaker_Click(object sender, RoutedEventArgs e)
        {
            BookmakerRegisterViewModel viewModel = new BookmakerRegisterViewModel(new BookmakerRegisterModel());
            RegisterBookmakerControl control = new RegisterBookmakerControl(viewModel);

            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.BookmakerCreated += (s, ea) =>
            {
                BookmakerRegisterModel bookmaker = ea.Bookmaker;
                BookmakerRegisterDTO bookmakerDTO = Mapper.Map<BookmakerRegisterModel, BookmakerRegisterDTO>(bookmaker);

                var service = factory.CreateAccountService();
                AuthResult result = service.Register(bookmakerDTO);

                string message;
                if (result.IsSuccessful)
                {
                    message = "Successfully registered new bookmaker!";
                    window.Close();
                }
                else
                {
                    message = result.Message;
                }

                MessageBox.Show(message);
            };

            window.ShowDialog();
        }

        private void ManageBookmakers_Click(object sender, RoutedEventArgs e)
        {
            ManageBookmakers();
        }
        private void ManageBookmakers()
        {
            //TODO
            //get from service
            List<BookmakerDisplayModel> bookmakers = new List<BookmakerDisplayModel>()
            {
                new BookmakerDisplayModel { FirstName = "FirstName1", LastName = "LastName1", PhoneNumber = "PhoneNumber1", Login = "Login1"},
                new BookmakerDisplayModel { FirstName = "FirstName2", LastName = "LastName2", PhoneNumber = "PhoneNumber2", Login = "Login2"},
                new BookmakerDisplayModel { FirstName = "FirstName3", LastName = "LastName3", PhoneNumber = "PhoneNumber3", Login = "Login3"}
            };
            ManageBookmakersViewModel viewModel = new ManageBookmakersViewModel(bookmakers);
            ManageBookmakersControl control = new ManageBookmakersControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.BookmakerDeleted += (s, e) =>
            {
                //TODO
                //service.delete

                bookmakers.Remove(e.Bookmaker);
                viewModel.Refresh(bookmakers);
            };

            window.ShowDialog();
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
