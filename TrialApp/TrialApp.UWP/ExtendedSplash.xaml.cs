using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TrialApp.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExtendedSplash : Page
    {
        public ExtendedSplash()
        {
            this.InitializeComponent();
            PositionVersion();
            ExtendeSplashScreen();
        }

        async void ExtendeSplashScreen()
        {
            await Task.Delay(TimeSpan.FromSeconds(6)); // set your desired delay  
            Frame.Navigate(typeof(MainPage)); // call MainPage  
        }

        /// <summary>
        /// Display version number from Manifest
        /// </summary>
        private void PositionVersion()
        {
            XNamespace ns = "http://schemas.microsoft.com/appx/manifest/foundation/windows10";
            var xElement = XDocument.Load("AppxManifest.xml").Root;
            if (xElement != null)
            {
                var version = xElement.Element(ns + "Identity")?.Attribute("Version").Value;
                txtVersion.Text = "Version: " + version?.Substring(0, version.LastIndexOf('.'));
            }
        }
    }
}
