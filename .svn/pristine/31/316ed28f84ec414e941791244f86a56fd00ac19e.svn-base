using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TrialApp.Common
{
    public class LogMessage
    {
        public async static Task<bool> Log(string msg)
        {
            try
            {
                await DependencyService.Get<ILogger>().Log(msg);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
