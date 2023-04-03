using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Maui;
using Shopping.App.Models;
using Shopping.App.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.App.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        private readonly UserService _userService;

        private readonly ImageSource _defaultHeadPortrait
            = ImageSource.FromFile("head_portrait.png");

        #region Property

        private string _account;

        public string Account
        {
            get => _account;
            set => SetProperty(ref _account, value);
        }

        public ImageSource HeadPortrait
            =>  _defaultHeadPortrait;


        private bool _isLightTheme;

        public bool IsLightTheme
        {
            get => _isLightTheme;
            set
            {
                _isLightTheme = value;
                App.Current.UserAppTheme = _isLightTheme
                                        ? AppTheme.Light
                                        : AppTheme.Dark;


                OnPropertyChanged();
            }
        }

        private bool _isLogin;

        public bool IsLogin
        {
            get => _isLogin;
            set
            {
                _isLogin = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsLogOut));
            }
        }


        public bool IsLogOut => !_isLogin;


        private bool _isInitData = false;

        public bool IsInitData
        {
            get => _isInitData;
            set
            {
                _isInitData = value;
                Preferences.Set("InitData", value);
                OnPropertyChanged();
            }
        }
        #endregion


        #region Command

        private Command _loginCommand;

        public Command LoginCommand
            => _loginCommand ??= new(Login);


        private Command _logoutCommand;

        public Command LogOutCommand
            => _logoutCommand ??= new(LogOut);
        #endregion


        public UserViewModel(UserService userService)
        {
            _userService = userService;

            IsLightTheme = App.Current.UserAppTheme == AppTheme.Unspecified
                          ? App.Current.PlatformAppTheme == AppTheme.Light
                          : App.Current.UserAppTheme == AppTheme.Light;

            IsInitData = Preferences.Get("InitData", false);
            RefreshData();
        }

        public void RefreshData()
        {
            IsLogin = _userService.IsLogIn;
            if (IsLogin)
                Account = _userService.Account;
        }


        private async void Login()
        {
            await Shell.Current.GoToAsync("//Home/Login");
        }


        private void LogOut()
        {
            _userService.LogOut();
            Account = string.Empty;
            IsLogin = false;
        }
    }
}
