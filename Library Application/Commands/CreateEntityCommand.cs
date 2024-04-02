﻿using Library_Application.Database;
using Library_Application.Stores;
using Library_Application.ViewModels;
using Library_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.Commands
{
    class CreateEntityCommand : CommandBase
    {
        // public
        public override void Execute(object? parameter)
        {
            if(entity == "booktype")
            {
                CreateBookTypeViewModel? currentViewModel = navigation.currentViewModel as CreateBookTypeViewModel;

                if (button == "cancel")
                {
                    navigation.currentViewModel = new ManageBookTypesViewModel(session, navigation);
                    return;
                }

                if(button == "create" && currentViewModel.Name != string.Empty && !currentViewModel.HasErrors)
                {
                    currentViewModel.BookTypeAlreadyExists = false;

                    if (DBUtils.doesBookTypeExists(currentViewModel.Name))
                    {
                        currentViewModel.BookTypeAlreadyExists = true;
                        return;
                    }

                    BookType bookType = new BookType(currentViewModel.Name);
                    bookType.store();

                    navigation.currentViewModel = new ManageBookTypesViewModel(session, navigation);
                }
            }
            if (entity == "publisher")
            {
                CreatePublisherViewModel? currentViewModel = navigation.currentViewModel as CreatePublisherViewModel;

                if (button == "cancel")
                {
                    navigation.currentViewModel = new ManagePublishersViewModel(session, navigation);
                    return;
                }

                if (button == "create" && currentViewModel.Name != string.Empty && !currentViewModel.HasErrors)
                {
                    currentViewModel.PublisherAlreadyExists = false;

                    if (DBUtils.doesPublisherExists(currentViewModel.Name))
                    {
                        currentViewModel.PublisherAlreadyExists = true;
                        return;
                    }

                    Publisher publisher = new Publisher(currentViewModel.Name);
                    publisher.store();

                    navigation.currentViewModel = new ManagePublishersViewModel(session, navigation);
                }
            }
            if (entity == "author")
            {
                CreateAuthorViewModel? currentViewModel = navigation.currentViewModel as CreateAuthorViewModel;

                if (button == "cancel")
                {
                    navigation.currentViewModel = new ManageAuthorsViewModel(session, navigation);
                    return;
                }

                if (button == "create" && !currentViewModel.HasErrors && !currentViewModel.EmptyFields)
                {
                    currentViewModel.AuthorAlreadyExists = false;

                    if (DBUtils.doesAuthorExists(currentViewModel.FirstName, currentViewModel.LastName))
                    {
                        currentViewModel.AuthorAlreadyExists = true;
                        return;
                    }

                    Author author = new Author(currentViewModel.FirstName, currentViewModel.LastName, currentViewModel.BirthDate);
                    author.store();

                    navigation.currentViewModel = new ManageAuthorsViewModel(session, navigation);
                }
            }
            if (entity == "book")
            {
                CreateBookViewModel? currentViewModel = navigation.currentViewModel as CreateBookViewModel;

                if (button == "cancel")
                {
                    navigation.currentViewModel = new ManageBooksViewModel(session, navigation);
                    return;
                }

                if (button == "create" && !currentViewModel.HasErrors && !currentViewModel.EmptyFields)
                {
                    currentViewModel.BookAlreadyExists = false;

                    if (DBUtils.doesBookExists(currentViewModel.Title))
                    {
                        currentViewModel.BookAlreadyExists = true;
                        return;
                    }

                    Book book = new Book(
                            currentViewModel.Title,
                            currentViewModel.PublishDate,
                            currentViewModel.BookType,
                            currentViewModel.Publisher,
                            new List<Author>(currentViewModel.Authors),
                            Convert.ToInt32(currentViewModel.Stock)
                        );
                    book.store();

                    navigation.currentViewModel = new ManageBooksViewModel(session, navigation);
                }
            }
        }

        public CreateEntityCommand(string entity, string button, Session session, Navigation navigation)
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
