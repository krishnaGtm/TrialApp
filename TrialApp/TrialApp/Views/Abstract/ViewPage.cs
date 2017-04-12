using TrialApp.ViewModels.Inetrfaces;
using Xamarin.Forms;

namespace TrialApp.Views.Abstract
{
    public class ViewPage<TViewModel>: ContentPage where TViewModel:IViewModel
    {
        public TViewModel ViewModel => (TViewModel)BindingContext;
    }
}
