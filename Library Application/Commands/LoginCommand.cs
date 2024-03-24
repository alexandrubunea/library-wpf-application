using Library_Application.Stores;
using Library_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.Commands
{
    internal class LoginCommand : CommandBase
    {
        // public

        public LoginCommand(string buttonName, Navigation navigation)
        {
            this.buttonName = buttonName;
            this.navigation = navigation;
        }

        public override void Execute(object? parameter)
        {
            if(buttonName == "login")
            {
                // Login into account...
            }
            else
            {
                navigation.currentViewModel = new StartViewModel(navigation);
            }
        }

        // private

        private string buttonName;
        private Navigation navigation;
    }
}
