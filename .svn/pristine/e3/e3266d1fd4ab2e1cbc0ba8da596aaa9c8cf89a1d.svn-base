using Xamarin.Forms;

namespace TrialApp.Common
{
    public  class DbPath
    {
        public static string GetMasterDbPath()
        {
            return DependencyService.Get<IFileHelper>().GetLocalFilePath("Master.db");
        }
        public static string GetTransactionDbPath()
        {
            return DependencyService.Get<IFileHelper>().GetLocalFilePath("Transaction.db");
        }
    }
}
