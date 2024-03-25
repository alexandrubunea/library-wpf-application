using Library_Application.Models;
using Library_Application.Stores;
using Library_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.Commands
{
    internal class RegisterCommand : CommandBase
    {
        // public

        public RegisterCommand(string buttonName, Navigation navigation)
        {
            this.buttonName = buttonName;
            this.navigation = navigation;
        }

        public override void Execute(object? parameter)
        {
            RegisterViewModel? viewModel = navigation.currentViewModel as RegisterViewModel;
            if(buttonName == "register" && viewModel.NotEmptyData && !viewModel.HasErrors)
            {
                // Register user...
            }
            else if(buttonName == "back")
            {
                navigation.currentViewModel = new StartViewModel(navigation);
            }
        }

        // private
        private string buttonName;
        private Navigation navigation;
    }
}
