using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TrialApp.Services;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace TrialApp.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            CopyDatabase();

            WebserviceTasks.SetDefaults("http://schemas.microsoft.com/appx/manifest/foundation/windows10");

            LoadApplication(new TrialApp.App());
        }

        private async Task CopyDatabase()
        {
            bool isDatabaseExisting = false;
            bool isDatabaseExisting1 = true;


            try
            {
                try
                {
                    StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync("Master.db");
                    StorageFile storageFile1 = await ApplicationData.Current.LocalFolder.GetFileAsync("Transaction.db");

                    isDatabaseExisting = true;
                    isDatabaseExisting1 = true;
                }
                catch
                {
                    isDatabaseExisting = false;
                    isDatabaseExisting1 = false;
                }

                if (!isDatabaseExisting)
                {
                    StorageFile databaseFile = await Package.Current.InstalledLocation.GetFileAsync("Transaction.db");
                    await databaseFile.CopyAsync(ApplicationData.Current.LocalFolder);
                }


                if (!isDatabaseExisting1)
                {

                    StorageFile databaseFile1 = await Package.Current.InstalledLocation.GetFileAsync("Master.db");
                    await databaseFile1.CopyAsync(ApplicationData.Current.LocalFolder);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
