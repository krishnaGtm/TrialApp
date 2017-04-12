using TrialApp.Common;
using TrialApp.WinPhone;
using Xamarin.Forms;
using System;
using Windows.Storage;
using System.IO;
using System.Threading.Tasks;

[assembly: Dependency(typeof(Logger))]
namespace TrialApp.WinPhone
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
