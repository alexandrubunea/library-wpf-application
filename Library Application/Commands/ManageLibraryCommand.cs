using Library_Application.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Application.ViewModels;

namespace Library_Application.Commands
{
    class ManageLibraryCommand : CommandBase
    {
        // public
        public override void Execute(object? parameter)
        {
             switch(button)
            {
                case "managebooks":
                    {
                        navigation.currentViewModel = new ManageBooksViewModel(session, navigation);
                        break;
                    }
                case "manageusers":
                    {
                        navigation.currentViewModel = new ManageUsersViewModel(session, navigation);
                        break;
                    }
                case "managebooktypes":
                    {
                        navigation.currentViewModel = new ManageBookTypesViewModel(session, navigation);
                        break;
                    }
                case "manageauthors":
                    {
                        navigation.currentViewModel = new ManageAuthorsViewModel(session, navigation);
                        break;
                    }
                case "managepublishers":
                    {
                        navigation.currentViewModel = new ManagePublishersViewModel(session, navigation);
                        break;
                    }
                case "managebooksborrow":
                    {
                        navigation.currentViewModel = new ManageBooksBorrowViewModel(session, navigation);
                        break;
                    }

                default:
                    {
                        return;
                    }
            }
        }

        public ManageLibraryCommand(Session session, Navigation navigation, string button)
        {
            this.button = button;
            this.navigation = navigation;
            this.session = session;
        }

        private string button;
        private readonly Session session;
        private readonly Navigation navigation;
    }
}
