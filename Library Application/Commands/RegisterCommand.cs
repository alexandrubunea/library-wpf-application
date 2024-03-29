using Library_Application.Database;
using Library_Application.Models;
using Library_Application.Stores;
using Library_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Library_Application.Commands
{
    internal class RegisterCommand : CommandBase
    {
        // public

        public RegisterCommand(string buttonName, Navigation navigation)
        {
            this.buttonName = buttonName;
            this.navigation = navigation;
        }

        public override void Execute(object? parameter)
        {
            RegisterViewModel? viewModel = navigation.currentViewModel as RegisterViewModel;
            if(buttonName == "register" && viewModel.NotEmptyData && !viewModel.HasErrors)
            {
                if (DBUtils.doesAccountExists(viewModel.Email, viewModel.Phone))
                {
                    viewModel.AccountAlreadyExists = true;
                    return;
                }

                viewModel.AccountAlreadyExists = false;

                User newUser = new User(
                        viewModel.FirstName,
                        viewModel.LastName,
                        viewModel.Email,
                        viewModel.Phone,
                        0
                    );

                newUser.store();
                newUser.updatePassword(viewModel.Password);

                navigation.currentViewModel = new LoginViewModel(navigation);
            }
            else if(buttonName == "back")
            {
                navigation.currentViewModel = new StartViewModel(navigation);
            }
        }

        // private
        private string buttonName;
        private Navigation navigation;
    }
}
