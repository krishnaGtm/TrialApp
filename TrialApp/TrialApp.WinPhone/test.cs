﻿using System;
using System.ComponentModel;
using TrialApp.Controls;
using TrialApp.WinPhone;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;

[assembly: ExportRenderer(typeof(NullableDatePicker), typeof(NullableDatePickerRenderer))]
namespace TrialApp.WinPhone
{
    public class NullableDatePicker : ViewRenderer<DatePicker, edit>
    {
        DatePickerDialog _dialog;
        bool _disposed;
        TextColorSwitcher _textColorSwitcher;

        public DatePickerRenderer()
        {
            AutoPackage = false;
            if (Forms.IsLollipopOrNewer)
                Device.Info.PropertyChanged += DeviceInfoPropertyChanged;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                if (Forms.IsLollipopOrNewer)
                    Device.Info.PropertyChanged -= DeviceInfoPropertyChanged;

                _disposed = true;
                if (_dialog != null)
                {
                    if (Forms.IsLollipopOrNewer)
                        _dialog.CancelEvent -= OnCancelButtonClicked;

                    _dialog.Hide();
                    _dialog.Dispose();
                    _dialog = null;
                }
            }
            base.Dispose(disposing);
        }

        protected override EditText CreateNativeControl()
        {
            return new EditText(Context) { Focusable = false, Clickable = true, Tag = this };
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var textField = CreateNativeControl();

                textField.SetOnClickListener(TextFieldClickHandler.Instance);
                SetNativeControl(textField);
                _textColorSwitcher = new TextColorSwitcher(textField.TextColors);
            }

            SetDate(Element.Date);

            UpdateMinimumDate();
            UpdateMaximumDate();
            UpdateTextColor();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == DatePicker.DateProperty.PropertyName || e.PropertyName == DatePicker.FormatProperty.PropertyName)
                SetDate(Element.Date);
            else if (e.PropertyName == DatePicker.MinimumDateProperty.PropertyName)
                UpdateMinimumDate();
            else if (e.PropertyName == DatePicker.MaximumDateProperty.PropertyName)
                UpdateMaximumDate();
            if (e.PropertyName == DatePicker.TextColorProperty.PropertyName)
                UpdateTextColor();
        }

        internal override void OnFocusChangeRequested(object sender, VisualElement.FocusRequestArgs e)
        {
            base.OnFocusChangeRequested(sender, e);

            if (e.Focus)
                OnTextFieldClicked();
            else if (_dialog != null)
            {
                _dialog.Hide();
                ((IElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
                Control.ClearFocus();

                if (Forms.IsLollipopOrNewer)
                    _dialog.CancelEvent -= OnCancelButtonClicked;

                _dialog = null;
            }
        }

        void CreateDatePickerDialog(int year, int month, int day)
        {
            DatePicker view = Element;
            _dialog = new DatePickerDialog(Context, (o, e) =>
            {
                view.Date = e.Date;
                ((IElementController)view).SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
                Control.ClearFocus();

                if (Forms.IsLollipopOrNewer)
                    _dialog.CancelEvent -= OnCancelButtonClicked;

                _dialog = null;
            }, year, month, day);
        }

        void DeviceInfoPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentOrientation")
            {
                DatePickerDialog currentDialog = _dialog;
                if (currentDialog != null && currentDialog.IsShowing)
                {
                    currentDialog.Dismiss();
                    CreateDatePickerDialog(currentDialog.DatePicker.Year, currentDialog.DatePicker.Month, currentDialog.DatePicker.DayOfMonth);
                    _dialog.Show();
                }
            }
        }

        void OnTextFieldClicked()
        {
            DatePicker view = Element;
            ((IElementController)view).SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, true);

            CreateDatePickerDialog(view.Date.Year, view.Date.Month - 1, view.Date.Day);

            UpdateMinimumDate();
            UpdateMaximumDate();

            if (Forms.IsLollipopOrNewer)
                _dialog.CancelEvent += OnCancelButtonClicked;

            _dialog.Show();
        }

        void OnCancelButtonClicked(object sender, EventArgs e)
        {
            Element.Unfocus();
        }

        void SetDate(DateTime date)
        {
            Control.Text = date.ToString(Element.Format);
        }

        void UpdateMaximumDate()
        {
            if (_dialog != null)
            {
                _dialog.DatePicker.MaxDate = (long)Element.MaximumDate.ToUniversalTime().Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
            }
        }

        void UpdateMinimumDate()
        {
            if (_dialog != null)
            {
                _dialog.DatePicker.MinDate = (long)Element.MinimumDate.ToUniversalTime().Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
            }
        }

        void UpdateTextColor()
        {
            _textColorSwitcher?.UpdateTextColor(Control, Element.TextColor);
        }

        class TextFieldClickHandler : Object, IOnClickListener
        {
            public static readonly TextFieldClickHandler Instance = new TextFieldClickHandler();

            public void OnClick(AView v)
            {
                ((DatePickerRenderer)v.Tag).OnTextFieldClicked();
            }
        }
    }
}

