using System.IO;
using Windows.Storage;
using TrialApp.Common;
using TrialApp.UWP;
using Xamarin.Forms;
using System.Threading.Tasks;
using System;

[assembly: Dependency(typeof(Logger))]
namespace TrialApp.UWP
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
