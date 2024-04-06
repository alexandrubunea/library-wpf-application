using Library_Application.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Application.ViewModels;
using System.Windows.Input;

namespace Library_Application.Commands
{
    internal class SideBarItemsCommand : CommandBase
    {
        // public

        public override void Execute(object? parameter)
        {
            if (parameter == null)
                return;

            string item = (string) parameter;
            
            switch(item)
            {
                case "ALL BOOKS":
                    {
                        navigation.currentViewModel = new AllBooksViewModel(session, navigation);
                        break;
                    }
                case "TYPE OF BOOKS":
                    {
                        navigation.currentViewModel = new TypesOfBooksViewModel(session, navigation);
                        break;
                    }
                case "AUTHORS":
                    {
                        navigation.currentViewModel = new AuthorsViewModel(session, navigation);
                        break;
                    }
                case "PUBLISHERS":
                    {
                        navigation.currentViewModel = new PublishingViewModel(session, navigation);
                        break;
                    }
                case "BORROWED BOOKS":
                    {
                        navigation.currentViewModel = new BorrowedBooksViewModel(session, navigation);
                        break;
                    }
                case "MANAGE LIBRARY":
                    {
                        navigation.currentViewModel = new ManageLibraryViewModel(session, navigation);
                        break;
                    }

                default: {
                        return;
                    }
            }

        }

        public SideBarItemsCommand(Navigation navigation, Session session)
        {
            this.navigation = navigation;
            this.session = session;
        }

        // private
        private Navigation navigation;
        private Session session;
    }
}
