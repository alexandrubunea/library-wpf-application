using Library_Application.Commands;
using Library_Application.Database;
using Library_Application.Models;
using Library_Application.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Library_Application.ViewModels
{
    internal class CreateBookViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        // public
        public ICommand AddAuthor { get; }
        public ICommand RemoveAuthor { get; }

        public ICommand SaveButton { get; }
        public ICommand CancelButton { get; }

        public string Title
        {
            get => title;
            set
            {
                title = value;

                ClearErrors(nameof(Title));
                if (string.IsNullOrEmpty(title))
                {
                    AddError(nameof(Title), "* This field is required.");
                }

                OnPropertyChanged(nameof(Title));
            }
        }
        public string PublishDate
        {
            get => publish_date;
            set
            {
                publish_date = value;

                ClearErrors(nameof(PublishDate));
                if (!Regex.IsMatch(publish_date, @"^(0?[1-9]|[12][0-9]|3[01])\.(0?[1-9]|1[0-2])\.\d{4}$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    AddError(nameof(PublishDate), "* You must introduce a date that respects the format!");
                }

                OnPropertyChanged(nameof(PublishDate));
            }
        }
        public BookType BookType
        {
            get => book_type;
            set
            {
                book_type = value;
                OnPropertyChanged(nameof(BookType));
            }
        }
        public Publisher Publisher
        {
            get => publisher;
            set
            {
                publisher = value;
                OnPropertyChanged(nameof(Publisher));
            }
        }
        public ObservableCollection<Author> Authors
        {
            get => authors;
            set
            {
                authors = value;
                OnPropertyChanged(nameof(Authors));
            }
        }

        public String Stock
        {
            get => stock;
            set
            {
                stock = value;

                ClearErrors(nameof(Stock));
                if (!Regex.IsMatch(stock, @"^[1-9]\d*$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    AddError(nameof(Stock), "* You must introduce a positive number.");
                }

                OnPropertyChanged(nameof(Stock));
            }
        }
        public ObservableCollection<Author> AllAuthors
        {
            get => all_authors;
        }
        public ObservableCollection<Publisher> AllPublishers
        {
            get => all_publishers;
        }
        public ObservableCollection<BookType> AllBookTypes
        {
            get => all_book_types;
        }
        public ICollectionView CurrentAuthorsCollectionView { get; set; }

        public Author SelectedAllAuthor
        {
            get => selected_all_author;
            set
            {
                selected_all_author = value;
                OnPropertyChanged(nameof(SelectedAllAuthor));
            }
        }

        public Author SelectedBookAuthor
        {
            get => selected_book_author;
            set
            {
                selected_book_author = value;
                OnPropertyChanged(nameof(SelectedBookAuthor));
            }
        }

        public bool BookAlreadyExists
        {
            get => book_already_exists;
            set
            {
                book_already_exists = value;
                OnPropertyChanged(nameof(BookAlreadyExists));
            }
        }

        public bool EmptyFields
        {
            get => Title == string.Empty || PublishDate == string.Empty || Stock == string.Empty ||
                book_type.Name == string.Empty || publisher.Name == string.Empty || authors.Count == 0;
        }

        public bool HasErrors => property_errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            /*
             * For a reason, idk why, after a good call on GetErrors with propertyName not being null
             *  another call it's made where propertyName it's null, it's only happening in this ViewModel.
             *  
             *  But this should fix it.
             * */

            if (propertyName == null)
            {
                return Enumerable.Empty<string>();
            }

            return property_errors.GetValueOrDefault(propertyName, []);
        }
        public void AddError(string propertyName, string errorMessage)
        {
            if (!property_errors.ContainsKey(propertyName))
            {
                property_errors.Add(propertyName, new List<string>());
            }

            property_errors[propertyName].Add(errorMessage);
            OnErrorsChange(propertyName);
        }

        public void ClearErrors(string propertyName)
        {
            if (property_errors.Remove(propertyName))
            {
                OnErrorsChange(propertyName);
            }
        }
        public bool EditMode
        {
            get => edit_mode;
        }

        public Book Book
        {
            get => book;
            set
            {
                book = value;

                BookAlreadyExists = false;

                Title = book.Title;
                PublishDate = book.PublishYear;
                Stock = Convert.ToString(book.Stock);

#pragma warning disable CS8601 // Possible null reference assignment.
                BookType = all_book_types.FirstOrDefault(book_type => book_type.Id == book.BookType.Id);
                Publisher = all_publishers.FirstOrDefault(publisher => publisher.Id == book.Publisher.Id);
#pragma warning restore CS8601 // Possible null reference assignment.

                if (book_type == null)
                    book_type = new BookType(string.Empty);
                if (publisher == null)
                    publisher = new Publisher(string.Empty);

                Authors = new ObservableCollection<Author>(book.Authors);
                CurrentAuthorsCollectionView = CollectionViewSource.GetDefaultView(authors);
                CurrentAuthorsCollectionView.Refresh();

                book_already_exists = false;
            }
        }

        public CreateBookViewModel(Session session, Navigation navigation, bool edit_mode=false)
        {
            this.session = session;
            this.navigation = navigation;

            title = string.Empty;
            publish_date = string.Empty;
            stock = string.Empty;
            book_type = new BookType(string.Empty);
            publisher = new Publisher(string.Empty);
            authors = new ObservableCollection<Author>();

            book_already_exists = false;

            all_book_types = new ObservableCollection<BookType>(DBUtils.retriveBookTypes());
            all_publishers = new ObservableCollection<Publisher>(DBUtils.retrivePublishers());
            all_authors = new ObservableCollection<Author>(DBUtils.retriveAuthors());

            selected_all_author = new Author(string.Empty, string.Empty, string.Empty);
            selected_book_author = new Author(string.Empty, string.Empty, string.Empty);

            CurrentAuthorsCollectionView = CollectionViewSource.GetDefaultView(authors);

            AddAuthor = new CreateBookCommand(navigation, "addauthor");
            RemoveAuthor = new CreateBookCommand(navigation, "removeauthor");

            SaveButton = new CreateEntityCommand("book", "save", session, navigation);
            CancelButton = new CreateEntityCommand("book", "cancel", session, navigation);

            book = new Book(
                string.Empty,
                string.Empty,
                new BookType(string.Empty),
                new Publisher(string.Empty),
                new List<Author>(),
            0);

            this.edit_mode = edit_mode;
        }

        // private
        private readonly Session session;
        private readonly Navigation navigation;
        private readonly Dictionary<string, List<string>> property_errors = new Dictionary<string, List<string>>();

        private string title;
        private string publish_date;
        private BookType book_type;
        private Publisher publisher;
        private ObservableCollection<Author> authors;
        private string stock;

        private bool book_already_exists;

        private readonly ObservableCollection<BookType> all_book_types;
        private readonly ObservableCollection<Publisher> all_publishers;
        private readonly ObservableCollection<Author> all_authors;

        private Author selected_all_author;
        private Author selected_book_author;

        private Book book;
        private bool edit_mode;

        private void OnErrorsChange(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
