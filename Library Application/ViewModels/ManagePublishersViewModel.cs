using Library_Application.Commands;
using Library_Application.Database;
using Library_Application.Models;
using Library_Application.Stores;
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
    class ManagePublishersViewModel : ViewModelBaseSideBar
    {
        // public
        public ICommand CreatePublisher { get; }
        public ICommand ActivateCommand { get; }
        public ICommand DeactivateCommand { get; }
        public ICommand EditCommand { get; }

        public ObservableCollection<Publisher> PublisherList
        {
            get => publisher_list;
            set
            {
                publisher_list = value;
                PublisherCollectionView.Refresh();
            }
        }

        public ICollectionView PublisherCollectionView { get; }

        public string PublisherFilter
        {
            get => publisher_filter;
            set
            {
                publisher_filter = value;
                OnPropertyChanged(nameof(PublisherFilter));
                PublisherCollectionView.Refresh();
            }
        }

        public ManagePublishersViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
            CreatePublisher = new ManagePublishersCommand("create", session, navigation);
            ActivateCommand = new ManagePublishersCommand("activate", session, navigation);
            DeactivateCommand = new ManagePublishersCommand("deactivate", session, navigation);
            EditCommand = new ManagePublishersCommand("edit", session, navigation);

            publisher_list = new ObservableCollection<Publisher>(DBUtils.retrivePublishers());
            PublisherCollectionView = CollectionViewSource.GetDefaultView(publisher_list);

            publisher_filter = string.Empty;
            PublisherCollectionView.Filter = FilterPublisher;
        }


        // private
        private ObservableCollection<Publisher> publisher_list;
        private string publisher_filter;
        private bool FilterPublisher(object obj)
        {
            if (obj is Publisher publisher)
            {
                return publisher.Name.Contains(PublisherFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
