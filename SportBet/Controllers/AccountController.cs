using System.Windows;
using SportBet.CommonControls.ChangePassword;
using SportBet.Models;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;
using SportBet.Contracts.Controllers;
using System.Windows.Controls;

namespace SportBet.Controllers
{
    class AccountController : ControllerBase, IAccountController
    {
        private readonly string login;

        public AccountController(ServiceFactory factory, string login)
            : base(factory)
        {
            this.login = login;
        }

        public void ChangePassword()
        {
            UserControl control = GetPasswordControl();
            Window window = WindowFactory.CreateByContentsSize(control);

            window.ShowDialog();
        }

        public UserControl GetPasswordControl()
        {
            ChangePasswordControl control = new ChangePasswordControl(login);

            control.PasswordChanged += (s, e) =>
            {
                ChangePasswordModel model = e.ChangePasswordModel;
                if (model.NewPassword == model.ConfirmPassword)
                {
                    ChangePasswordDTO changePasswordDTO = new ChangePasswordDTO
                    {
                        Login = model.Login,
                        OldPassword = model.OldPassword,
                        NewPassword = model.NewPassword
                    };

                    using (IAccountService service = factory.CreateAccountService())
                    {
                        ServiceMessage serviceMessage = service.ChangePassword(changePasswordDTO);
                        RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                        if (serviceMessage.IsSuccessful)
                        {
                            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                            Application.Current.Shutdown();
                        }
                    }
                }
                else
                {
                    RaiseReceivedMessageEvent(false, "Passwords are not same");
                }
            };

            return control;
        }
    }
}
