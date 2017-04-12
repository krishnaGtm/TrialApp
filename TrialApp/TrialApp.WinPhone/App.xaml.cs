using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace TrialApp.WinPhone
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        private TransitionCollection transitions;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending; 
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            try
            {
                await CopyDatabase();
            }
            catch (Exception ex)
            {
                await LogFile(ex.Message);
            }
            
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                // TODO: change this value to a cache size that is appropriate for your application
                rootFrame.CacheSize = 1;

                Xamarin.Forms.Forms.Init(e);

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                //Hide status bar and navigation bar
                await StatusBar.GetForCurrentView().HideAsync();
                ApplicationView.GetForCurrentView().SuppressSystemOverlays = true;

                // Removes the turnstile navigation for startup.
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;

                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(ExtendedSplash), e.Arguments))
                {
                   await LogFile("Failed to create initial page");
                    //throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Details about the navigation event.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        private async Task LogFile(string log)
        {
            var folders = await KnownFolders.DocumentsLibrary.GetFoldersAsync();
            if (folders != null)
            {
                StorageFile resultfile = await folders.FirstOrDefault().CreateFileAsync(
        "TrialLog.txt",
        CreationCollisionOption.OpenIfExists);

                await FileIO.AppendTextAsync(resultfile, log + Environment.NewLine); }
                
            
        }

        private async Task CopyDatabase()
        {
            bool isDatabaseExisting = false;
            bool isDatabaseExisting1 = false;


            try
            {
             
                try
                {
                    StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync("Master.db");
                    StorageFile storageFile1 = await ApplicationData.Current.LocalFolder.GetFileAsync("Transaction.db");
                    //await storageFile.DeleteAsync();
                    //await storageFile1.DeleteAsync();
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
