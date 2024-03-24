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

        public RegisterCommand(string buttonName, Navigation navigation, bool validData)
        {
            this.buttonName = buttonName;
            this.navigation = navigation;
            this.validData = validData;
        }

        public override void Execute(object? parameter)
        {
            if(buttonName == "register" && validData)
            {
                // Register user...
            }
            else
            {
                navigation.currentViewModel = new StartViewModel(navigation);
            }
        }

        // private
        private string buttonName;
        private Navigation navigation;
        private bool validData;
    }
}
