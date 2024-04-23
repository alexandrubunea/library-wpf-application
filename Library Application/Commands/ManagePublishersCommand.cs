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
    internal class ManagePublishersCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            if (parameter == null)
                return;

            if (button == "create")
            {
                navigation.currentViewModel = new CreatePublisherViewModel(session, navigation);
                return;
            }

            ManagePublishersViewModel? currentView = navigation.currentViewModel as ManagePublishersViewModel;

            if (button == "activate")
            {
                Publisher? publisherFound = currentView.PublisherList.FirstOrDefault(bookType => bookType.Id == (parameter as Publisher).Id);
                if (publisherFound != null)
                {
                    publisherFound.setActiveStatus(true);
                    currentView.PublisherCollectionView.Refresh();
                }
                return;
            }
            if (button == "deactivate")
            {
                Publisher? publisherFound = currentView.PublisherList.FirstOrDefault(bookType => bookType.Id == (parameter as Publisher).Id);
                if (publisherFound != null)
                {
                    publisherFound.setActiveStatus(false);
                    currentView.PublisherCollectionView.Refresh();
                }
                return;
            }
            if (button == "edit")
            {
                Publisher? publisherFound = currentView.PublisherList.FirstOrDefault(bookType => bookType.Id == (parameter as Publisher).Id);
                if (publisherFound != null)
                {
                    navigation.currentViewModel = new CreatePublisherViewModel(session, navigation, true);
                    CreatePublisherViewModel? currViewModel = navigation.currentViewModel as CreatePublisherViewModel;

                    if (currViewModel == null)
                        return;

                    currViewModel.Publisher = publisherFound;
                }
                return;
            }
        }

        public ManagePublishersCommand(string button, Session session, Navigation navigation)
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
