﻿using System;
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
    class BookmakerController : SubjectBase, ISubject, IBookmakerController
    {
        private readonly FacadeBase<BookmakerDisplayModel> facade;

        public BookmakerController(ServiceFactory factory, FacadeBase<BookmakerDisplayModel> facade)
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
            BookmakerRegisterViewModel viewModel = new BookmakerRegisterViewModel(new BookmakerRegisterModel());
            RegisterBookmakerControl control = new RegisterBookmakerControl(viewModel);

            viewModel.BookmakerCreated += (s, ea) =>
            {
                BookmakerRegisterModel bookmaker = ea.Bookmaker;
                BookmakerRegisterDTO bookmakerDTO = Mapper.Map<BookmakerRegisterModel, BookmakerRegisterDTO>(bookmaker);

                using (IAccountService service = factory.CreateAccountService())
                {
                    ServiceMessage serviceMessage = service.Register(bookmakerDTO);
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
            ManageBookmakersViewModel viewModel = new ManageBookmakersViewModel(this, facade);
            ManageBookmakersControl control = new ManageBookmakersControl(viewModel);

            viewModel.BookmakerSelectRequest += (s, e) => Edit(e.Bookmaker);
            viewModel.BookmakerDeleteRequest += (s, e) =>
            {
                using (IBookmakerService service = factory.CreateBookmakerService())
                {
                    BookmakerDisplayDTO deletedBookmaker = Mapper.Map<BookmakerDisplayModel, BookmakerDisplayDTO>(e.Bookmaker);
                    ServiceMessage serviceMessage = service.Delete(deletedBookmaker.Login);

                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        Notify();
                    }
                }
            };

            return control;
        }

        private void Edit(BookmakerDisplayModel bookmakerDisplayModel)
        {
            BookmakerEditModel bookmakerEditModel = Mapper.Map<BookmakerDisplayModel, BookmakerEditModel>(bookmakerDisplayModel);

            BookmakerInfoViewModel viewModel = new BookmakerInfoViewModel(bookmakerEditModel);
            BookmakerInfoControl control = new BookmakerInfoControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.BookmakerEdited += (s, e) =>
            {
                bookmakerEditModel = e.Bookmaker;
                BookmakerEditDTO bookmakerEditDTO = Mapper.Map<BookmakerEditModel, BookmakerEditDTO>(bookmakerEditModel);

                using (IBookmakerService service = factory.CreateBookmakerService())
                {
                    ServiceMessage serviceMessage = service.Update(bookmakerEditDTO, bookmakerDisplayModel.Login);
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
