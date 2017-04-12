using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Navigation;
using TrialApp.Services;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TrialApp.WinPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            try
            {
                this.InitializeComponent();

                this.NavigationCacheMode = NavigationCacheMode.Required;

                WebserviceTasks.SetDefaults("http://schemas.microsoft.com/appx/2010/manifest");

                LoadApplication(new TrialApp.App());
            }
            catch (System.Exception ex)
            {
                 LogFile(ex.Message).Wait();
            }
            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async Task LogFile(string log)
        {
            var folders = await KnownFolders.DocumentsLibrary.GetFoldersAsync();
            StorageFile resultfile = await folders.FirstOrDefault().CreateFileAsync(
    "TrialLog.txt",
    CreationCollisionOption.GenerateUniqueName);

            await FileIO.AppendTextAsync(resultfile, log + Environment.NewLine);
        }
    }
}
