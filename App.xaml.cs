using System;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace ServeAdmin
{
    public partial class App : Application
    {
        public static string BackendUrl = "https://androidrescuer.cf";

        public App()
        {
            InitializeComponent();

            Console.WriteLine("init");

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new MainPage();
            else
                MainPage = new NavigationPage(new MainPage());
        }

        public void Support() {
            if (Device.RuntimePlatform == Device.iOS)
                (MainPage as MainPage).Support();
            else
                ((MainPage as NavigationPage).RootPage as MainPage).Support();
        }

        public void Error()
        {
            if (Device.RuntimePlatform == Device.iOS)
                (MainPage as MainPage).Error();
            else
                ((MainPage as NavigationPage).RootPage as MainPage).Error();
        }
    }
}
