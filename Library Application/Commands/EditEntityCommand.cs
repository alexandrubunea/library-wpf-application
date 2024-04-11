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
    internal class EditEntityCommand : CommandBase
    {
        // public
        public override void Execute(object? parameter)
        {
            if (entity == "booktype")
            {
                EditBookTypeViewModel? currentViewModel = navigation.currentViewModel as EditBookTypeViewModel;

                if (button == "cancel")
                {
                    navigation.currentViewModel = new ManageBookTypesViewModel(session, navigation);
                    return;
                }

                if (button == "edit" && currentViewModel.Name != string.Empty && !currentViewModel.HasErrors)
                {
                    currentViewModel.BookTypeAlreadyExists = false;

                    currentViewModel.BookType.Name = currentViewModel.Name;
                    currentViewModel.BookType.update();

                    navigation.currentViewModel = new ManageBookTypesViewModel(session, navigation);
                }
            }
            if (entity == "publisher")
            {
                EditPublisherViewModel? currentViewModel = navigation.currentViewModel as EditPublisherViewModel;

                if (button == "cancel")
                {
                    navigation.currentViewModel = new ManagePublishersViewModel(session, navigation);
                    return;
                }

                if (button == "edit" && currentViewModel.Name != string.Empty && !currentViewModel.HasErrors)
                {
                    currentViewModel.PublisherAlreadyExists = false;

                    currentViewModel.Publisher.Name = currentViewModel.Name;
                    currentViewModel.Publisher.update();

                    navigation.currentViewModel = new ManagePublishersViewModel(session, navigation);
                }
            }
            if (entity == "author")
            {
                EditAuthorViewModel? currentViewModel = navigation.currentViewModel as EditAuthorViewModel;

                if (button == "cancel")
                {
                    navigation.currentViewModel = new ManageAuthorsViewModel(session, navigation);
                    return;
                }

                if (button == "edit" && !currentViewModel.HasErrors && !currentViewModel.EmptyFields)
                {
                    currentViewModel.AuthorAlreadyExists = false;


                    currentViewModel.Author.FirstName = currentViewModel.FirstName;
                    currentViewModel.Author.LastName = currentViewModel.LastName;
                    currentViewModel.Author.BirthDate = currentViewModel.BirthDate;

                    currentViewModel.Author.update();

                    navigation.currentViewModel = new ManageAuthorsViewModel(session, navigation);
                }
            }
            if (entity == "book")
            {
                EditBookViewModel? currentViewModel = navigation.currentViewModel as EditBookViewModel;

                if (button == "cancel")
                {
                    navigation.currentViewModel = new ManageBooksViewModel(session, navigation);
                    return;
                }

                if (button == "edit" && !currentViewModel.HasErrors && !currentViewModel.EmptyFields)
                {
                    currentViewModel.BookAlreadyExists = false;

                    currentViewModel.Book.Title = currentViewModel.Title;
                    currentViewModel.Book.PublishYear = currentViewModel.PublishDate;
                    currentViewModel.Book.BookType = currentViewModel.BookType;
                    currentViewModel.Book.Publisher = currentViewModel.Publisher;
                    currentViewModel.Book.Authors = new List<Author>(currentViewModel.Authors);
                    currentViewModel.Book.Stock = Convert.ToInt32(currentViewModel.Stock);
                    currentViewModel.Book.update();

                    navigation.currentViewModel = new ManageBooksViewModel(session, navigation);
                }
            }
        }

        public EditEntityCommand(string entity, string button, Session session, Navigation navigation)
        {
            this.entity = entity;
            this.button = button;
            this.session = session;
            this.navigation = navigation;
        }

        // private
        private readonly string entity;
        private readonly string button;
        private readonly Session session;
        private readonly Navigation navigation;
    }
}
