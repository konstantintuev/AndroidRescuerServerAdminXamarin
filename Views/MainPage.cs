using System;
using BottomBar.XamarinForms;
using Xamarin.Forms;

namespace ServeAdmin
{
    public class MainPage : BottomBarPage
    {
        Page itemsPage, sendPage, aboutPage = null;

        public MainPage()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    itemsPage = new NavigationPage(new ItemsPage())
                    {
                        Title = "Errors"
                    };

                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "About"
                    };

                    sendPage = new NavigationPage(new NewItemPage())
					{
						Title = "Send"
					};
                    break;
                default:
                    itemsPage = new ItemsPage()
                    {
                        Title = "Errors"
                    };

                    aboutPage = new AboutPage()
                    {
                        Title = "About"
                    };

					sendPage = new NewItemPage()
					{
						Title = "Send"
					};
                    break;
            }
			itemsPage.Icon = (FileImageSource)ImageSource.FromFile("tab_feed.png");
			sendPage.Icon = (FileImageSource)ImageSource.FromFile("tab_send.png");
			aboutPage.Icon = (FileImageSource)ImageSource.FromFile("tab_about.png");
            BottomBarPageExtensions.SetTabColor(itemsPage, Color.Blue);
            BottomBarPageExtensions.SetTabColor(sendPage, Color.Blue);
            BottomBarPageExtensions.SetTabColor(aboutPage, Color.Blue);

            Children.Add(itemsPage);
            Children.Add(sendPage);
            Children.Add(aboutPage);

            Title = Children[0].Title;


		}

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }

        public void Support()
        {
            if (Device.RuntimePlatform != Device.iOS)
                (itemsPage as ItemsPage).Support();
            else
                ((itemsPage as NavigationPage).RootPage as ItemsPage).Support();
        }

        public void Error()
        {
            if (Device.RuntimePlatform != Device.iOS)
                (itemsPage as ItemsPage).Error();
            else
                ((itemsPage as NavigationPage).RootPage as ItemsPage).Error();
        }
    }
}
