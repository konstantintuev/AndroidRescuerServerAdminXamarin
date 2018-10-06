using System;
using Xamarin.Forms;

namespace ServeAdmin
{
    public partial class AboutPage : ContentPage
    {
        
        public AboutPage()
        {
            InitializeComponent();
            switchURL.IsToggled = App.BackendUrl == "http://192.168.100.200";
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            root.Children.Add(new PinchToZoomContainer
            {
                Content = new Image { Source = new UriImageSource
                    {
                        Uri = new Uri(App.BackendUrl + "/server/webcam"+"/" + Passwords.user + "/" + Passwords.password),
                        CachingEnabled = false
                    }
                }
            });
        }

        void Handle_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value) { App.BackendUrl = "http://192.168.100.200"; } else { App.BackendUrl = "https://androidrescuer.cf"; }
            CloudDataStore.Refresh();
        }
    }
}
