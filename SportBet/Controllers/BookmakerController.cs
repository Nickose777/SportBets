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

namespace SportBet.Controllers
{
    class BookmakerController : SubjectBase, ISubject
    {
        private readonly FacadeBase<BookmakerDisplayModel> facade;

        public BookmakerController(ServiceFactory factory, FacadeBase<BookmakerDisplayModel> facade)
            : base(factory)
        {
            this.facade = facade;
            facade.ReceivedMessage += (s, e) => RaiseReceivedMessageEvent(s, e);
        }

        public void RegisterBookmaker()
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

        public void DisplayBookmakers()
        {
            ManageBookmakersViewModel viewModel = new ManageBookmakersViewModel(this, facade);
            ManageBookmakersControl control = new ManageBookmakersControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.BookmakerDeleted += (s, e) =>
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

            window.Show();
        }
    }
}
