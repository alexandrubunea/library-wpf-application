using Library_Application.Database;
using Library_Application.Models;
using Library_Application.Stores;
using Library_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.Commands
{
    internal class BorrowedBooksCommand : CommandBase
    {
        public BorrowedBooksCommand(string button, Session session, Navigation navigation)
        {
            this.button = button;
            this.session = session;
            this.navigation = navigation;
        }

        public override void Execute(object? parameter)
        {
            if (parameter == null)
                return;

            if(button == "markreturn")
            {
                BorrowedBooksViewModel? currentViewModel = navigation.currentViewModel as BorrowedBooksViewModel;
                UserBook? borrow = currentViewModel.BorrowedBooksList.FirstOrDefault(bborrow => bborrow.Id == (parameter as UserBook).Id);

                if(borrow != null)
                {
                    borrow.setActiveStatus(false);
                    DBUtils.increaseBookStock(borrow.Book.Id);
                    currentViewModel.BorrowedBooksList.Remove(borrow);
                    currentViewModel.BookBorrowsCollectionView.Refresh();
                }
            }
        }

        // private
        private readonly string button;
        private readonly Session session;
        private readonly Navigation navigation;
    }
}
