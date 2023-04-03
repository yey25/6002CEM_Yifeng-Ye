using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.App.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T prop, T value, [CallerMemberName] string propertyName = null)
        {
            prop = value;
            OnPropertyChanged(propertyName);
        }

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        protected async void DisplayAlert(string title, string message, string cancel = "Close")
        {
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
