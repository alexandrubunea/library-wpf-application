using Library_Application.Models;
using Library_Application.Stores;
using Library_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Library_Application.Commands
{
    class ManageBookTypesCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            if(parameter == null)
                return;

            if(button == "create")
            {
                navigation.currentViewModel = new CreateBookTypeViewModel(session, navigation);
                return;
            }

            ManageBookTypesViewModel? currentView = navigation.currentViewModel as ManageBookTypesViewModel;

            if(button == "activate")
            {
                BookType? bookTypeFound = currentView.BookTypesList.FirstOrDefault(bookType => bookType.Id == (parameter as BookType).Id);
                if (bookTypeFound != null)
                {
                    bookTypeFound.setActiveStatus(true);
                    currentView.BookTypesCollectionView.Refresh();
                }
                return;
            }
            if(button == "deactivate")
            {
                BookType? bookTypeFound = currentView.BookTypesList.FirstOrDefault(bookType => bookType.Id == (parameter as BookType).Id);
                if (bookTypeFound != null)
                {
                    bookTypeFound.setActiveStatus(false);
                    currentView.BookTypesCollectionView.Refresh();
                }
                return;
            }
            if (button == "edit")
            {
                BookType? bookTypeFound = currentView.BookTypesList.FirstOrDefault(bookType => bookType.Id == (parameter as BookType).Id);
                if (bookTypeFound != null) 
                { 
                    navigation.currentViewModel = new CreateBookTypeViewModel(session, navigation, true);
                    CreateBookTypeViewModel? currViewModel = navigation.currentViewModel as CreateBookTypeViewModel;

                    if (currViewModel == null)
                        return;

                    currViewModel.BookType = bookTypeFound;
                }
                return;
            }
        }

        public ManageBookTypesCommand(string button, Session session, Navigation navigation)
        {
            this.button = button;
            this.session = session;
            this.navigation = navigation;
        }

        // private
        private readonly string button;
        private readonly Session session;
        private readonly Navigation navigation;

    }
}
