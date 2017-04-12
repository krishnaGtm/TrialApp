using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TrialApp.Views
{
    public partial class Page1 : ContentPage
    {
        public ObservableCollection<Person> Persons { get; set; }
        public Page1()
        {
            Persons = new ObservableCollection<Person>();
            InitializeComponent();
            BindingContext = this;
            LoadData();

        }
        public class Person : INotifyPropertyChanged
        {
            private string _name;
            private int _age;

            public string Name
            {
                get { return _name; }
                set
                {
                    if (_name == value) return;
                    _name = value;
                    OnPropertyChanged();
                }
            }

            public int Age
            {
                get { return _age; }
                set
                {
                    if (_age == value) return;
                    _age = value;
                    OnPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                var handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void LoadData()
        {
            for (int i = 0; i < 20; i++)
            {
                var person = new Person { Name = "person " + i, Age = 10 + i };
                Persons.Add(person);
            }
        }
        private void Button_OnClicked(object sender, EventArgs e)
        {
            var person = new Person { Name = "person " + Persons.Count, Age = 10 + Persons.Count };
            Persons.Add(person);
        }
    }
}
