using System;
using System.IO;
using TrialApp.Common;
using TrialApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(Logger))]
namespace TrialApp.Droid
{
   public class Logger : ILogger
    {
        public async Task Log(string text)
        {
            var folder = await Environment.SpecialFolder.Personal.CreateFolderAsync("Log", CreationCollisionOption.OpenIfExists);
            if (folder != null)
            {
                StorageFile resultfile = await folder.CreateFileAsync("TrialLog.txt", CreationCollisionOption.OpenIfExists);
                await FileIO.AppendTextAsync(resultfile, text + Environment.NewLine);
            }
        }
    }
}
