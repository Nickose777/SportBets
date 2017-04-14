using System.Windows;
using SportBet.CommonControls.ChangePassword;
using SportBet.Models;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;

namespace SportBet.Controllers
{
    //TODO
    //Think about login parameter
    class AccountController : ControllerBase
    {
        public AccountController(ServiceFactory factory)
            : base(factory) { }

        public void ChangePassword(string login)
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
