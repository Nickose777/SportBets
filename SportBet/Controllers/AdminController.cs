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
    class AdminController : SubjectBase, ISubject, IAdminController
    {
        private readonly FacadeBase<AdminDisplayModel> facade;

        public AdminController(ServiceFactory factory, FacadeBase<AdminDisplayModel> facade)
            : base(factory)
        {
            this.facade = facade;
            facade.ReceivedMessage += RaiseReceivedMessageEvent;
        }

        public void Register()
        {
            AdminRegisterViewModel viewModel = new AdminRegisterViewModel(new AdminRegisterModel());
            RegisterAdminControl control = new RegisterAdminControl(viewModel);

            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.AdminCreated += (s, e) =>
            {
                AdminRegisterModel admin = e.Admin;
                AdminRegisterDTO adminDTO = Mapper.Map<AdminRegisterModel, AdminRegisterDTO>(admin);

                using (IAccountService service = factory.CreateAccountService())
                {
                    ServiceMessage serviceMessage = service.Register(adminDTO);
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
            ManageAdminsViewModel viewModel = new ManageAdminsViewModel(this, facade);
            ManageAdminsControl control = new ManageAdminsControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.AdminDeleted += (s, e) =>
            {
                using (IAdminService service = factory.CreateAdminService())
                {
                    AdminDisplayDTO deletedAdmin = Mapper.Map<AdminDisplayModel, AdminDisplayDTO>(e.Admin);
                    ServiceMessage serviceMessage = service.Delete(deletedAdmin.Login);

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
