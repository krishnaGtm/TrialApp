﻿using TrialApp.ViewModels;
using TrialApp.Views.Abstract;

namespace TrialApp.Views
{
    public partial class SignInPage : ViewPage<SignInPageViewModel>//ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
            ViewModel.Navigation = this.Navigation;
            //BindingContext = new SignInPageViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.StartTimer();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
           // ViewModel.Timer.Stop();
        }
    }
}
