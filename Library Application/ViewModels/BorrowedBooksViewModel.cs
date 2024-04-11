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
    class BorrowedBooksViewModel : ViewModelBaseSideBar
    {
        // public
        public ICommand MarkReturn { get; }
        public ObservableCollection<UserBook> BorrowedBooksList
        {
            get => borrowed_books_list;
            set
            {
                borrowed_books_list = value;
                OnPropertyChanged(nameof(BorrowedBooksList));
            }
        }

        public ICollectionView BookBorrowsCollectionView { get; }

        public BorrowedBooksViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
            MarkReturn = new BorrowedBooksCommand("markreturn", session, navigation);
            borrowed_books_list = new ObservableCollection<UserBook>(DBUtils.getUserBorrows(session.User.Id));
            borrowed_books_list = new ObservableCollection<UserBook>(borrowed_books_list.OrderBy(borrowed_book => borrowed_book.DateForOrgz));
            BookBorrowsCollectionView = CollectionViewSource.GetDefaultView(borrowed_books_list);
        }

        // private
        private ObservableCollection<UserBook> borrowed_books_list;
    }
}
