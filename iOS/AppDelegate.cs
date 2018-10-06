using Foundation;
using UIKit;

namespace ServeAdmin.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        App app;
        public override bool FinishedLaunching(UIApplication app1, NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();
            app = new App();
            LoadApplication(app);

            return base.FinishedLaunching(app1, options);
        }

        public override bool OpenUrl(UIApplication app1, NSUrl url1, string sourceApp, NSObject annotation)
        {
            string url = url1.ToString();
            if (url.Contains("support")) {
                app.Support();
            } else if (url.Contains("error")) {
                app.Error();
            }
            return true;
        }

    }
}
