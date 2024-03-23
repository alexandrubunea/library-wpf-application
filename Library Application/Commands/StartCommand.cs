using Library_Application.Stores;
using Library_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.Commands
{
    internal class StartCommand : CommandBase
    {
        // public
        public StartCommand(string buttonName, Navigation navigation) 
        {
            this.buttonName = buttonName;
            this.navigation = navigation;
        }

        public override void Execute(object? parameter)
        {
            if(buttonName == "login")
            {
                navigation.currentViewModel = new LoginViewModel();
            } 
            else
            {
                navigation.currentViewModel = new RegisterViewModel();
            }
        }

        // private
        private readonly string buttonName;
        private readonly Navigation navigation;
    }
}
