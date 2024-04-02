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
    class ManageBooksViewModel : ViewModelBaseSideBar
    {
        // public
        public ICommand CreateBook { get; }
        public ICommand ActivateCommand { get; }
        public ICommand DeactivateCommand { get; }
        public ManageBooksViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
            CreateBook = new ManageBooksCommand(session, navigation, "create");
            ActivateCommand = new ManageBooksCommand(session, navigation, "activate");
            DeactivateCommand = new ManageBooksCommand(session, navigation, "deactivate");
        }

        // private
    }
}
