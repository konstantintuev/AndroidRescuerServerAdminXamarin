using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace ServeAdmin
{
    public class AboutViewModel : BaseViewModel
    {
        public Boolean HomeWifi = false;

        public AboutViewModel()
        {
            Title = "About";

            HomeWifi = App.BackendUrl.Equals("http://192.168.100.200");

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri(App.BackendUrl)));
            TurnSwift = new Command(() => CloudDataStore.TurnServerOn());
        }

        public ICommand OpenWebCommand { get; }
        public ICommand TurnSwift { get; }
    }
}
