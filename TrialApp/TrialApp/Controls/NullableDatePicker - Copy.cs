using System;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using Xamarin.Forms;

namespace TrialApp.Controls
{
    public class NullableDatePicker : DatePicker
    {
        

         private string _format = null;
         //public static readonly BindableProperty NullableDateProperty = BindableProperty.Create<NullableDatePicker, DateTime?>(p => p.NullableDate, null,BindingMode.TwoWay);
         public static readonly BindableProperty NullableDateProperty =
            BindableProperty.Create("NullableDate", typeof(DateTime?), typeof(NullableDatePicker), null, BindingMode.TwoWay);
         //public static readonly BindableProperty EmptyStateTextProperty = BindableProperty.Create(
         //   "EmptyStateText", typeof(string), typeof(NullableDatePicker), string.Empty, BindingMode.OneWay);

         public DateTime? NullableDate
         {
             get { return (DateTime?)GetValue(NullableDateProperty); }
             set { SetValue(NullableDateProperty, value); UpdateDate(); }
         }
         private void UpdateDate()
         {
             if (NullableDate.HasValue)
             {
                 if (null != _format) Format = _format;
                 Date = NullableDate.Value;
             }
             else
             {
                 _format = Format;
                 Format = "pick ...";
                 //OnPropertyChanged("Format");
                 //_format = "pick ...";
                 //Format = "pick ...";
             }
         }
         //public string EmptyStateText
         //{
         //    get { return (string)GetValue(EmptyStateTextProperty); }
         //    set { SetValue(EmptyStateTextProperty, value); }
         //}
         protected override void OnBindingContextChanged()
         {
             base.OnBindingContextChanged();
             UpdateDate();
         }

         protected override void OnPropertyChanged(string propertyName = null)
         {
             //base.OnPropertyChanged(propertyName);
             //Device.OnPlatform(() =>
             //{
             //    if (propertyName == IsFocusedProperty.PropertyName)
             //    {
             //        if (IsFocused)
             //        {
             //            if (!NullableDate.HasValue)
             //            {
             //                Date = (DateTime)DateProperty.DefaultValue;
             //            }
             //        }
             //        else
             //        {
             //            OnPropertyChanged(DateProperty.PropertyName);
             //        }
             //    }
             ////});

             //if (propertyName == DateProperty.PropertyName)
             //{
             //    NullableDate = Date;
             //}

             //if (propertyName == NullableDateProperty.PropertyName)
             //{
             //    if (NullableDate.HasValue)
             //    {
             //        Date = NullableDate.Value;
             //    }
             //}
             if (propertyName == "Date")
                 NullableDate = Date.Date;


         }


         
         /*
        public const string NullableDatePropertyName = "NullableDate";
        public const string isFocusedPropertyName = "IsFocusedEx";
        /// <summary>
        /// BinableProperty
        /// </summary>
        //public static readonly BindableProperty NullableDateProperty = BindableProperty.Create<NullableDatePicker, DateTime?>(i => i.NullableDate, null, BindingMode.TwoWay, null, NullableDateChanged);
        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create("NullableDate", typeof(DateTime?), typeof(NullableDatePicker), null, BindingMode.TwoWay);
        public static readonly BindableProperty IsFocusedExProperty = BindableProperty.Create("IsFocusedEx", typeof(bool), typeof(NullableDatePicker), false, BindingMode.TwoWay);
        /// <summary>
        /// date value which accepts null values
        /// </summary>
        public DateTime? NullableDate
        {
            get
            {
                return (DateTime?)this.GetValue(NullableDateProperty);
            }
            set
            {
                this.SetValue(NullableDateProperty, value);
                Focus();
            }
        }
        /// <summary>
        /// Focused bindableproperty
        /// </summary>
        public bool IsFocusedEx
        {
            get { return (bool)this.GetValue(IsFocusedExProperty); }
            set
            {
                this.SetValue(IsFocusedExProperty, value);
                if (value)
                    Focus();
            }
        }
        public const string NullTextPropertyName = "NullText";
        //public static readonly BindableProperty NullTextProperty = BindableProperty.Create<NullableDatePicker, string>(i => i.NullText, default(string), BindingMode.TwoWay);
        public static readonly BindableProperty NullTextProperty = BindableProperty.Create("NullText", typeof(string), typeof(NullableDatePicker), null, BindingMode.TwoWay);
       
        public string NullText
        {
            get
            {
                return (string)this.GetValue(NullTextProperty);
            }
            set
            {
                this.SetValue(NullTextProperty, value);
            }
        }
        
        public const string DisplayBorderPropertyName = "DisplayBorder";
        
        public static readonly BindableProperty DisplayBorderProperty = BindableProperty.Create<NullableDatePicker, bool>(i => i.DisplayBorder, default(bool), BindingMode.TwoWay);
        
        public bool DisplayBorder
        {
            get
            {
                return (bool)this.GetValue(DisplayBorderProperty);
            }
            set
            {
                this.SetValue(DisplayBorderProperty, value);
            }
        }
        
        public NullableDatePicker()
        {
            this.DateSelected += CustomDatePicker_DateSelected;
            this.Format = "dd.MM.yyyy";
        }

        private void CustomDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            this.NullableDate = new DateTime(
                e.NewDate.Year,
                e.NewDate.Month,
                e.NewDate.Day,
                this.NullableDate.HasValue ? this.NullableDate.Value.Hour : 0,
                this.NullableDate.HasValue ? this.NullableDate.Value.Minute : 0,
                this.NullableDate.HasValue ? this.NullableDate.Value.Second : 0);
        }
        private static void NullableDateChanged(BindableObject obj, DateTime? oldValue, DateTime? newValue)
        {
            var customDatePicker = obj as NullableDatePicker;

            if (customDatePicker != null)
            {
                if (newValue.HasValue)
                {
                    customDatePicker.Date = newValue.Value;
                }
            }
        }
        */


    }
    public class CustomDatePicker : DatePicker
    {
        public const string NullableDatePropertyName = "NullableDate";
        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create<CustomDatePicker, DateTime?>(i => i.NullableDate, null, BindingMode.TwoWay, null, NullableDateChanged);
        public DateTime? NullableDate
        {
            get
            {
                return (DateTime?)this.GetValue(NullableDateProperty);
            }
            set
            {
                this.SetValue(NullableDateProperty, value);
            }
        }
        public const string NullTextPropertyName = "NullText";
        
        public static readonly BindableProperty NullTextProperty = BindableProperty.Create<CustomDatePicker, string>(i => i.NullText, default(string), BindingMode.TwoWay);
        
        public string NullText
        {
            get
            {
                return (string)this.GetValue(NullTextProperty);
            }
            set
            {
                this.SetValue(NullTextProperty, value);
            }
        }
        
        public const string DisplayBorderPropertyName = "DisplayBorder";
        
        public static readonly BindableProperty DisplayBorderProperty = BindableProperty.Create<CustomDatePicker, bool>(i => i.DisplayBorder, default(bool), BindingMode.TwoWay);
        
        public bool DisplayBorder
        {
            get
            {
                return (bool)this.GetValue(DisplayBorderProperty);
            }
            set
            {
                this.SetValue(DisplayBorderProperty, value);
            }
        }
        
        public CustomDatePicker()
        {
            this.DateSelected += CustomDatePicker_DateSelected;

            this.Format = "dd.MM.yyyy";
        }
        
        void CustomDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            this.NullableDate = new DateTime(
                e.NewDate.Year,
                e.NewDate.Month,
                e.NewDate.Day,
                this.NullableDate.HasValue ? this.NullableDate.Value.Hour : 0,
                this.NullableDate.HasValue ? this.NullableDate.Value.Minute : 0,
                this.NullableDate.HasValue ? this.NullableDate.Value.Second : 0);
        }
        
        private static void NullableDateChanged(BindableObject obj, DateTime? oldValue, DateTime? newValue)
        {
            var customDatePicker = obj as CustomDatePicker;

            if (customDatePicker != null)
            {
                if (newValue.HasValue)
                {
                    customDatePicker.Date = newValue.Value;
                }
            }
        }
    }


}
