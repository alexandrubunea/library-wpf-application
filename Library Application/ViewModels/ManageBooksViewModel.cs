using Library_Application.Commands;
using Library_Application.Database;
using Library_Application.Models;
using Library_Application.Stores;
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
    class ManageBooksViewModel : ViewModelBaseSideBar
    {
        // public
        public ICommand CreateBook { get; }
        public ICommand ActivateCommand { get; }
        public ICommand DeactivateCommand { get; }
        public ICommand EditCommand { get; }

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

        public string BookFilter
        {
            get => book_filter;
            set
            {
                book_filter = value;
                OnPropertyChanged(nameof(BookFilter));
                BookCollectionView.Refresh();
            }
        }
        public ManageBooksViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
            CreateBook = new ManageBooksCommand(session, navigation, "create");
            ActivateCommand = new ManageBooksCommand(session, navigation, "activate");
            DeactivateCommand = new ManageBooksCommand(session, navigation, "deactivate");
            EditCommand = new ManageBooksCommand(session, navigation, "edit");

            books_list = new ObservableCollection<Book>(DBUtils.retriveBooks());
            BookCollectionView = CollectionViewSource.GetDefaultView(books_list);

            book_filter = string.Empty;
            BookCollectionView.Filter = FilterBooks;
        }

        // private
        private ObservableCollection<Book> books_list;
        private string book_filter;
        private bool FilterBooks(object obj)
        {
            if (obj is Book book)
            {
                bool does_contains_author_name = false;

                foreach(Author author in book.Authors)
                {
                    if(string.Concat(author.FirstName, " ", author.LastName).Contains(BookFilter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        does_contains_author_name = true;
                        break;
                    }
                }

                return book.Title.Contains(BookFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       book.Publisher.Name.Contains(BookFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       book.BookType.Name.Contains(BookFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       does_contains_author_name;
            }
            return false;
        }

        // private
    }
}
