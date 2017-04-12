using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace TrialApp.Core
{
    public class SQLProvider : ISQLProvider
    {

        public SQLiteConnection GetMasterConnection()
        {
            //var sqliteFilename = "Master.db";
            string documentsPath = DependencyService.Get<IFileHelper>().GetLocalFilePath("Master.db"); // Documents folder
           // var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection
            var conn = new SQLite.SQLiteConnection(documentsPath);
            // Return the database connection
            return conn;
        }

        public SQLiteConnection GetTransactionConnection()
        {
            //var sqliteFilename = "Master.db";
            string documentsPath = DependencyService.Get<IFileHelper>().GetLocalFilePath("Transaction.db"); // Documents folder
                                                                                                       // var path = Path.Combine(documentsPath, sqliteFilename);
         // Create the connection
            var conn = new SQLite.SQLiteConnection(documentsPath);
            // Return the database connection
            return conn;
        }
    }
}
