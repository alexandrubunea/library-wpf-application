using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Library_Application.Stores;
using Library_Application.ViewModels;

namespace Library_Application.Commands
{
    internal class LogoutCommand : CommandBase
    {
        // public
        public LogoutCommand(Navigation navigation) 
        { 
            this.navigation = navigation;
        }
        public override void Execute(object? parameter)
        {
            navigation.currentViewModel = new StartViewModel(navigation);
        }

        // private
        private readonly Navigation navigation;
    }
}
