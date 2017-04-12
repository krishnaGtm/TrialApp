﻿using System;
using System.Collections.Generic;
using System.Linq;
using TrialApp.Common.Extensions;
using TrialApp.Services;
using TrialApp.ViewModels;
using Xamarin.Forms;
using Button = Xamarin.Forms.Button;
using Entry = Xamarin.Forms.Entry;
using Image = Xamarin.Forms.Image;
using StackLayout = Xamarin.Forms.StackLayout;

namespace TrialApp.Views
{
    public partial class MainPage
    {
        public static readonly Dictionary<string, string> TrialStatus = new Dictionary<string, string>
        {
            {"10","New"},
            {"20","Synced"},
            {"30","Updated"}
        };

        private readonly TrialService _service;
        private readonly MainPageViewModel _vm;
        private int ticks { get; set; }
        public MainPage()
        {

            InitializeComponent();
            _service = new TrialService();
            _vm = BindingContext as MainPageViewModel;
            _vm.Navigation = Navigation;
            //_vm.LoadTrials();
            MessagingCenter.Unsubscribe<MainPageViewModel, string[]>(this, "DisplayAlert");
            MessagingCenter.Subscribe<MainPageViewModel, string[]>(this, "DisplayAlert", async (sender, values) =>
            {
                var action = await DisplayAlert(values[0], values[1], "Remove", "Keep");
                if (action)
                {
                    await _vm.RemoveTrials();
                    await _vm.RefershFilter();
                    _vm.SubmitVisible = false;
                    _vm.SelectedTileList.Clear();

                    OnAppearing();
                }
                else
                {
                    await _vm.UpdateTrial();
                    _vm.SubmitVisible = false;
                    _vm.SelectedTileList.Clear();
                    OnAppearing();
                }
            });
        }


        protected async override void OnAppearing()
        {
            _vm.SearchVisible = false;
            _vm.UserName = WebserviceTasks.usernameWS.ToText();
            _vm.SaveFilterList = await _vm.SaveFilterService.GetSaveFilterAsync();
            //await _vm.ReloadTrial("");
            _vm.ReloadTrialProp = false;
            _vm.Settings = _vm._settingParametersService.GetParamsList().Single();
            await _vm.LoadTrials();
            _vm.PersistSubmitTrialList();
            _vm.StartTimer();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //_vm.Timer.Stop();
        }
        private async void SignOut_Activated(object sender, EventHandler e)
        {
            if (string.IsNullOrEmpty(_vm.UserName)) return;
            var answer = await DisplayAlert("Question?", "Do you really want to Sign Out?", "Yes", "No");
            if (!answer) return;
            var loginButton = _vm.listSource.FirstOrDefault(x => x.IsTrial == false);
            if (loginButton == null) return;
            loginButton.TrialColor = Color.Gray;
            loginButton.FontsizeTrialName = Device.GetNamedSize(NamedSize.Medium, typeof(Button));
            loginButton.OnlineStatus = "OFFLINE";
            _vm.ClearUserForSingOut();
        }

        private async void Tile_Tapping(object sender, MR.Gestures.TapEventArgs e)
        {
            var tile = sender as MR.Gestures.StackLayout;
            var test = e.ViewPosition;
            var classid = tile?.ClassId;
            if (classid != null)
            {
               await App.MainNavigation.PushAsync(new VarietyPage(Convert.ToInt32(tile.ClassId.Split('|')[0]), tile.ClassId.Split('|')[1], tile.ClassId.Split('|')[2]));
            }
        }

        private async void Tile_LongPressing(object sender, MR.Gestures.LongPressEventArgs e)
        {
            if (_vm.UserName == "" || !WebserviceTasks.CheckTokenValidDate())
               await App.MainNavigation.PushAsync(new SignInPage());
            else
            {
                var tile = sender as MR.Gestures.StackLayout;
                var image = (tile.Children[0] as StackLayout).Children[1] as Image;
                image.IsVisible = !image.IsVisible;
                var param = (ViewModels.Trial)tile.LongPressedCommandParameter;
                var param1 = new Entities.Transaction.Trial
                {
                    CountryCode = param.CountryCode,
                    TrialName = param.TrialName,
                    EZID = param.EZID,
                    TrialTypeID = param.TrialTypeID,
                    StatusCode = param.StatusName == "New" ? 10 : param.StatusName == "Synced" ? 20 : 30,// param.StatusCode,
                    CropCode = param.CropCode
                };
                _vm.UpdateSubmit(param1, image.IsVisible);
            }

        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn.Text == "+" && WebserviceTasks.CheckTokenValidDate())
                await App.MainNavigation.PushAsync(new TransferPage());
            else
               await  App.MainNavigation.PushAsync(new SignInPage());
        }

        private async void Entry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if(_vm.ReloadTrialProp)
                await _vm.ReloadTrial(_vm.SearchText);
        }

        private void SearchImage_Click(object sender, EventArgs e)
        {
            if (_vm.SearchVisible)
                _vm.SearchVisible = false;
            //await _vm.ReloadTrial(_vm.SearchText);
            else
            {
                _vm.SearchVisible = true;
                EntrySearch.Focus();
            }

        }
    }
}