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
    internal class BookCommand : CommandBase
    {
        // public
        public BookCommand(string button, Session session, Navigation navigation)
        {
            this.button = button;
            this.session = session;
            this.navigation = navigation;
        }

        public override void Execute(object? parameter)
        {
            if (parameter == null)
                return;

            AllBooksViewModel? currentView = navigation.currentViewModel as AllBooksViewModel;

            if (button == "borrow")
            {
                Book? bookFound = currentView.BooksList.FirstOrDefault(book => book.Id == (parameter as Book).Id);
                if (bookFound != null && !DBUtils.doesBorrowExists(session.User.Id, bookFound.Id))
                {
                    string currentDate = (DateTime.UtcNow.Date).ToString("dd/MM/yyyy");
                    string returnDate = (DateTime.UtcNow.Date.AddDays(30)).ToString("dd/MM/yyyy");
                    UserBook borrow = new UserBook(RestrictedDataUser.ConvertUserToRDUser(session.User), bookFound, currentDate, returnDate);
                    borrow.store();
                    DBUtils.decreaseBookStock(bookFound.Id);
                    currentView.BooksList.FirstOrDefault(book => book.Id == bookFound.Id).Stock -= 1;
                    currentView.BookCollectionView.Refresh();
                }
                return;
            }
        }

        // private
        private readonly string button;
        private readonly Session session;
        private readonly Navigation navigation;
    }
}
