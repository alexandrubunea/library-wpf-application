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
        public ICommand SideBarItemsCommand { get; }
        public ICommand LogoutCommand { get; }

        public ObservableCollection<string> SideBarItems
        {
            get => items;
        }

        public ViewModelBaseSideBar(Session session, Navigation navigation)
        {
            this.session = session;
            this.navigation = navigation;

            LogoutCommand = new LogoutCommand(this.navigation);

            ObservableCollection<string> items = new ObservableCollection<string>()
            {
                "ALL BOOKS",
                "TYPE OF BOOKS",
                "AUTHORS",
                "PUBLISHERS",
                "BORROWED BOOKS"
            };

            if (session.User.AccessLevel > 0)
            {
                items.Add("MANAGE LIBRARY");
            }

            this.items = items;

            SideBarItemsCommand = new SideBarItemsCommand(navigation, session);
        }

        // protected
        protected readonly Session session;
        protected readonly Navigation navigation;

        // private
        private readonly ObservableCollection<string> items;
    }
}
