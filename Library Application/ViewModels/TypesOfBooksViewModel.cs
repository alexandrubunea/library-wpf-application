using Library_Application.Database;
using Library_Application.Models;
using Library_Application.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Library_Application.ViewModels
{
    class TypesOfBooksViewModel : ViewModelBaseSideBar
    {
        public string FilterBookType
        {
            get => filter_book_type;
            set
            {
                filter_book_type = value;
                OnPropertyChanged(FilterBookType);
                BookTypeCollectionView.Refresh();
            }
        }
        public string OrderBookTypeBy
        {
            get => order_book_type_by;
            set
            {
                order_book_type_by = value;
                orderPublishersList();
                OnPropertyChanged(OrderBookTypeBy);
                BookTypeCollectionView.Refresh();
            }
        }
        public string AscOrDescOrder
        {
            get => asc_or_desc_order;
            set
            {
                asc_or_desc_order = value;
                orderPublishersList();
                OnPropertyChanged(OrderBookTypeBy);
                BookTypeCollectionView.Refresh();
            }
        }
        public ObservableCollection<BookType> BookTypeList
        {
            get => book_type_list;
            set
            {
                book_type_list = value;
                BookTypeCollectionView.Refresh();
            }
        }
        public ICollectionView BookTypeCollectionView { get; }
        public ObservableCollection<string> OrderOptions { get; set; }
        public ObservableCollection<string> OrderAscDesc { get; set; }

        public TypesOfBooksViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
            filter_book_type = string.Empty;
            order_book_type_by = "Name";
            asc_or_desc_order = "Ascending";

            OrderOptions = new ObservableCollection<string> { "Name", "Number of books" };
            OrderAscDesc = new ObservableCollection<string> { "Ascending", "Descending" };

            book_type_list = new ObservableCollection<BookType>(DBUtils.retriveActiveBookTypes());

            foreach (BookType publisher in book_type_list)
            {
                publisher.fetchNumberOfBooks();
            }

            book_type_list = new ObservableCollection<BookType>(book_type_list.Where(book_type => book_type.NumberOfBooks != 0));

            book_type_list = new ObservableCollection<BookType>(book_type_list.OrderBy(bookType => bookType.Name));

            BookTypeCollectionView = CollectionViewSource.GetDefaultView(book_type_list);

            BookTypeCollectionView.Filter = FilterPublishers;

            orderPublishersList();
        }

        // private
        private string filter_book_type;
        private string order_book_type_by;
        private string asc_or_desc_order;
        private ObservableCollection<BookType> book_type_list;

        private bool FilterPublishers(object obj)
        {
            if (obj is BookType bookType)
            {
                return bookType.Name.Contains(FilterBookType, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        private void orderPublishersList()
        {
            switch (OrderBookTypeBy)
            {
                case "Name":
                    BookTypeCollectionView.SortDescriptions.Clear();
                    BookTypeCollectionView.SortDescriptions.Add(
                        new SortDescription("Name", AscOrDescOrder == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending));
                    break;
                case "Number of books":
                    BookTypeCollectionView.SortDescriptions.Clear();
                    BookTypeCollectionView.SortDescriptions.Add(
                        new SortDescription("NumberOfBooks", AscOrDescOrder == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending));
                    break;
                default:
                    break;
            }
        }
    }
}
