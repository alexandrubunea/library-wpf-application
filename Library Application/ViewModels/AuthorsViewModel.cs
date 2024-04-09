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
    class AuthorsViewModel : ViewModelBaseSideBar
    {
        public string FilterAuthor
        {
            get => filter_author;
            set
            {
                filter_author = value;
                OnPropertyChanged(FilterAuthor);
                AuthorCollectionView.Refresh();
            }
        }
        public string OrderAuthorBy
        {
            get => order_author_by;
            set
            {
                order_author_by = value;
                orderAuthorsList();
                OnPropertyChanged(OrderAuthorBy);
                AuthorCollectionView.Refresh();
            }
        }
        public string AscOrDescOrder
        {
            get => asc_or_desc_order;
            set
            {
                asc_or_desc_order = value;
                orderAuthorsList();
                OnPropertyChanged(OrderAuthorBy);
                AuthorCollectionView.Refresh();
            }
        }
        public ObservableCollection<Author> AuthorsList
        {
            get => authors_list;
            set
            {
                authors_list = value;
                AuthorCollectionView.Refresh();
            }
        }
        public ICollectionView AuthorCollectionView { get; }
        public ObservableCollection<string> OrderOptions { get; set; }
        public ObservableCollection<string> OrderAscDesc { get; set; }

        public AuthorsViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
            filter_author = string.Empty;
            order_author_by = "First Name";
            asc_or_desc_order = "Ascending";

            OrderOptions = new ObservableCollection<string> { "First Name", "Last Name", "Birthdate", "Number of books"};
            OrderAscDesc = new ObservableCollection<string> { "Ascending", "Descending" };

            authors_list = new ObservableCollection<Author>(DBUtils.retriveActiveAuthors());

            foreach(Author author in authors_list)
            {
                author.fetchNumberOfBooks();
            }

            authors_list = new ObservableCollection<Author>(authors_list.Where(author => author.NumberOfBooks != 0));

            authors_list = new ObservableCollection<Author>(authors_list.OrderBy(author => author.FirstName));

            AuthorCollectionView = CollectionViewSource.GetDefaultView(authors_list);

            AuthorCollectionView.Filter = FilterAuthors;

            orderAuthorsList();
        }

        // private
        private string filter_author;
        private string order_author_by;
        private string asc_or_desc_order;
        private ObservableCollection<Author> authors_list;

        private bool FilterAuthors(object obj)
        {
            if (obj is Author author)
            {
                return string.Concat(author.FirstName, " ", author.LastName).Contains(FilterAuthor, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        private void orderAuthorsList()
        {
            switch (OrderAuthorBy)
            {
                case "First Name":
                    AuthorCollectionView.SortDescriptions.Clear();
                    AuthorCollectionView.SortDescriptions.Add(
                        new SortDescription("FirstName", AscOrDescOrder == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending));
                    break;
                case "Last Name":
                    AuthorCollectionView.SortDescriptions.Clear();
                    AuthorCollectionView.SortDescriptions.Add(
                        new SortDescription("LastName", AscOrDescOrder == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending));
                    break;
                case "Birthdate":
                    AuthorCollectionView.SortDescriptions.Clear();
                    AuthorCollectionView.SortDescriptions.Add(
                        new SortDescription("BirthDateDate", AscOrDescOrder == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending));
                    break;
                case "Number of books":
                    AuthorCollectionView.SortDescriptions.Clear();
                    AuthorCollectionView.SortDescriptions.Add(
                        new SortDescription("NumberOfBooks", AscOrDescOrder == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending));
                    break;
                default:
                    break;
            }
        }
    }
}
