using System.IO;
using Windows.Storage;
using TrialApp.Common;
using TrialApp.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace TrialApp.UWP
{
   public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
