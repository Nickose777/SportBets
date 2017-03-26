﻿using SportBet.Models;
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
    }
}
