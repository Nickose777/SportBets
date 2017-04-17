using System.Windows;
using SportBet.CommonControls.ChangePassword;
using SportBet.Models;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;
using SportBet.Contracts.Controllers;

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
            ChangePasswordControl control = new ChangePasswordControl(login);
            Window window = WindowFactory.CreateByContentsSize(control);

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

            window.ShowDialog();
        }
    }
}
