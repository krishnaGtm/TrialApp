using Xamarin.Forms;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Collections;

namespace TrialApp.Controls
{
    class BindableStackLayout : StackLayout
    {
        public static readonly BindableProperty ItemsProperty =
            BindableProperty.Create(nameof(Items), typeof(ObservableCollection<StackLayout>), typeof(BindableStackLayout), null,
                propertyChanged: (b, o, n) =>
                {
                    (n as ObservableCollection<StackLayout>).CollectionChanged += (coll, arg) =>
                    {
                        switch (arg.Action)
                        {
                            case NotifyCollectionChangedAction.Add:
                                foreach (var v in arg.NewItems)
                                    (b as BindableStackLayout).Children.Add((StackLayout)v);
                                break;
                            case NotifyCollectionChangedAction.Remove:
                                foreach (var v in arg.NewItems)
                                    (b as BindableStackLayout).Children.Remove((StackLayout)v);
                                break;
                        }
                    };
                });


        public ObservableCollection<StackLayout> Items
        {
            get { return (ObservableCollection<StackLayout>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
    }
}
