using SportBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet
{
    public class RegisterObservableObject : ObservableObject
    {
        private readonly RegisterBaseModel model;

        public RegisterObservableObject(RegisterBaseModel model)
        {
            this.model = model;
        }

        public string Login
        {
            get { return model.Login; }
            set
            {
                model.Login = value;
                RaisePropertyChangedEvent("Login");
            }
        }
        public string Password
        {
            get { return model.Password; }
            set
            {
                model.Password = value;
                RaisePropertyChangedEvent("Password");
            }
        }
        public string ConfirmPassword
        {
            get { return model.ConfirmPassword; }
            set
            {
                model.ConfirmPassword = value;
                RaisePropertyChangedEvent("ConfirmPassword");
            }
        }

        protected bool CanCreateModel()
        {
            return
                !String.IsNullOrEmpty(model.Login) &&
                !String.IsNullOrEmpty(model.Password) &&
                !String.IsNullOrEmpty(model.ConfirmPassword) &&
                model.Login.Length > 3 &&
                model.Password == model.ConfirmPassword;
        }
    }
}
