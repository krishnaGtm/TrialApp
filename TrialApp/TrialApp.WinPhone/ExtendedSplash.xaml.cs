﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TrialApp.WinPhone
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
            try
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1000)); // set your desired delay  
                Frame.Navigate(typeof(MainPage)); // call MainPage
            }
            catch (Exception ex)
            {

               await LogFile(ex.Message);
            }
              
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        private  async Task LogFile(string log)
        {
            var folders = await KnownFolders.DocumentsLibrary.GetFoldersAsync();
            StorageFile resultfile = await folders.FirstOrDefault().CreateFileAsync(
    "TrialLog.txt",
    CreationCollisionOption.GenerateUniqueName);

            await FileIO.AppendTextAsync(resultfile, log + Environment.NewLine);
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}