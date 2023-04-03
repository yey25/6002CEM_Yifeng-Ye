using Shopping.App.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.App.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly UserService _userService;

        #region Property
        private string _account;

        public string Account
        { 
            get => _account; 
            set => SetProperty(ref _account, value);
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        #endregion


        #region Command

        private Command _loginCommand;

        public Command LoginCommand
            => _loginCommand ??= new (Login);


        private Command _signUpCommand;

        public Command SignUpCommand
            => _signUpCommand ??= new (SignUp);

        #endregion

        public LoginViewModel(UserService userService)
        {
            _userService = userService;
        }


        private async void Login()
        {
            var user = _userService.Login(Account, Password);

            if(user is null) 
            {
                DisplayAlert("Login Failed", "incorrect username or password");
                return;
            }

            await Shell.Current.GoToAsync("..");
            await Shell.Current.GoToAsync("//Home");
        }

        private async void SignUp()
        {
            await Shell.Current.GoToAsync("SignIn");
        }
    }
}
