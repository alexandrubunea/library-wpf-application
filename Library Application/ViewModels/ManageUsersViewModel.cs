using Library_Application.Commands;
using Library_Application.Database;
using Library_Application.Stores;
using Library_Application.Models;
using Library_Application.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Library_Application.ViewModels
{
    class ManageUsersViewModel : ViewModelBaseSideBar
    {
        // public
        public ICommand ActivateCommand { get; }
        public ICommand DeactivateCommand { get; }
        public ObservableCollection<RestrictedDataUser> UsersList
        {
            get => users_list;
            set
            {
                users_list = value;
                UsersCollectionView.Refresh();
            }
        }

        public ICollectionView UsersCollectionView { get; }

        public string UsersFilter
        {
            get => users_filter;
            set
            {
                users_filter = value;
                OnPropertyChanged(nameof(UsersFilter));
                UsersCollectionView.Refresh();
            }
        }
        public ManageUsersViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
            ActivateCommand = new ManageUsersCommand("activate", navigation);
            DeactivateCommand = new ManageUsersCommand("deactivate", navigation);

            users_list = new ObservableCollection<RestrictedDataUser>(DBUtils.retriveUsers());
            UsersCollectionView = CollectionViewSource.GetDefaultView(users_list);

            users_filter = string.Empty;
            UsersCollectionView.Filter = FilterUsers;
        }

        // private
        private ObservableCollection<RestrictedDataUser> users_list;
        private string users_filter;
        private bool FilterUsers(object obj)
        {
            if (obj is RestrictedDataUser user)
            {
                    return user.Email.Contains(UsersFilter, StringComparison.InvariantCultureIgnoreCase) ||
                           user.Phone.Contains(UsersFilter, StringComparison.InvariantCultureIgnoreCase) ||
                           string.Concat(user.FirstName, " ", user.LastName).Contains(UsersFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
