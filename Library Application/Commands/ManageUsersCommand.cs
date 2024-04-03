using Library_Application.Models;
using Library_Application.Stores;
using Library_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Library_Application.Commands
{
    internal class ManageUsersCommand : CommandBase
    {
        // public
        public ManageUsersCommand(string button, Navigation navigation)
        {
            this.button = button;
            this.navigation = navigation;
        }
        public override void Execute(object? parameter)
        {
            if (parameter == null)
                return;

            ManageUsersViewModel? currentView = navigation.currentViewModel as ManageUsersViewModel;

            if (button == "activate")
            {
                RestrictedDataUser? userFound = currentView.UsersList.FirstOrDefault(user => user.Id == (parameter as RestrictedDataUser).Id);
                if (userFound != null)
                {
                    userFound.setActiveStatus(true);
                    currentView.UsersCollectionView.Refresh();
                }
                return;
            }
            if (button == "deactivate")
            {
                RestrictedDataUser? userFound = currentView.UsersList.FirstOrDefault(user => user.Id == (parameter as RestrictedDataUser).Id);
                if (userFound != null)
                {
                    userFound.setActiveStatus(false);
                    currentView.UsersCollectionView.Refresh();
                }
                return;
            }
        }

        // private
        private readonly string button;
        private readonly Navigation navigation;

    }
}
