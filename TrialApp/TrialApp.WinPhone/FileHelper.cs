using System.IO;
using Windows.Storage;
using TrialApp.Common;
using TrialApp.WinPhone;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace TrialApp.WinPhone
{
   public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
