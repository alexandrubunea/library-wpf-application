using Library_Application.Commands;
using Library_Application.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library_Application.ViewModels
{
    internal class StartViewModel : ViewModelBase
    {
        // public

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public StartViewModel(Navigation navigation)
        {
            LoginCommand = new StartCommand("login", navigation);
            RegisterCommand = new StartCommand("register", navigation);
        }
    }
}
