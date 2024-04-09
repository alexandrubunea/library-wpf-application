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

namespace Library_Application.ViewModels
{
    class PublishingViewModel : ViewModelBaseSideBar
    {
        public string FilterPublisher
        {
            get => filter_publisher;
            set
            {
                filter_publisher = value;
                OnPropertyChanged(FilterPublisher);
                PublisherCollectionView.Refresh();
            }
        }
        public string OrderPublisherBy
        {
            get => order_publisher_by;
            set
            {
                order_publisher_by = value;
                orderPublishersList();
                OnPropertyChanged(OrderPublisherBy);
                PublisherCollectionView.Refresh();
            }
        }
        public string AscOrDescOrder
        {
            get => asc_or_desc_order;
            set
            {
                asc_or_desc_order = value;
                orderPublishersList();
                OnPropertyChanged(OrderPublisherBy);
                PublisherCollectionView.Refresh();
            }
        }
        public ObservableCollection<Publisher> PublisherList
        {
            get => publishers_list;
            set
            {
                publishers_list = value;
                PublisherCollectionView.Refresh();
            }
        }
        public ICollectionView PublisherCollectionView { get; }
        public ObservableCollection<string> OrderOptions { get; set; }
        public ObservableCollection<string> OrderAscDesc { get; set; }

        public PublishingViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
            filter_publisher = string.Empty;
            order_publisher_by = "Name";
            asc_or_desc_order = "Ascending";

            OrderOptions = new ObservableCollection<string> { "Name", "Number of books" };
            OrderAscDesc = new ObservableCollection<string> { "Ascending", "Descending" };

            publishers_list = new ObservableCollection<Publisher>(DBUtils.retriveActivePublishers());

            foreach (Publisher publisher in publishers_list)
            {
                publisher.fetchNumberOfBooks();
            }

            publishers_list = new ObservableCollection<Publisher>(publishers_list.Where(publisher => publisher.NumberOfBooks != 0));

            publishers_list = new ObservableCollection<Publisher>(publishers_list.OrderBy(publisher => publisher.Name));

            PublisherCollectionView = CollectionViewSource.GetDefaultView(publishers_list);

            PublisherCollectionView.Filter = FilterPublishers;

            orderPublishersList();
        }

        // private
        private string filter_publisher;
        private string order_publisher_by;
        private string asc_or_desc_order;
        private ObservableCollection<Publisher> publishers_list;

        private bool FilterPublishers(object obj)
        {
            if (obj is Publisher publisher)
            {
                return publisher.Name.Contains(FilterPublisher, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        private void orderPublishersList()
        {
            switch (OrderPublisherBy)
            {
                case "Name":
                    PublisherCollectionView.SortDescriptions.Clear();
                    PublisherCollectionView.SortDescriptions.Add(
                        new SortDescription("Name", AscOrDescOrder == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending));
                    break;
                case "Number of books":
                    PublisherCollectionView.SortDescriptions.Clear();
                    PublisherCollectionView.SortDescriptions.Add(
                        new SortDescription("NumberOfBooks", AscOrDescOrder == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending));
                    break;
                default:
                    break;
            }
        }
    }
}
