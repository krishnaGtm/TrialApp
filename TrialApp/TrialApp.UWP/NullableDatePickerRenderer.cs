using System.ComponentModel;
using TrialApp.Controls;
using TrialApp.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace TrialApp.UWP
{
    public class CustomDatePickerRenderer :DatePickerRenderer
    {
        /// <summary>
        /// Wird gefeuert wenn das Element sich ändert
        /// </summary>
        /// <param name="e">Event Argumente</param>
        protected override void OnElementChanged(Xamarin.Forms.Platform.UWP.ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null) return;

            var customDatePicker = e.NewElement as CustomDatePicker;

            if (customDatePicker != null)
            {       
                this.SetValue(customDatePicker);
            }
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

            var customDatePicker = this.Element as CustomDatePicker;

            if (customDatePicker != null)
            {
                switch (e.PropertyName)
                {
                    case CustomDatePicker.NullableDatePropertyName:
                        this.SetValue(customDatePicker);
                        break;
                    case CustomDatePicker.NullTextPropertyName:
                        this.SetValue(customDatePicker);
                        break;
                }
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
                Control.Text = customDatePicker.NullableDate.Value.ToString(customDatePicker.Format);
            }
            else
            {   
                Control.Text = customDatePicker.NullText ?? string.Empty;
            }
        }
    }
}
