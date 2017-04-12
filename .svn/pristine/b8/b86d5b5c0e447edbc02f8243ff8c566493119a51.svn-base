using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TrialApp.Windows
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
            Frame.Navigate(typeof(ExtendedSplash)); // call MainPage  
        }

        /// <summary>
        /// Display version number from Manifest
        /// </summary>
        private void PositionVersion()
        {
            XNamespace ns = "http://schemas.microsoft.com/appx/2010/manifest";
            var xElement = XDocument.Load("AppxManifest.xml").Root;
            if (xElement != null)
            {
                var version = xElement.Element(ns + "Identity")?.Attribute("Version").Value;
                txtVersion.Text = "Version: " + version?.Substring(0, version.LastIndexOf('.'));
            }
        }
    }
}
