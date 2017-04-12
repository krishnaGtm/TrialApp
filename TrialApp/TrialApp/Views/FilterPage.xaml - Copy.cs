﻿using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TrialApp.ViewModels;
using Xamarin.Forms;

namespace TrialApp.Views
{
    public partial class FilterPage
    {
        //FilterPageViewModel1 ViewModel = new FilterPageViewModel1();
        public FilterPage(List<Entities.Transaction.Trial> allTrials)
        {
            InitializeComponent();
            ViewModel.IsInitialLoad = true;
            ViewModel.Navigation = this.Navigation;
            
            ViewModel.TrialList = allTrials;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.IsInitialLoad = true;
            await ViewModel.LoadAllFilterData();
            

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //ViewModel.Timer.Stop();
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
            //ViewModel.ReloadFilter(entry.StyleId, entry.Text);
            if(!ViewModel.IsInitialLoad)
                ViewModel.ReloadFilter1(entry.StyleId, trialtypeEntry.Text, cropEntry.Text, cropsegmentEntry.Text, trialregionEntry.Text, countryEntry.Text);

        }
    }
}