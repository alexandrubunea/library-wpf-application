using Library_Application.Commands;
using Library_Application.Database;
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
using Library_Application.Models;

namespace Library_Application.ViewModels
{
    class ManageAuthorsViewModel : ViewModelBaseSideBar
    {
        public ICommand CreateAuthor { get; }
        public ICommand ActivateCommand { get; }
        public ICommand DeactivateCommand { get; }
        public ICommand EditCommand { get; }


        public ObservableCollection<Author> AuthorList
        {
            get => author_list;
            set
            {
                author_list = value;
                AuthorCollectionView.Refresh();
            }
        }

        public ICollectionView AuthorCollectionView { get; }

        public string AuthorFilter
        {
            get => author_filter;
            set
            {
                author_filter = value;
                OnPropertyChanged(nameof(AuthorFilter));
                AuthorCollectionView.Refresh();
            }
        }

        public ManageAuthorsViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
            CreateAuthor = new ManageAuthorsCommand(session, navigation, "create");
            ActivateCommand = new ManageAuthorsCommand(session, navigation, "activate");
            DeactivateCommand = new ManageAuthorsCommand(session, navigation, "deactivate");
            EditCommand = new ManageAuthorsCommand(session, navigation, "edit");

            author_list = new ObservableCollection<Author>(DBUtils.retriveAuthors());
            AuthorCollectionView = CollectionViewSource.GetDefaultView(author_list);

            author_filter = string.Empty;
            AuthorCollectionView.Filter = FilterAuthors;
        }

        // private
        private ObservableCollection<Author> author_list;
        private string author_filter;
        private bool FilterAuthors(object obj)
        {
            if (obj is Author author)
            {
                return author.FirstName.Contains(AuthorFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       author.LastName.Contains(AuthorFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
