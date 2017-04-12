﻿using System;
using System.Windows.Input;
using TrialApp.Controls;
using Xamarin.Forms;

namespace TrialApp.UserControls
{
    public partial class EntryUserControl : ContentView
    {
        public EventHandler<FocusEventArgs> UnFocusEx;
        public EventHandler<FocusEventArgs> FocusEx;
        public EventHandler SelectedIndexChangedEx;
        public EventHandler<DateChangedEventArgs> DateSelectedEx;
        public EventHandler ClickedEx;
        public EventHandler RevertClickedEx;
        public EventHandler<FocusEventArgs> DatePickerUnFocusedEx;
        public EventHandler<TextChangedEventArgs> EntryTextChangedEx;

        public EntryUserControl()
        {
            InitializeComponent();
        }

        public void OnRevertClickedEX(object sender, EventArgs e)
        {
            RevertClickedEx?.Invoke(sender, e);
        }

        public void OnUnFocusEx(object sender, FocusEventArgs e)
        {
            UnFocusEx?.Invoke(sender, e);
        }

        public void OnDateSelectedEx(object sender, DateChangedEventArgs e)
        {
            DateSelectedEx?.Invoke(sender, e);
        }

        public void OnSelectedIndexChangedEx(object sender, EventArgs e)
        {
            SelectedIndexChangedEx?.Invoke(sender, e);
        }

        public void OnFocusEX(object sender, FocusEventArgs e)
        {
            FocusEx?.Invoke(sender, e);
        }

        public void OnClickedEx(object sender, EventArgs e)
        {
            ClickedEx?.Invoke(sender, e);
        }

        public void OnDatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            DatePickerUnFocusedEx?.Invoke(sender, e);
        }

        public void Entry_OnTextChangedEx(object sender, TextChangedEventArgs e)
        {
            EntryTextChangedEx?.Invoke(sender, e);
        }
    }
}
