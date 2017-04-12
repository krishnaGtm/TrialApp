﻿using System;
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
            _tranferPageVm = this.BindingContext as TransferPageViewModel;// new TransferPageViewModel();
            BindingContext = _tranferPageVm;
            EntrySearch.TextChanged += _tranferPageVm.SearchTextChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _tranferPageVm.StartTimer();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _tranferPageVm.Timer.Stop();
        }

        private void SearchImage_Click(object sender, EventArgs e)
        {
            if (_tranferPageVm.SearchVisible)
                _tranferPageVm.SearchVisible = true;
            else
            {
                _tranferPageVm.SearchVisible = false;
                _tranferPageVm.FilterData(_tranferPageVm.SearchText);
            }
        }
    }
}
