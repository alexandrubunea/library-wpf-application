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
    class ManageBookTypesViewModel : ViewModelBaseSideBar
    {
        // public
        public ICommand CreateBookType { get; }
        public ICommand ActivateCommand { get; }
        public ICommand DeactivateCommand { get; }
        public ICommand EditCommand { get; }

        public ObservableCollection<BookType> BookTypesList
        {
            get => book_type_list;
            set
            {
                book_type_list = value;
                BookTypesCollectionView.Refresh();
            }
        }

        public ICollectionView BookTypesCollectionView { get; }

        public string BookTypesFilter
        {
            get => book_types_filter;
            set
            {
                book_types_filter = value;
                OnPropertyChanged(nameof(BookTypesFilter));
                BookTypesCollectionView.Refresh();
            }
        }

        public ManageBookTypesViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
            CreateBookType = new ManageBookTypesCommand("create", session, navigation);
            ActivateCommand = new ManageBookTypesCommand("activate", session, navigation);
            DeactivateCommand = new ManageBookTypesCommand("deactivate", session, navigation);
            EditCommand = new ManageBookTypesCommand("edit", session, navigation);

            book_type_list = new ObservableCollection<BookType>(DBUtils.retriveBookTypes());
            BookTypesCollectionView = CollectionViewSource.GetDefaultView(book_type_list);

            book_types_filter = string.Empty;
            BookTypesCollectionView.Filter = FilterBookTypes;
        }


        // private
        private ObservableCollection<BookType> book_type_list;
        private string book_types_filter;
        private bool FilterBookTypes(object obj)
        {
            if(obj is BookType bookType)
            {
                return bookType.Name.Contains(BookTypesFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

    }
}
