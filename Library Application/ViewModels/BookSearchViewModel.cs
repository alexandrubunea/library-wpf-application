using Library_Application.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.ViewModels
{
    class BookSearchViewModel : ViewModelBaseSideBar
    {
        public BookSearchViewModel(Session session, Navigation navigation) : base(session, navigation)
        {
        }
    }
}
