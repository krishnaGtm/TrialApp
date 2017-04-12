using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrialApp.ViewModels;
using TrialApp.Views.Abstract;
using Xamarin.Forms;
//using View = System.Windows.Forms.View;

namespace TrialApp.Views
{
    public partial class EditTrialPropertiesPage
    {
        public EditTrialPropertiesPage(int ezid, string crop)
        {
            InitializeComponent();
            ViewModel.Navigation = this.Navigation;
            ViewModel.EzId = ezid.ToString();
            ViewModel.TrialEzId = ezid;
            ViewModel.LoadTrialName();
            ViewModel.LoadFieldsset(crop);
            TrialPropertiesUserControl.UnFocusEx += ViewModel.Entry_Unfocused;
            TrialPropertiesUserControl.SelectedIndexChangedEx += ViewModel.Picker_SelectedIndexChanged;
            TrialPropertiesUserControl.DateSelectedEx += ViewModel.DateData_DateSelected;
            TrialPropertiesUserControl.FocusEx += ViewModel.DateEntry_Focused;
            TrialPropertiesUserControl.ClickedEx += ViewModel.Today_Clicked;
            TrialPropertiesUserControl.DatePickerUnFocusedEx += ViewModel.DatePicker_UnFocusedEX;
            TrialPropertiesUserControl.RevertClickedEx += ViewModel.Revert_Clicked;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.StartTimer();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //ViewModel.Timer.Stop();
        }

        private async void PropertysetPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (PropertysetPicker.SelectedItem != null)
            {
                ViewModel.SelectedFieldset = (int)PropertysetPicker.SelectedValue;
                if (ViewModel.SelectedFieldset > 0)
                    await ViewModel.LoadProperties((int)PropertysetPicker.SelectedValue);
                else
                    ViewModel.TraitList = null;

            }
            else
            {
                ViewModel.SelectedFieldset = null;
            }

        }
    }
}
