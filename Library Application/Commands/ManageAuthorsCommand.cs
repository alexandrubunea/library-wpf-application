using Library_Application.Models;
using Library_Application.Stores;
using Library_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.Commands
{
    class ManageAuthorsCommand : CommandBase
    {
        // public
        public ManageAuthorsCommand(Session session, Navigation navigation, string button)
        {
            this.session = session;
            this.navigation = navigation;
            this.button = button;
        }

        public override void Execute(object? parameter)
        {
            if (parameter == null)
                return;

            if (button == "create")
            {
                navigation.currentViewModel = new CreateAuthorViewModel(session, navigation);
                return;
            }

            ManageAuthorsViewModel? currentView = navigation.currentViewModel as ManageAuthorsViewModel;

            if (button == "activate")
            {
                Author? authorFound = currentView.AuthorList.FirstOrDefault(author => author.Id == (parameter as Author).Id);
                if (authorFound != null)
                {
                    authorFound.setActiveStatus(true);
                    currentView.AuthorCollectionView.Refresh();
                }
                return;
            }
            if (button == "deactivate")
            {
                Author? authorFound = currentView.AuthorList.FirstOrDefault(author => author.Id == (parameter as Author).Id);
                if (authorFound != null)
                {
                    authorFound.setActiveStatus(false);
                    currentView.AuthorCollectionView.Refresh();
                }
                return;
            }
            if (button == "edit")
            {
                Author? authorFound = currentView.AuthorList.FirstOrDefault(author => author.Id == (parameter as Author).Id);
                if (authorFound == null)
                {
                    // TODO: log error
                    return;
                }

                navigation.currentViewModel = new CreateAuthorViewModel(session, navigation, true);
                CreateAuthorViewModel? currViewModel = navigation.currentViewModel as CreateAuthorViewModel;
                if (currViewModel != null)
                    currViewModel.Author = authorFound;

                return;
            }
        }

        // private
        private Session session;
        private Navigation navigation;
        private string button;

    }
}
