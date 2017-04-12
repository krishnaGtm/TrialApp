using TrialApp.ViewModels;
using Xamarin.Forms;

namespace TrialApp.Views
{
    public partial class TransferPage : ContentPage
    {
        private TransferPageViewModel _tranferPageVm;
        public TransferPage()
        {
            InitializeComponent();
            _tranferPageVm = new TransferPageViewModel();
            _tranferPageVm.Navigation = Navigation;
            BindingContext = _tranferPageVm;
            EntrySearch.TextChanged += _tranferPageVm.SearchTextChanged;
            MessagingCenter.Unsubscribe<TransferPageViewModel>(this, "Error");
            MessagingCenter.Subscribe<TransferPageViewModel>(this,"Error",(sender) =>
            {
                DisplayAlert("Error", "Some trials are not downloaded.", "Ok");

            });

        }

        protected async override void OnAppearing()
        {
            _tranferPageVm.StartTimer();
            await _tranferPageVm.ReloadList();
            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //_tranferPageVm.Timer.Stop();
        }

        private void SearchImage_Click(object sender, System.EventArgs e)
        {
            if (_tranferPageVm.SearchVisible)
                _tranferPageVm.SearchVisible = false;
            //_tranferPageVm.FilterData(_tranferPageVm.SearchText);
            else
            {
                _tranferPageVm.SearchVisible = true;
                EntrySearch.Focus();
            }
        }
    }
}
