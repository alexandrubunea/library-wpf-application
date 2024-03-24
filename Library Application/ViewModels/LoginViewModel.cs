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
    internal class LoginViewModel : ViewModelBase
    {
        // public
        public ICommand LoginCommand { get; }
        public ICommand BackCommand { get; }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        
        public LoginViewModel(Navigation navigation)
        {
            LoginCommand = new LoginCommand("login", navigation);
            BackCommand = new LoginCommand("back", navigation);
            email = "";
        }

        // private
        private string email;
    }
}
