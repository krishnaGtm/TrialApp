﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.IO;
using System.Reflection;

namespace TrialApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            CopyDatabase();

            global::Xamarin.Forms.Forms.Init();
            MR.Gestures.iOS.Settings.LicenseKey = "F5VH-CW8U-LVNZ-7FSZ-AWJX-7UPC-ZBRB-PF3B-YFAK-XGZ5-X4EW-2HGA-U4U2";
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
        private void CopyDatabase()
        {
            var fileHelper = new FileHelper();
            var transDbPath = fileHelper.GetLocalFilePath("Transaction.db");
            var masterDbPath = fileHelper.GetLocalFilePath("Master.db");
            var masterSource = Path.Combine(NSBundle.MainBundle.BundlePath, "Master.db");
            var transactionSource = Path.Combine(NSBundle.MainBundle.BundlePath, "Transaction.db");
            CopyDatabaseIfNotExists(transDbPath, transactionSource);
            CopyDatabaseIfNotExists(masterDbPath, masterSource);
        }

        private void CopyDatabaseIfNotExists(string DestPath, string dbSource)
        {
            if (!File.Exists(DestPath))
            {
                using (var br = new BinaryReader(new FileStream(dbSource, FileMode.Open, FileAccess.Read)))
                {
                    using (var bw = new BinaryWriter(new FileStream(DestPath, FileMode.Create, FileAccess.Write)))
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
