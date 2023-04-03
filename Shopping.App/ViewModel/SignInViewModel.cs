using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shopping.App.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.App.ViewModel
{
    public class SignInViewModel : BaseViewModel
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

        private string _confirm;

        public string Confirm
        {
            get => _confirm;
            set => SetProperty(ref _confirm, value);
        }
        #endregion


        #region Command

        private Command _loginCommand;

        public Command LoginCommand
            => _loginCommand ??= new(Login);


        private Command _signInCommand;

        public Command SignInCommand
            => _signInCommand ??= new(SignIn);

        #endregion

        public SignInViewModel(UserService userService)
        {
            _userService = userService; ;
        }

        private async void Login()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void SignIn()
        {
            if(Password != Confirm)
            {
                DisplayAlert("Failed", "The passwords do not match.");
                return;
            }

            var user = _userService.SignIn(Account, Password);

            if(user is null)
            {
                DisplayAlert("Failed", "Sign In Failed");
                return;
            }

            await Shell.Current.GoToAsync("//Home");
        }
    }
}
