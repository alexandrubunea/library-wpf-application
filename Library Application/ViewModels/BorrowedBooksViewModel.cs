using Library_Application.Database;
using Library_Application.Models;
using Library_Application.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.ViewModels
{
    class BorrowedBooksViewModel : ViewModelBaseSideBar
    {
        public ObservableCollection<UserBook> BorrowedBooksList
        {
            get => borrowed_books_list;
            set
            {
                borrowed_books_list = value;
                OnPropertyChanged(nameof(BorrowedBooksList));
            }
        }


        public BorrowedBooksViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
            borrowed_books_list = new ObservableCollection<UserBook>(DBUtils.getUserBorrows(session.User.Id));
        }

        // private
        private ObservableCollection<UserBook> borrowed_books_list;
    }
}
