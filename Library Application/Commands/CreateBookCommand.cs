using Library_Application.Stores;
using Library_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Application.Commands
{
    class CreateBookCommand : CommandBase
    {
        // public
        public CreateBookCommand(Navigation navigation, string button)
        {
            this.navigation = navigation;
            this.button = button;
        }
        public override void Execute(object? parameter)
        {
            if (parameter == null)
                return;


            CreateBookViewModel? currentViewModel = navigation.currentViewModel as CreateBookViewModel;

            if(button == "addauthor")
            {
                if (currentViewModel.Authors.FirstOrDefault(author => author.Id == currentViewModel.SelectedAllAuthor.Id) != default)
                    return;

                currentViewModel.Authors.Add(currentViewModel.SelectedAllAuthor);
                currentViewModel.CurrentAuthorsCollectionView.Refresh();
                return;
            }
            if(button == "removeauthor")
            {
                currentViewModel.Authors.Remove(currentViewModel.SelectedBookAuthor);
                currentViewModel.CurrentAuthorsCollectionView.Refresh();
                return;
            }
        }

        // private
        private readonly Navigation navigation;
        private readonly string button;
    }
}
