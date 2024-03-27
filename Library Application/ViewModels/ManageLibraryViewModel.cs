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
    class ManageLibraryViewModel : ViewModelBaseSideBar
    {
        // public
        public ICommand CommandManageBooks { get; set; }
        public ICommand CommandManageAuthors { get; set; }
        public ICommand CommandManagePublishers { get; set; }
        public ICommand CommandManageUsers { get; set; }
        public ICommand CommandManageBookTypes { get; set; }
        public ICommand CommandManageBooksBorrow { get; set; }

        public ManageLibraryViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
            CommandManageBooks = new ManageLibraryCommand(session, navigation, "managebooks");
            CommandManageAuthors = new ManageLibraryCommand(session, navigation, "manageauthors");
            CommandManagePublishers = new ManageLibraryCommand(session, navigation, "managepublishers");
            CommandManageUsers = new ManageLibraryCommand(session, navigation, "manageusers");
            CommandManageBookTypes = new ManageLibraryCommand(session, navigation, "managebooktypes");
            CommandManageBooksBorrow = new ManageLibraryCommand(session, navigation, "managebooksborrow");
        }
    }
}
