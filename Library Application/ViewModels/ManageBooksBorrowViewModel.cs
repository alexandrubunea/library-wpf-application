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
    class ManageBooksBorrowViewModel : ViewModelBaseSideBar
    {
        public ObservableCollection<UserBook> AllBorrowsList
        {
            get => all_borrows_list;
            set
            {
                all_borrows_list = value;
                OnPropertyChanged(nameof(AllBorrowsList));
            }
        }
        public ICollectionView AllBorrowsCollectionView { get; }

        public ManageBooksBorrowViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
            all_borrows_list = new ObservableCollection<UserBook>(DBUtils.retriveBookBorrows());
            all_borrows_list = new ObservableCollection<UserBook>(all_borrows_list
                .OrderByDescending(borrowed_book => borrowed_book.DateForOrgz <= DateTime.Now)
                .ThenBy(borrowed_book => Math.Abs((borrowed_book.DateForOrgz - DateTime.Now).TotalDays)));
            AllBorrowsCollectionView = CollectionViewSource.GetDefaultView(all_borrows_list);
        }

        // private
        private ObservableCollection<UserBook> all_borrows_list;
    }
}
