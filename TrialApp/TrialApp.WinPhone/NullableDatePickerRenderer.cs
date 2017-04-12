using System.ComponentModel;
using TrialApp.Controls;
using TrialApp.WinPhone;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.WinRT;
[assembly: ExportRenderer(typeof(NullableDatePicker), typeof(NullableDatePickerRenderer))]
namespace TrialApp.WinPhone
{
    public class NullableDatePickerRenderer : ViewRenderer<DatePicker,Windows.UI.Xaml.Controls.DatePicker>// DatePickerRenderer
    {
        /// <summary>
        /// Wird gefeuert wenn das Element sich ändert
        /// </summary>
        /// <param name="e">Event Argumente</param>
        protected override void OnElementChanged(Xamarin.Forms.Platform.WinRT.ElementChangedEventArgs<DatePicker> e)
        {
            //base.OnElementChanged(e);

            //if (this.Control == null) return;

            //var customDatePicker = e.NewElement as NullableDatePicker;

            //if (customDatePicker != null)
            //{                
            //    this.SetValue(NullableDatePicker);
            //}
        }
        /// <summary>
        /// Wird gefeuert wenn sich eine Property des Elements ändert
        /// </summary>
        /// <param name="sender">Der Sender</param>
        /// <param name="e">Event Argumente</param>
        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (this.Control == null) return;

            var customDatePicker = this.Element as NullableDatePicker;

            if (customDatePicker != null)
            {
                if (e.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
                {
                    if (Control != null)
                    {
                        var pickerdate = this.Element as NullableDatePicker;
                        Control
                        
                    }
                    

                }
                //switch (e.PropertyName)
                //{
                //    case NullableDatePicker.IsFocusedProperty:
                //        this.SetValue(customDatePicker);
                //        break;
                //    case CustomDatePicker.NullTextPropertyName:
                //        this.SetValue(customDatePicker);
                //        break;
                //}
            }
        }
        /// <summary>
        /// Setzt den Datumswert oder den NullText
        /// </summary>
        /// <param name="customDatePicker">Das PCL Control</param>
        private void SetValue(CustomDatePicker customDatePicker)
        {
            if (customDatePicker.NullableDate.HasValue)
            {
                Control.for
                Control. = customDatePicker.NullableDate.Value.ToString(customDatePicker.Format);
            }
            else
            {   
                Control.Text = customDatePicker.NullText ?? string.Empty;
            }
        }
    }
}
