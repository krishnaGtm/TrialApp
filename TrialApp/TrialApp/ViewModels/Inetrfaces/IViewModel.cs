using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TrialApp.ViewModels.Inetrfaces
{
    public interface IViewModel
    {
        bool IsBusy { get; set; }
        
        //void Open(INavigation navigation);
        //void Close(INavigation navigation);
    }
}
