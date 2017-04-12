using TrialApp.Common;
using Xamarin.Forms;

namespace TrialApp
{
    public partial class App : Application
    {
        public static string Token = "";
        public static NavigationPage MainNavigation;
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new Views.MainPage(""));
            MainPage = MainNavigation = new NavigationPage(new Views.MainPage());
        }

       
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
