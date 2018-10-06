using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Net.Wifi;

namespace ServeAdmin.Droid
{
    [IntentFilter(new[] { Intent.ActionView },
              Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
              DataScheme = "serveadmin")]
    [Activity(Label = "ServeAdmin.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            WifiManager wifi = (WifiManager)GetSystemService(Context.WifiService);
            String ssid = wifi.ConnectionInfo?.SSID;
            Console.WriteLine("ssid: " + ssid);

            if (ssid.Equals("\"M-Tel_1963\""))
            {
                App.BackendUrl = "http://192.168.100.200";
            }

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            String url = Intent?.Data?.Host ?? "";
            App app = new App();
            if (url.Contains("support"))
            {
                app.Support();
            }
            else if (url.Contains("error"))
            {
                app.Error();
            }

            LoadApplication(app);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}
