using System;
using System.Windows;
using AutoMapper;
using SportBet.Contracts;
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
using SportBet.Models.Edit;
using SportBet.Services.DTOModels.Edit;

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
            AdminRegisterViewModel viewModel = new AdminRegisterViewModel(new AdminRegisterModel());
            RegisterAdminControl control = new RegisterAdminControl(viewModel);

            viewModel.AdminCreated += (s, e) =>
            {
                AdminRegisterDTO admin = Mapper.Map<AdminRegisterModel, AdminRegisterDTO>(e.Admin);

                using (IAccountService service = factory.CreateAccountService())
                {
                    ServiceMessage serviceMessage = service.Register(admin);
                    RaiseReceivedMessageEvent(serviceMessage);

                    if (serviceMessage.IsSuccessful)
                    {
                        Notify();
                    }
                }
            };

            return control;
        }

        public UIElement GetDisplayElement()
        {
            ManageAdminsViewModel viewModel = new ManageAdminsViewModel(this, facade);
            ManageAdminsControl control = new ManageAdminsControl(viewModel);

            viewModel.AdminEdit += (s, e) => Edit(e.Admin);
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

            return control;
        }

        private void Edit(AdminDisplayModel adminDisplayModel)
        {
            AdminEditModel admin = new AdminEditModel
            {
                FirstName = adminDisplayModel.FirstName,
                LastName = adminDisplayModel.LastName,
                PhoneNumber = adminDisplayModel.PhoneNumber,
                Login = adminDisplayModel.Login
            };

            AdminInfoViewModel viewModel = new AdminInfoViewModel(admin);
            AdminInfoControl control = new AdminInfoControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.AdminEdited += (s, e) =>
            {
                AdminEditModel adminEditModel = e.Admin;
                AdminEditDTO adminEditDTO = Mapper.Map<AdminEditModel, AdminEditDTO>(adminEditModel);

                using (IAdminService service = factory.CreateAdminService())
                {
                    ServiceMessage serviceMessage = service.Update(adminEditDTO);
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
