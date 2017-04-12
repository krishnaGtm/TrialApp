using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Content.Res;
using Android.OS;
using Android.Util;
using Java.IO;
using Java.Lang;
using Exception = System.Exception;
using File = System.IO.File;
using IOException = System.IO.IOException;

namespace TrialApp.Droid
{
    [Activity(Label = "TrialApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            CopyDatabase();
            LoadApplication(new App());
        }

        
        private void CopyDatabase()
        {
            var fileHelper = new FileHelper();
            var transDbPath = fileHelper.GetLocalFilePath("Transaction.db");
            var masterDbPath = fileHelper.GetLocalFilePath("Master.db");
            CopyDatabaseIfNotExists(transDbPath, "Transaction.db");
            CopyDatabaseIfNotExists(masterDbPath, "Master.db");
        }

        private  void CopyDatabaseIfNotExists(string dbPath, string dbFileName)
        {
            //File.Delete(dbPath);
            if (!File.Exists(dbPath))
            {
                using (var br = new BinaryReader(Application.Context.Assets.Open(dbFileName)))
                {
                    using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int length = 0;
                        while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, length);
                        }
                    }
                }
            }
        }

    }
}

