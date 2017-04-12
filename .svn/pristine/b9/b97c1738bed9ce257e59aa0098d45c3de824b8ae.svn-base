using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TrialApp.ViewModels;
using Xamarin.Forms;

namespace TrialApp.Views
{
    public partial class FilterPage
    {
        bool eventRaised = false;
        public FilterPage(List<Entities.Transaction.Trial> allTrials)
        {
            InitializeComponent();
            ViewModel.Navigation = this.Navigation;
            
            ViewModel.TrialList = allTrials;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.LoadAllFilterData();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void FilterSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            var value = sender as Switch;
            if (value == null) return;
            ViewModel.DisableFilter = value.IsToggled;
            var toggleValue = value.IsToggled ? "1" : "0";
            ViewModel.ToggleFilterSetting(toggleValue);
        }
        
        private void entry_Textchanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            ViewModel.ReloadFilter(entry.StyleId, trialtypeEntry.Text, cropEntry.Text, cropsegmentEntry.Text, trialregionEntry.Text, countryEntry.Text);

        }

        private void FilterTrialTypePicker_Clicked(object sender, System.EventArgs e)
        {
            if (!eventRaised)
            {
                trialtypeEntry.TextChanged += entry_Textchanged;
                countryEntry.TextChanged += entry_Textchanged;
                cropEntry.TextChanged += entry_Textchanged;
                cropsegmentEntry.TextChanged += entry_Textchanged;
                trialregionEntry.TextChanged += entry_Textchanged;
                eventRaised = true;
            }

        }
    }
}
