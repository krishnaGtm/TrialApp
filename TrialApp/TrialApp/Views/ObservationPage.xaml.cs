﻿using System;
using System.Collections.Generic;
//using Windows.UI.ViewManagement;
using TrialApp.ViewModels;
using Xamarin.Forms;

namespace TrialApp.Views
{
    public partial class ObservationPage : ContentPage
    {
        private ObservationPageViewModel vm;

        public ObservationPage(string id, string crop, List<VarietyData> varList, int trialEzid)
        {
            InitializeComponent();
            vm = new ObservationPageViewModel();
            vm.LoadObservationViewModel(id, crop, varList, trialEzid);
            vm.LoadFieldsset(crop);            
            BindingContext = vm;
            ObservationUserControl.UnFocusEx += vm.Entry_Unfocused;
            ObservationUserControl.SelectedIndexChangedEx += vm.Picker_SelectedIndexChanged;
            ObservationUserControl.DateSelectedEx += vm.DateData_DateSelected;
            ObservationUserControl.FocusEx += vm.DateEntry_Focused;
            ObservationUserControl.ClickedEx += vm.Today_Clicked;
            ObservationUserControl.DatePickerUnFocusedEx += vm.DatePicker_UnFocusedEX;
            ObservationUserControl.RevertClickedEx += vm.Revert_Clicked;
            ObservationUserControl.EntryTextChangedEx += vm.EntryTextChanged;

        }
        private async void FieldsetPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (FieldsetPicker.SelectedItem != null)
            {
                vm.SelectedFieldset = (int)FieldsetPicker.SelectedValue;
                if (vm.SelectedFieldset > 0)
                    await vm.LoadTraits((int) FieldsetPicker.SelectedValue);
                else
                    vm.TraitList = null;
            }
            else
            {
                vm.SelectedFieldset = null;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.StartTimer();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //vm.Timer.Stop();
        }

    }
}
