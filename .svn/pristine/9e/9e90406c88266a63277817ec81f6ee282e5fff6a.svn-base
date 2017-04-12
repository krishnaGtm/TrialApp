using Windows.Storage;
using TrialApp.Common;
using TrialApp.Windows;
using Xamarin.Forms;
using System;
using System.Threading.Tasks;

[assembly: Dependency(typeof(Logger))]
namespace TrialApp.Windows
{
   public class Logger : ILogger
    {
        public async Task Log(string text)
        {
            var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Log", CreationCollisionOption.OpenIfExists);
            if (folder != null)
            {
                StorageFile resultfile = await folder.CreateFileAsync("TrialLog.txt", CreationCollisionOption.OpenIfExists);
                await FileIO.AppendTextAsync(resultfile, text + Environment.NewLine);
            }
        }
    }
}
