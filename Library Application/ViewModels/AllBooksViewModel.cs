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
    class AllBooksViewModel : ViewModelBaseSideBar
    {
        public ICommand BorrowBook { get; set; }
        public string FilterBook
        {
            get => filter_book;
            set
            {
                filter_book = value;
                OnPropertyChanged(FilterBook);
                BookCollectionView.Refresh();
            }
        }
        public string OrderBookBy
        {
            get => order_book_by;
            set
            {
                order_book_by = value;
                orderBookList();
                OnPropertyChanged(OrderBookBy);
                BookCollectionView.Refresh();
            }
        }
        public string AscOrDescOrder
        {
            get => asc_or_desc_order;
            set
            {
                asc_or_desc_order = value;
                orderBookList();
                OnPropertyChanged(OrderBookBy);
                BookCollectionView.Refresh();
            }
        }
        public ObservableCollection<Book> BooksList
        {
            get => books_list;
            set
            {
                books_list = value;
                BookCollectionView.Refresh();
            }
        }
        public ICollectionView BookCollectionView { get; }
        public ObservableCollection<string> OrderOptions { get; set; }
        public ObservableCollection<string> OrderAscDesc { get; set; }

        public AllBooksViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
            BorrowBook = new BookCommand("borrow", session, navigation);

            filter_book = string.Empty;
            order_book_by = "Title";
            asc_or_desc_order = "Ascending";

            OrderOptions = new ObservableCollection<string> { "Title", "Author", "Publisher", "Type", "Publish date" };
            OrderAscDesc = new ObservableCollection<string> { "Ascending", "Descending" };

            books_list = new ObservableCollection<Book>(DBUtils.retriveActiveBooks());
            books_list = new ObservableCollection<Book>(books_list.OrderBy(book => book.Title));

            BookCollectionView = CollectionViewSource.GetDefaultView(books_list);

            BookCollectionView.Filter = FilterBooks;

            orderBookList();
        }

        // private
        private string filter_book;
        private string order_book_by;
        private string asc_or_desc_order;
        private ObservableCollection<Book> books_list;

        private bool FilterBooks(object obj)
        {
            if (obj is Book book)
            {
                return book.Title.Contains(FilterBook, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        private void orderBookList()
        {
            switch (OrderBookBy)
            {
                case "Title":
                    BookCollectionView.SortDescriptions.Clear();
                    BookCollectionView.SortDescriptions.Add(
                        new SortDescription("Title", AscOrDescOrder == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending));
                    break;
                case "Author":
                    BookCollectionView.SortDescriptions.Clear();
                    BookCollectionView.SortDescriptions.Add(
                        new SortDescription("Authors[0].FirstName", AscOrDescOrder == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending));
                    break;
                case "Publisher":
                    BookCollectionView.SortDescriptions.Clear();
                    BookCollectionView.SortDescriptions.Add(
                        new SortDescription("Publisher.Name", AscOrDescOrder == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending));
                    break;
                case "Type":
                    BookCollectionView.SortDescriptions.Clear();
                    BookCollectionView.SortDescriptions.Add(
                        new SortDescription("BookType.Name", AscOrDescOrder == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending));
                    break;
                case "Publish date":
                    BookCollectionView.SortDescriptions.Clear();
                    BookCollectionView.SortDescriptions.Add(
                        new SortDescription("PublishDate", AscOrDescOrder == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending));
                    break;
                default:
                    break;
            }
        }
    }
}
