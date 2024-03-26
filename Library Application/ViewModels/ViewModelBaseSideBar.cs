using Library_Application.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Library_Application.Commands;

namespace Library_Application.ViewModels
{
    class ViewModelBaseSideBar : ViewModelBase
    {
        // public
        public ObservableCollection<string> SideBarItems
        {
            get => items;
        }

        public ViewModelBaseSideBar(Session session, Navigation navigation)
        {
            this.session = session;
            this.navigation = navigation;

            ObservableCollection<string> items = new ObservableCollection<string>()
            {
                "ALL BOOKS",
                "SEARCH A BOOK",
                "TYPE OF BOOKS",
                "AUTHORS",
                "PUBLISHING",
                "BORROWED BOOKS"
            };

            if(session.User.AccessLevel > 0)
            {
                items.Add("MANAGE LIBRARY");
            }

            this.items = items;
        }

        // protected
        protected readonly Session session;
        protected readonly Navigation navigation;

        // private
        private readonly ObservableCollection<string> items;
    }
}
