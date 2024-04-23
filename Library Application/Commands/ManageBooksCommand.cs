using Library_Application.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Application.ViewModels;
using Library_Application.Models;

namespace Library_Application.Commands
{
    class ManageBooksCommand : CommandBase
    {
        // public
        public ManageBooksCommand(Session session, Navigation navigation, string button)
        {
            this.session = session;
            this.navigation = navigation;
            this.button = button;
        }

        public override void Execute(object? parameter)
        {
            if (parameter == null)
                return;

            if(button == "create")
            {
                navigation.currentViewModel = new CreateBookViewModel(session, navigation);
                return;
            }

            ManageBooksViewModel? currentView = navigation.currentViewModel as ManageBooksViewModel;

            if (button == "activate")
            {
                Book? bookFound = currentView.BooksList.FirstOrDefault(author => author.Id == (parameter as Book).Id);
                if (bookFound != null)
                {
                    bookFound.setActiveStatus(true);
                    currentView.BookCollectionView.Refresh();
                }
                return;
            }
            if (button == "deactivate")
            {
                Book? bookFound = currentView.BooksList.FirstOrDefault(book => book.Id == (parameter as Book).Id);
                if (bookFound != null)
                {
                    bookFound.setActiveStatus(false);
                    currentView.BookCollectionView.Refresh();
                }
                return;
            }
            if(button == "edit")
            {
                Book? bookFound = currentView.BooksList.FirstOrDefault(book => book.Id == (parameter as Book).Id);

                if (bookFound != null)
                {
                    navigation.currentViewModel = new CreateBookViewModel(session, navigation, true);
                    CreateBookViewModel? currViewModel = navigation.currentViewModel as CreateBookViewModel;

                    if (currViewModel == null)
                        return;

                    currViewModel.Book = bookFound;
                }

                return;
            }
        }

        // private
        private readonly Session session;
        private readonly Navigation navigation;
        private string button;
    }
}
