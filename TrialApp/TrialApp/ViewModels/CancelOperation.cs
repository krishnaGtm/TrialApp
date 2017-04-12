using System;
using System.Windows.Input;

namespace TrialApp.ViewModels
{
    internal class CancelOperation : ICommand
    {
        public CancelOperation()
        {
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;

        }

        public async void Execute(object parameter)
        {
            if (parameter is SignInPageViewModel)
            {
                var signInViewModel = parameter as SignInPageViewModel;
                signInViewModel.UserName = "";
                await App.MainNavigation.PopAsync();
            }
            else if (parameter is FilterPageViewModel)
            {
                var vm = parameter as FilterPageViewModel;
                await App.MainNavigation.PopAsync();
            }
        }
    }
}