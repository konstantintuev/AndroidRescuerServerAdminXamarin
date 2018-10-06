using System;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace ServeAdmin
{
    public partial class NewItemPage : ContentPage
    {
        
        bool Check = true;
        public string Header { get; set; }
        public string Desc { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        void Handle_Toggled(object sender, ToggledEventArgs e)
        {
            Check = e.Value;
        }

        async void Send_Clicked(object sender, EventArgs e)
        {
            if (Header != null && Desc != null && !Header.Equals("") && !Desc.Equals("") && CrossConnectivity.Current.IsConnected)
            {

                var response = await BaseViewModel.DataStore.SendNotification(Header, Desc, Check);

                await DisplayAlert("Result", response, "OK");
            }
        }
    }
}
