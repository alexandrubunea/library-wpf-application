using Library_Application.Commands;
using Library_Application.Database;
using Library_Application.Models;
using Library_Application.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library_Application.ViewModels
{
    class ManageBookTypesViewModel : ViewModelBaseSideBar
    {
        // public
        public ICommand CreateBookType { get; }

        public ObservableCollection<BookType> BookTypesList
        {
            get => book_type_list;
            set
            {
                book_type_list = value;
            }
        }

        public ManageBookTypesViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
            CreateBookType = new ManageBookTypesCommand("create", session, navigation);

            book_type_list = new ObservableCollection<BookType>(DBUtils.retriveBookTypes());
        }

        // private
        private ObservableCollection<BookType> book_type_list;
    }
}
